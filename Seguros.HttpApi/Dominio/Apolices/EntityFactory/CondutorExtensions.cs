using Seguros.HttpApi.Dominio.Apolices.CriarApolice;
using Seguros.HttpApi.Dominio.Condutores;

namespace Seguros.HttpApi.Dominio.Apolices.EntityFactory;

public static class CondutorExtensions
{
    public static Result<Condutor> ToEntity(this CondutorApolice dto)
    {
        var endereco = new Endereco(dto.Residencia.Uf, dto.Residencia.Cidade, dto.Residencia.Bairro);
        return Condutor.Criar(dto.Cpf, dto.DataNascimento, endereco);
    }
}
