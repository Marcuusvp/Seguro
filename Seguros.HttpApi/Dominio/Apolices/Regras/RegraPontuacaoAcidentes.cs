namespace Seguros.HttpApi.Dominio.Apolices.Regras;

public class RegraPontuacaoAcidentes
{
    public int AcidentesMinimos { get; set; }
    public int AcidentesMaximos { get; set; }
    public int Pontuacao { get; set; }
}
