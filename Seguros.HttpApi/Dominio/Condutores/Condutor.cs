﻿using Seguros.HttpApi.Dominio.Apolices;

namespace Seguros.HttpApi.Dominio.Condutores;

public sealed class Condutor
{
    public Guid Id { get; }
    public string Cpf { get; }
    public DateOnly DataNascimento { get; }
    public Endereco Residencia { get; private set; }

    private Condutor() { }
    private Condutor(string cpf, DateOnly dataNascimento, Endereco residencia)
    {
        Id = Guid.NewGuid();
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Residencia = residencia;
    }

    public static Result<Condutor> Criar(string cpf, DateOnly dataNascimento, Endereco residencia)
    {
        return Result.Success(new Condutor(cpf, dataNascimento, residencia));
    }

    public void AtualizarEndereco(Endereco endereco)
    {
        Residencia = endereco;
    }
}
