using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OneMillionRows.Models.IntermediateStructures;


internal class NaiveAggregator
{
    private SpreadsheetDocument _document;
    private Dictionary<DateDomain, Average> _dateDomainAverages = new();
    private Dictionary<DateLocation, Average> _dateLocationAverages = new();
    private Dictionary<string, Average> _domainAverages = new();
    private Dictionary<string, Average> _locationAverages = new();
    private readonly Dictionary<int, string> _headerIndices = new Dictionary<int, string>
    {
        { 0, "Date" },
        { 1, "Domain" },
        { 2, "Location" },
        { 3, "Value" },
    };

    public NaiveAggregator(string pathToSpreadsheet)
    {
        _document = OpenDocumentReadOnly(pathToSpreadsheet);
        Console.WriteLine("Spreadsheet opened successfully.");
        var result = AggregateData();
        Console.WriteLine("Data aggregation completed.");
    }
    private SpreadsheetDocument OpenDocumentReadOnly(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"The file at path {path} was not found.");
        }
        return SpreadsheetDocument.Open(path, false);
    }

    private FinalResult AggregateData()
    {
        // var domSheetData = domDocument.WorkbookPart.WorksheetParts.First().Worksheet.Elements<SheetData>().First();
        var sheetData = _document?.WorkbookPart?.WorksheetParts.First()?.Worksheet?.GetFirstChild<SheetData>();
        Console.Write(sheetData);
        if (sheetData == null)
        {
            throw new InvalidOperationException("The spreadsheet does not contain any sheet data.");
        }
        // start reading rows
        int columnIndex = 0;
        int rowIndex = 0;
        foreach (Row row in sheetData.Elements<Row>())
        {
            columnIndex = 0;
            if (rowIndex == 0)
            {
                // header row, skip
                rowIndex++;
                continue; // skip header row
            }
            Console.Write($"Row {row.RowIndex}:");
            foreach (Cell cell in row.Elements<Cell>())
            {
                string cellValue = cell.CellValue?.Text ?? string.Empty;
                // var formattedValue = ValueFormatter.FormatValue(cellValue, _headerIndices[columnIndex]);
                Console.Write($"{cellValue} (Col {_headerIndices[columnIndex]}), ");
                columnIndex++;
            }
            Console.WriteLine();
        }

        return null;
    }

}