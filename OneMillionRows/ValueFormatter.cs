internal static class ValueFormatter
{
    public static object FormatValue(object value, string columnName)
    {
        return columnName switch
        {
            "Date" => DateTime.Parse(value.ToString() ?? string.Empty),
            "Domain" => value.ToString() ?? string.Empty,
            "Location" => value.ToString() ?? string.Empty,
            "Value" => decimal.Parse(value.ToString() ?? "0"),
            _ => throw new ArgumentException($"Unknown column name: {columnName}"),
        };
    }
}