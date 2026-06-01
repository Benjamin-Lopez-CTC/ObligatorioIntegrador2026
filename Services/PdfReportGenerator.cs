using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
using ObligatorioIntegrador2026.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ObligatorioIntegrador2026.Services
{
    public class PdfReportGenerator
    {
        public static byte[] GenerateGlobalReport(
            int totalApiarios,
            int totalColmenas,
            double totalMiel,
            int totalAlertas,
            List<Apiario> apiarios,
            List<Colmena> colmenasEnAlerta,
            List<Movimiento> movimientos,
            List<Equipment> inventarioBajoStock,
            bool hasActiveAnalysis,
            double activeTotalInversion,
            double activeGananciaBruta,
            double activeBalanceNeto,
            string? logoPath = null)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));

                    page.Header().Element(container => ComposeHeader(container, logoPath));
                    page.Content().Element(x => ComposeContent(x, totalApiarios, totalColmenas, totalMiel, totalAlertas, apiarios, colmenasEnAlerta, movimientos, inventarioBajoStock, hasActiveAnalysis, activeTotalInversion, activeGananciaBruta, activeBalanceNeto));
                    page.Footer().Element(ComposeFooter);
                });
            });

            return document.GeneratePdf();
        }

        private static void ComposeHeader(IContainer container, string? logoPath)
        {
            container.Row(row =>
            {
                if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
                {
                    row.ConstantItem(40).Image(logoPath);
                    row.ConstantItem(12);
                }

                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("ZÁNGANOS S.A.").FontSize(20).SemiBold().FontColor("#D9A000"); // Primary-like color
                    column.Item().Text("Reporte Integral de Operaciones").FontSize(14).FontColor(Colors.Grey.Darken2);
                    column.Item().Text($"Fecha de emisión: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        }

        private static void ComposeContent(
            IContainer container, 
            int totalApiarios, 
            int totalColmenas, 
            double totalMiel, 
            int totalAlertas, 
            List<Apiario> apiarios, 
            List<Colmena> colmenasEnAlerta, 
            List<Movimiento> movimientos, 
            List<Equipment> inventarioBajoStock,
            bool hasActiveAnalysis,
            double activeTotalInversion,
            double activeGananciaBruta,
            double activeBalanceNeto)
        {
            container.PaddingVertical(1, Unit.Centimetre).Column(column => 
            {
                // Resumen de KPIs
                column.Item().PaddingBottom(5).Text("Resumen Global").FontSize(14).SemiBold().FontColor(Colors.Black);
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Total Apiarios").SemiBold();
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Colmenas Activas").SemiBold();
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Miel Acumulada").SemiBold();
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Alertas Activas").SemiBold().FontColor(Colors.Red.Medium);

                    table.Cell().Padding(5).Text(totalApiarios.ToString());
                    table.Cell().Padding(5).Text(totalColmenas.ToString());
                    table.Cell().Padding(5).Text($"{totalMiel:0.##} kg");
                    table.Cell().Padding(5).Text(totalAlertas.ToString()).FontColor(Colors.Red.Medium);
                });

                column.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten3);

                // Colmenas en Alerta
                column.Item().PaddingBottom(5).Text("Colmenas que requieren atención (Alerta / Crítico)").FontSize(14).SemiBold().FontColor(Colors.Black);
                if (colmenasEnAlerta.Any())
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Identificador").SemiBold();
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Apiario").SemiBold();
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Estado").SemiBold();
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Est. Reina").SemiBold();
                        });

                        foreach (var colmena in colmenasEnAlerta)
                        {
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(colmena.Identificador ?? colmena.CodigoEscaneo);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(colmena.Apiario?.Nombre ?? "N/A");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(colmena.Estado).FontColor(colmena.Estado == "Crítico" ? Colors.Red.Medium : Colors.Orange.Medium).SemiBold();
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(colmena.EstadoReina).FontSize(9);
                        }
                    });
                }
                else
                {
                    column.Item().Text("No hay colmenas en estado de alerta.").Italic().FontColor(Colors.Grey.Medium);
                }

                column.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten3);

                // Movimientos Activos
                column.Item().PaddingBottom(5).Text("Movimientos Activos").FontSize(14).SemiBold().FontColor(Colors.Black);
                if (movimientos.Any())
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Colmena").SemiBold();
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Origen").SemiBold();
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Destino").SemiBold();
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Retorno Estimado").SemiBold();
                        });

                        foreach (var mov in movimientos)
                        {
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(mov.Colmena?.Identificador ?? mov.Colmena?.CodigoEscaneo);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(mov.ApiarioOrigen?.Nombre ?? "N/A");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(mov.ApiarioDestino?.Nombre ?? "N/A");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(mov.FechaRegreso.ToString("dd/MM/yyyy"));
                        }
                    });
                }
                else
                {
                    column.Item().Text("No hay movimientos activos actualmente.").Italic().FontColor(Colors.Grey.Medium);
                }

                column.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten3);

                // Inventario Bajo Stock
                column.Item().PaddingBottom(5).Text("Inventario Bajo Stock Mínimo").FontSize(14).SemiBold().FontColor(Colors.Black);
                if (inventarioBajoStock.Any())
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Item").SemiBold();
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Categoría").SemiBold();
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Stock Actual").SemiBold();
                            header.Cell().Background("#F0F0F0").Padding(4).Text("Stock Mínimo").SemiBold();
                        });

                        foreach (var item in inventarioBajoStock)
                        {
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(item.Name);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(item.Category);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(item.Stock.ToString()).FontColor(Colors.Red.Medium).SemiBold();
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(4).Text(item.LowThreshold.ToString());
                        }
                    });
                }
                else
                {
                    column.Item().Text("El stock de todo el equipamiento es adecuado.").Italic().FontColor(Colors.Grey.Medium);
                }

                column.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten3);

                // Análisis Financiero Activo
                column.Item().PaddingBottom(5).Text("Análisis Financiero (Temporada Activa)").FontSize(14).SemiBold().FontColor(Colors.Black);
                if (hasActiveAnalysis)
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Inversión Total").SemiBold();
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Ganancia Bruta").SemiBold();
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Balance Neto").SemiBold();

                        table.Cell().Padding(5).Text($"$ {activeTotalInversion:N2}").FontColor("#D32F2F");
                        table.Cell().Padding(5).Text($"$ {activeGananciaBruta:N2}").FontColor("#2E7D32");
                        
                        var netColor = activeBalanceNeto >= 0 ? "#2E7D32" : "#D32F2F";
                        var netSign = activeBalanceNeto >= 0 ? "" : "-";
                        table.Cell().Padding(5).Text($"$ {netSign}{Math.Abs(activeBalanceNeto):N2}").FontColor(netColor).SemiBold();
                    });
                }
                else
                {
                    column.Item().Text("No hay un análisis financiero activo en curso.").Italic().FontColor(Colors.Grey.Medium);
                }

                column.Item().PaddingVertical(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten3);

                // Declaraciones MGAP / SINATPA
                column.Item().PaddingBottom(5).Text("Declaraciones MGAP (SINATPA)").FontSize(14).SemiBold().FontColor(Colors.Black);
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Última Declaración Presentada").SemiBold();
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Próxima Declaración Pendiente").SemiBold();

                    table.Cell().Padding(5).Text("21 de Julio, 2025");
                    table.Cell().Padding(5).Text("Julio 2026 (Válida hasta 30/06/2026)");
                });
            });
        }

        private static void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(x =>
            {
                x.Span("Página ");
                x.CurrentPageNumber();
                x.Span(" de ");
                x.TotalPages();
            });
        }
    }
}
