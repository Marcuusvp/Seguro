namespace Seguros.HttpApi.Dominio.Apolices.Regras;
public class RegraCustoCobertura
{
    public string Cobertura { get; set; } // Ex: "RouboFurto", "Colisao"
    public decimal PercentualCustoBase { get; set; }
    public decimal CustoFixo { get; set; }
}
