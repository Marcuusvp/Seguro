namespace Seguros.HttpApi.Dominio.Apolices.Servicos.Auxiliares;

public class FipeMarca
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
}

public class FipeModelo
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
}

public class FipeModelosResponse
{
    public List<FipeModelo> Modelos { get; set; }
    public List<FipeAno> Anos { get; set; }
}

public class FipeAno
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
}

public class FipeValorResponse
{
    public string Valor { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int AnoModelo { get; set; }
    public string Combustivel { get; set; }
    public string CodigoFipe { get; set; }
    public string MesReferencia { get; set; }
    public int TipoVeiculo { get; set; }
    public string SiglaCombustivel { get; set; }
}
