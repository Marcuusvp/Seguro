namespace Seguros.HttpApi.Dominio.Apolices.Regras
{
    public static class IdadeMinima
    {
        public static bool IdadeValida(DateOnly dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            return idade >= 18;
        }
    }
}
