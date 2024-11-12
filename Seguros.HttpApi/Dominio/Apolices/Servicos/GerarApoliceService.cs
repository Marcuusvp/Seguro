using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
namespace Seguros.HttpApi.Dominio.Apolices.Servicos;

public class GerarApoliceService(IConfiguration configuration)
{
    public byte[] GerarApolice(Apolice apolice)
    {
        string _logoPath = configuration["Paths:LogoPath"];

        QuestPDF.Settings.License = LicenseType.Community;
        using (var stream = new MemoryStream())
        {
            Document.Create(apolicePdf =>
            {
                apolicePdf.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header().Height(100).Image(_logoPath);

                    page.Content()
                    .PaddingVertical(10)
                    .Column(column =>
                    {
                        column.Item().Text("Dados do Segurado").FontSize(14).Bold().Underline();
                        column.Item().Text($"Nome: {apolice.Proprietario.Nome}");
                        column.Item().Text($"CPF: {apolice.Proprietario.Cpf}");
                        column.Item().Text($"Endereço: {apolice.Proprietario.Residencia.Uf}, {apolice.Proprietario.Residencia.Cidade}, {apolice.Proprietario.Residencia.Bairro}");

                        column.Spacing(10);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            table.Cell().ColumnSpan(3).LabelCell("Veículo segurado:");
                            table.Cell().ValueCell().Text($"Marca: {apolice.Veiculo.Marca}");
                            table.Cell().ValueCell().Text($"Modelo: {apolice.Veiculo.Modelo}");
                            table.Cell().ValueCell().Text($"Ano: {apolice.Veiculo.Ano}");
                        });

                        column.Spacing(10);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            table.Cell().ColumnSpan(2).LabelCell("Condutores segurados:");
                            foreach (var condutor in apolice.Condutores)
                            {
                                table.Cell().LabelCell($"{condutor.Cpf}");
                                table.Cell().LabelCell($"{condutor.DataNascimento.ToString("dd/MM/yyyy")}");
                            }
                        });

                        column.Spacing(10);
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });
                            table.Cell().ColumnSpan(4).LabelCell("Coberturas contratadas:");
                            table.Cell().LabelCell(apolice.Cobertura.Colisao ? "SIM" : "NÃO");
                            table.Cell().LabelCell(apolice.Cobertura.RouboFurto ? "SIM" : "NÃO");
                            table.Cell().LabelCell(apolice.Cobertura.Terceiros ? "SIM" : "NÃO");
                            table.Cell().LabelCell(apolice.Cobertura.Residencial ? "SIM" : "NÃO");
                        });

                    });

                    page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Span("Obrigado por escolher nossos serviços. Para mais informações, entre em contato com nosso suporte.");
                            });

                });
            }).GeneratePdf(stream);

            return stream.ToArray();
        }
    }        
}

static class SimpleExtension
{
    private static IContainer Cell(this IContainer container, bool dark)
    {
        return container
            .Border(1)
            .Background(dark ? Colors.Grey.Lighten2 : Colors.White)
            .Padding(10);
    }

    // displays only text label
    public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).Medium();

    // allows you to inject any type of content, e.g. image
    public static IContainer ValueCell(this IContainer container) => container.Cell(false);
}
