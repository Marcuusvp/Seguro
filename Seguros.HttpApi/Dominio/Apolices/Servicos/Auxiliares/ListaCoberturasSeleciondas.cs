namespace Seguros.HttpApi.Dominio.Apolices.Servicos.Auxiliares;

public static class ListaCoberturasSeleciondas
{
    public static List<string> GerarListaDeCoberturas(Cobertura cobertura)
    {
        var coberturas = new List<string>();

        if (cobertura.RouboFurto)
            coberturas.Add("RouboFurto");

        if (cobertura.Colisao)
            coberturas.Add("Colisao");

        if (cobertura.Terceiros)
            coberturas.Add("Terceiros");

        if (cobertura.Residencial)
            coberturas.Add("ProtecaoResidencial");

        return coberturas;
    }
}
