internal class FinalResult
{
    public IEnumerable<AverageDailyTrxByDomain> AverageDailyTrxByDomains { get; set; }
    public IEnumerable<AverageDailyTrxByLocation> AverageDailyTrxByLocations { get; set; }
    public IEnumerable<TrxCountByDomain> AverageTrxByDomains { get; set; }
    public IEnumerable<AverageTrxCountByLocation> AverageTrxByLocations { get; set; }
}

internal record AverageDailyTrxByDomain(DateTime Date, string Domain, decimal AverageDailyTrx);
internal record AverageDailyTrxByLocation(DateTime Date, string Location, decimal AverageDailyTrx);
internal record TrxCountByDomain(string Domain, decimal AverageTrx);
internal record AverageTrxCountByLocation(string Location, decimal AverageTrx);
