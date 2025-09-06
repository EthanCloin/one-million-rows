namespace OneMillionRows.Models.IntermediateStructures;

public class Average(decimal Sum, int Count)
{
    // public decimal Value => Count == 0 ? 0 : Sum / Count;

    /// <summary>
    /// Adds a new value to the average calculation, incrementing count.
    /// </summary>    
    public void Add(decimal value)
    {
        Sum += value;
        Count++;
    }
    public decimal GetValue()
    {
        return Count == 0 ? 0 : Sum / Count;
    }
    public decimal GetSum()
    {
        return Sum;
    }
    public int GetCount()
    {
        return Count;
    }
}