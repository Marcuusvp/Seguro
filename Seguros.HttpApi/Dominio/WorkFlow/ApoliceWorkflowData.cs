public class CriarApoliceWorkflowData
{
    // Dados de entrada
    public VeiculoApolice VeiculoInput { get; set; }
    public ProprietarioApolice ProprietarioInput { get; set; }
    public List<CondutorApolice> CondutoresInput { get; set; }
    public EnderecoApolice EnderecoInput { get; set; }
    public CoberturaApolice CoberturaInput { get; set; }

    // Dados intermediários e de saída
    public Veiculo Veiculo { get; set; }
    public decimal ValorVeiculo { get; set; }
    public Guid ProprietarioId { get; set; }
    public List<Guid> CondutoresIds { get; set; } = new();
    public int RiscoApolice { get; set; }
    public Endereco Endereco { get; set; }
    public Cobertura Cobertura { get; set; }
    public decimal ValorApolice { get; set; }
}
