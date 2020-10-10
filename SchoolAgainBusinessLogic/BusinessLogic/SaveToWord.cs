using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using SchoolAgainBusinessLogic.HelperModels;

namespace SchoolAgainBusinessLogic.BusinessLogic
{
    public class SaveToWord
    {
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                Table table = new Table();
                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 }
                    )
                );
                table.AppendChild<TableProperties>(tblProp);
                TableRow headerRow = new TableRow();
                TableCell headerNumberCell = new TableCell(new Paragraph(new Run(new Text("№"))));
                TableCell headerNameCell = new TableCell(new Paragraph(new Run(new Text("Название"))));
                TableCell headerPriceCell = new TableCell(new Paragraph(new Run(new Text("Цена"))));
                headerRow.Append(headerNumberCell);
                headerRow.Append(headerNameCell);
                headerRow.Append(headerPriceCell);
                table.Append(headerRow);
                int i = 1;
                foreach (var service in info.Services)
                {
                    TableRow tourRow = new TableRow();
                    TableCell numberCell = new TableCell(new Paragraph(new Run(new Text(i.ToString()))));
                    TableCell nameCell = new TableCell(new Paragraph(new Run(new Text(service.ServiceName))));
                    TableCell priceCell = new TableCell(new Paragraph(new Run(new Text(service.Price.ToString()))));
                    tourRow.Append(numberCell);
                    tourRow.Append(nameCell);
                    tourRow.Append(priceCell);
                    table.Append(tourRow);
                    i++;
                }
                docBody.Append(table);
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();
                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize
                    {
                        Val = paragraph.TextProperties.Size
                    });
                    if (paragraph.TextProperties.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run,
                        Space = SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }
        private static ParagraphProperties
        CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val = paragraphProperties.Size
                    });
                }
                if (paragraphProperties.Bold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}
