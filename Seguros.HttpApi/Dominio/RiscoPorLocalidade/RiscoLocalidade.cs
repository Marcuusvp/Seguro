namespace Seguros.HttpApi.Dominio.RiscoPorLocalidade;

public sealed class RiscoLocalidade
{
    public Guid Id { get; set; }
    public string UF { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string NivelRisco { get; set; }
}
