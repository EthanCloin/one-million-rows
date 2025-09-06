using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
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
        AggregateData();
        Console.WriteLine("Data aggregation completed.");
        // at this point my dictionaries are populated, with a unique key for each combination of Date+Domain and Date+Location
        // and the corresponding Average object containing the sum and count of values for that key
        // so i just need to convert these to the final output format
    }
    private SpreadsheetDocument OpenDocumentReadOnly(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"The file at path {path} was not found.");
        }
        return SpreadsheetDocument.Open(path, false);
    }

    /// <summary>
    /// Aggregates data from the spreadsheet into in-memory dictionaries.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private void AggregateData()
    {
        // var domSheetData = domDocument.WorkbookPart.WorksheetParts.First().Worksheet.Elements<SheetData>().First();
        var sheetData = _document?.WorkbookPart?.WorksheetParts.First()?.Worksheet?.GetFirstChild<SheetData>();

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
                rowIndex++;
                continue; // skip header row
                // maybe turn this into a check that only headers are the expected ones
            }

            DateTime trxDate = default;
            decimal trxValue = default;
            string trxDomain = default;
            string trxLocation = default;
            foreach (var cell in row.Elements<Cell>())
            {
                string columnName = _headerIndices[columnIndex];
                object cellValue = cell.CellValue?.InnerText ?? string.Empty;
                cellValue = HandleSharedString(cell, cellValue);
                object formattedValue = ValueFormatter.FormatValue(cellValue, columnName);
                switch (columnName)
                {
                    case "Date":
                        trxDate = (DateTime)formattedValue;
                        break;
                    case "Domain":
                        trxDomain = (string)formattedValue;
                        break;
                    case "Location":
                        trxLocation = (string)formattedValue;
                        break;
                    case "Value":
                        trxValue = (decimal)formattedValue;
                        break;
                }
                columnIndex++;
            }
            // Now we have all values for the row, we can update our aggregates
            UpdateAggregateDictionaries(trxDate, trxValue, trxDomain, trxLocation);
            rowIndex++;
        }
    }

    private void UpdateAggregateDictionaries(DateTime trxDate, decimal trxValue, string trxDomain, string trxLocation)
    {
        var dateDomainKey = new DateDomain(trxDate, trxDomain);
        if (!_dateDomainAverages.ContainsKey(dateDomainKey))
        {
            _dateDomainAverages[dateDomainKey] = new Average(trxValue, 1);
        }
        else
        {
            _dateDomainAverages[dateDomainKey].Add(trxValue);
        }

        var dateLocationKey = new DateLocation(trxDate, trxLocation);
        if (!_dateLocationAverages.ContainsKey(dateLocationKey))
        {
            _dateLocationAverages[dateLocationKey] = new Average(trxValue, 1);
        }
        else
        {
            _dateLocationAverages[dateLocationKey].Add(trxValue);
        }

        if (!_domainAverages.ContainsKey(trxDomain))
        {
            _domainAverages[trxDomain] = new Average(trxValue, 1);
        }
        else
        {
            _domainAverages[trxDomain].Add(trxValue);
        }
        if (!_locationAverages.ContainsKey(trxLocation))
        {
            _locationAverages[trxLocation] = new Average(trxValue, 1);
        }
        else
        {
            _locationAverages[trxLocation].Add(trxValue);
        }
    }

    private object HandleSharedString(Cell cell, object cellValue)
    {
        if (cell.DataType?.Value == CellValues.SharedString)
        {
            int sharedStringIndex = int.Parse(cellValue.ToString() ?? "0");
            cellValue = _document?.WorkbookPart?.SharedStringTablePart?.SharedStringTable?.Elements<SharedStringItem>()?.ElementAt(sharedStringIndex)?.InnerText ?? string.Empty;
        }

        return cellValue;
    }
}