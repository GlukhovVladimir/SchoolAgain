using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainBusinessLogic.Enums;
using SchoolAgainBusinessLogic.HelperModels;

namespace SchoolAgainBusinessLogic.BusinessLogic
{
    public class SaveToPdf
    {
        public static void CreateDoc(PdfInfo info)
        {
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            foreach (var reception in info.Receptions)
            {
                var receptionLabel = section.AddParagraph("Услуга №" + reception.Id + " от " + reception.DateCreate.ToShortDateString());
                receptionLabel.Style = "NormalTitle";
                receptionLabel.Format.SpaceBefore = "1cm";
                receptionLabel.Format.SpaceAfter = "0,25cm";
                var servicesLabel = section.AddParagraph("Услуги:");
                servicesLabel.Style = "NormalTitle";
                var serviceTable = document.LastSection.AddTable();
                List<string> headerWidths = new List<string> { "1cm", "3,5cm", "3cm", "3,5cm" };
                foreach (var elem in headerWidths)
                {
                    serviceTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = serviceTable,
                    Texts = new List<string> { "№", "Название", "Цена", "Количество" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                int i = 1;
                foreach (var service in reception.ReceptionServices)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = serviceTable,
                        Texts = new List<string> { i.ToString(), service.ServiceName, (service.Price * service.Count).ToString(), service.Count.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    i++;
                }

                CreateRow(new PdfRowParameters
                {
                    Table = serviceTable,
                    Texts = new List<string> { "", "", "Итого:", reception.TotalSum.ToString() },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Left
                });
                if (reception.ReceptionStatus == ReceptionStatus.Оформлен)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = serviceTable,
                        Texts = new List<string> { "", "", "К оплате:", reception.TotalSum.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
                else
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = serviceTable,
                        Texts = new List<string> { "", "", "К оплате:", reception.LeftSum.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                }
                if (info.Payments[reception.Id].Count == 0)
                {
                    continue;
                }
                var paymentsLabel = section.AddParagraph("Платежи:");
                paymentsLabel.Style = "NormalTitle";
                var paymentTable = document.LastSection.AddTable();
                headerWidths = new List<string> { "1cm", "6cm", "6cm", "3cm" };
                foreach (var elem in headerWidths)
                {
                    paymentTable.AddColumn(elem);
                }
                CreateRow(new PdfRowParameters
                {
                    Table = paymentTable,
                    Texts = new List<string> { "№", "Дата", "Сумма" },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                i = 1;
                foreach (var payment in info.Payments[reception.Id])
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = paymentTable,
                        Texts = new List<string> { i.ToString(), payment.DatePayment.ToString(), payment.Sum.ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Left
                    });
                    i++;
                }
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(info.FileName);
        }
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
