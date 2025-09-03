namespace OneMillionRows.Models;

public class Average(decimal Sum, int Count)
{
    public decimal Value => Count == 0 ? 0 : Sum / Count;
}