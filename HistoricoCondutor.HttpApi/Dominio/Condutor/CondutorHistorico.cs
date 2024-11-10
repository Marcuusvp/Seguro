namespace HistoricoCondutor.HttpApi.Dominio.Condutor;

public class CondutorHistorico
{
    public int Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public int QuantidadeAcidentes { get; set; }
}
