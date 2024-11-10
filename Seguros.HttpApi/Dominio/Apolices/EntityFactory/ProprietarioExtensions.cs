using Seguros.HttpApi.Dominio.Apolices.CriarApolice;
using Seguros.HttpApi.Dominio.Proprietarios;

namespace Seguros.HttpApi.Dominio.Apolices.EntityFactory;

public static class ProprietarioExtensions
{
    public static Result<Proprietario> ToEntity(this ProprietarioApolice dto)
    {
        var endereco = new Endereco(dto.Residencia.Uf, dto.Residencia.Cidade, dto.Residencia.Bairro);
        return Proprietario.Criar(dto.Cpf, dto.Nome, dto.DataNascimento, endereco);
    }
}
