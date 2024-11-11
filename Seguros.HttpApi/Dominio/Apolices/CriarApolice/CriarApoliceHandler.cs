
using Seguros.HttpApi.Dominio.Apolices.EntityFactory;
using Seguros.HttpApi.Dominio.Apolices.Enums;
using Seguros.HttpApi.Dominio.Apolices.Servicos.Auxiliares;
using Seguros.HttpApi.Dominio.Apolices.Validators;
using Seguros.HttpApi.Dominio.RiscoPorLocalidade;

namespace Seguros.HttpApi.Dominio.Apolices.CriarApolice;
public record CriarApoliceCommand(VeiculoApolice Veiculo,
        ProprietarioApolice Proprietario,
        List<CondutorApolice> Condutores,
        EnderecoApolice Endereco,
        CoberturaApolice Cobertura) : ICommand<Result<CriarApoliceResult>>;
public record CriarApoliceResult(Guid Id);

public class CreateApoliceCommandValidator : AbstractValidator<CriarApoliceCommand>
{
    public CreateApoliceCommandValidator()
    {
        RuleFor(p => p.Veiculo)
            .NotNull().WithMessage("O veículo deve ser informado")
            .SetValidator(new VeiculoApoliceValidator());

        RuleFor(p => p.Proprietario)
            .NotNull().WithMessage("O proprietário deve ser informado")
            .SetValidator(new ProprietarioApoliceValidator());

        RuleFor(p => p.Condutores)
            .NotEmpty().WithMessage("Um ou mais condutores devem ser informados")
            .ForEach(condutorRule => condutorRule.SetValidator(new CondutorApoliceValidator()));

        RuleFor(p => p.Endereco)
            .NotNull().WithMessage("Endereço deve ser informado")
            .SetValidator(new EnderecoApoliceValidator());

        RuleFor(p => p.Cobertura)
            .NotNull().WithMessage("Informe os serviços de cobertura")
            .SetValidator(new CoberturaApoliceValidator());
    }
}
internal class CriarApoliceHandler(ApoliceRepository _apoliceRepository
    , IUnitOfWork _unitOfWork
    , IFipeService _fipeService
    , ProprietarioRepository _proprietarioRepository
    , CondutorRepository _condutorRepository
    , IHistoricoAcidentesService _historicoAcidentesService
    , RiscoPorLocalidadeRepository _riscoPorLocalidadeRepository
    , CalculoRiscoService _calculoRisco
    , CalculoValorSeguroService _calculoValor) : ICommandHandler<CriarApoliceCommand, Result<CriarApoliceResult>>
{
    public async Task<Result<CriarApoliceResult>> Handle(CriarApoliceCommand request, CancellationToken cancellationToken)
    {
        // Criar o veículo
        var veiculo = new Veiculo(request.Veiculo.Marca, request.Veiculo.Modelo, request.Veiculo.Ano, request.Veiculo.Tipo);

        var obterValorVeiculo = await _fipeService.ObterValorVeiculoAsync(
            tipoVeiculo: veiculo.Tipo.ToString()
            ,marca: veiculo.Marca
            ,modelo: veiculo.Modelo
            ,ano: veiculo.Ano,
            cancellationToken);

        // Criar o proprietário
        var proprietario = await _proprietarioRepository.ObterPorCpfAsync(request.Proprietario.Cpf, cancellationToken);

        if (proprietario == null)
        {
            // Criar novo proprietário
            var proprietarioResult = request.Proprietario.ToEntity();

            if (proprietarioResult.IsFailure)
                return Result.Failure<CriarApoliceResult>(proprietarioResult.Error);

            proprietario = proprietarioResult.Value;
            await _proprietarioRepository.AdicionarAsync(proprietario.Value, cancellationToken);
        }
        else
        {
            // Atualizar o endereço se necessário
            var novoEndereco = new Endereco(
                request.Proprietario.Residencia.Uf,
                request.Proprietario.Residencia.Cidade,
                request.Proprietario.Residencia.Bairro);

            if (!proprietario.Value.Residencia.Equals(novoEndereco))
            {
                proprietario.Value.AtualizaEndereco(novoEndereco);
                await _proprietarioRepository.AtualizarAsync(proprietario.Value, cancellationToken);
            }
        }
        // 4. Obter ou criar os condutores
        var condutoresSegurados = new List<Condutor>();

        foreach (var condutorRequest in request.Condutores)
        {
            var condutor = await _condutorRepository.ObterPorCpfAsync(condutorRequest.Cpf, cancellationToken);

            if (condutor == null)
            {
                // Criar novo condutor
                var condutorResult = condutorRequest.ToEntity();

                if (condutorResult.IsFailure)
                    return Result.Failure<CriarApoliceResult>(condutorResult.Error);

                condutor = condutorResult.Value;
                await _condutorRepository.AdicionarAsync(condutor.Value, cancellationToken);
            }
            else
            {
                // Atualizar o endereço se necessário
                var novoEndereco = new Endereco(
                    condutorRequest.Residencia.Uf,
                    condutorRequest.Residencia.Cidade,
                    condutorRequest.Residencia.Bairro);

                if (!condutor.Value.Residencia.Equals(novoEndereco))
                {
                    condutor.Value.AtualizarEndereco(novoEndereco);
                    await _condutorRepository.AtualizarAsync(condutor.Value, cancellationToken);
                }
            }
            condutoresSegurados.Add(condutor.Value);
        }

        int riscoApolice = 0;
        foreach (var condutor in condutoresSegurados)
        {      
            var acidentesResult = await _historicoAcidentesService.ObterQuantidadeAcidentesAsync(condutor.Cpf, cancellationToken);
            if (acidentesResult.IsFailure)
                return Result.Failure<CriarApoliceResult>(acidentesResult.Error);
            var acidentes = acidentesResult.Value;
            var riscoLocalidade = await _riscoPorLocalidadeRepository.ObterNivelRiscoLocalidadeAsync(condutor.Residencia.Uf, condutor.Residencia.Cidade, condutor.Residencia.Bairro);
            var riscoCondutor = await _calculoRisco.CalcularNivelRiscoAsync(condutor, acidentes, riscoLocalidade);
            riscoApolice += riscoCondutor;
        }

        // Criar o endereço
        var endereco = new Endereco(
            request.Endereco.Uf,
            request.Endereco.Cidade,
            request.Endereco.Bairro);

        // Criar a cobertura
        var cobertura = new Cobertura(
            request.Cobertura.RouboFurto,
            request.Cobertura.Colisao,
            request.Cobertura.Terceiros,
            request.Cobertura.Residencial);

        var coberturasDesejadas = ListaCoberturasSeleciondas.GerarListaDeCoberturas(cobertura);
        var valorApolice = await _calculoValor.CalcularValorSeguroAsync(obterValorVeiculo.Value, riscoApolice, coberturasDesejadas);

        // Criar a apólice
        var apoliceResult = Apolice.Criar(
            veiculo,
            proprietario.Value,
            condutoresSegurados,
            endereco,
            cobertura,
            valorApolice);

        if (apoliceResult.IsFailure)
            return Result.Failure<CriarApoliceResult>(apoliceResult.Error);

        var apolice = apoliceResult.Value;

        await _apoliceRepository.Adicionar(apolice, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(new CriarApoliceResult(apolice.Id));
    }
}

public record VeiculoApolice(string Marca, string Modelo, string Ano, ETipoVeiculo Tipo);
public record ProprietarioApolice(string Cpf, string Nome, DateOnly DataNascimento, EnderecoApolice Residencia);
public record CondutorApolice(string Cpf, DateOnly DataNascimento, EnderecoApolice Residencia);
public record EnderecoApolice(string Uf, string Cidade, string Bairro);
public record CoberturaApolice(bool RouboFurto, bool Colisao, bool Terceiros, bool Residencial);
