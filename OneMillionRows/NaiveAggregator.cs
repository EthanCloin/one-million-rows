using DocumentFormat.OpenXml.Packaging;
using OneMillionRows.Models;

internal class NaiveAggregator
{
    private SpreadsheetDocument _document;
    private Dictionary<DateDomain, Average> _dateDomainAverages = new();
    private Dictionary<DateLocation, Average> _dateLocationAverages = new();
    private Dictionary<string, Average> _domainAverages = new();
    private Dictionary<string, Average> _locationAverages = new();

    public NaiveAggregator(string pathToSpreadsheet)
    {
        _document = OpenDocumentReadOnly(pathToSpreadsheet);
        Console.WriteLine("Spreadsheet opened successfully.");
    }
    private SpreadsheetDocument OpenDocumentReadOnly(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"The file at path {path} was not found.");
        }
        return SpreadsheetDocument.Open(path, false);
    }

}