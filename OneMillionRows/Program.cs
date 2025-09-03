// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Xml.Schema;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

// document method
var path = Path.GetFullPath("samplesheet.xlsx");
var naiveAggregator = new NaiveAggregator(path);

// var domDocument = SpreadsheetDocument.Open("samplebook.xlsx", false);
// var domSheetData = domDocument.WorkbookPart.WorksheetParts.First().Worksheet.Elements<SheetData>().First();
// string? text;
// int cellCount = 0;
// foreach (Row r in domSheetData.Elements<Row>())
// {
//     foreach (Cell c in r.Elements<Cell>())
//     {
//         text = c?.CellValue?.Text;
//         Console.Write(text + "");
//         cellCount++;
//     }
//     Console.WriteLine();
// }


// stream method
// Stream excelFileStream = File.Open("samplebook.xlsx", FileMode.Open, FileAccess.Read);
// using (SpreadsheetDocument document = SpreadsheetDocument.Open(excelFileStream, false))
// {
//     WorkbookPart workbookPart = document.WorkbookPart;
//     Sheet sheet = workbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
//     WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
//     OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
//     int rowCount = 0;
//     while (reader.Read())
//     {
//         if (reader.ElementType == typeof(Row))
//         {
//             Row row = (Row)reader.LoadCurrentElement();
//             string cellValue = row.GetFirstChild<Cell>().InnerText;
//             Console.WriteLine($"Row {rowCount}: {cellValue}");
//             rowCount++;
//             if (rowCount % 10 == 0)
//             {
//                 Console.WriteLine($"Read {rowCount} rows...");
//                 break;
//             }
//         }
// }
//     Console.WriteLine($"Total rows read: {rowCount}");
// }