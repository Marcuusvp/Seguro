namespace Seguros.HttpApi.Dominio.Apolices.Regras;

public class RegraCalculo
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public string ConteudoJson { get; set; }
    public bool Ativa { get; set; }
}
