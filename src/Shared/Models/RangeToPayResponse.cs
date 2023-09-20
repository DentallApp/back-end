namespace DentallApp.Shared.Models;

public class RangeToPayResponse
{
    public double PriceMin { get; init; }
    public double PriceMax { get; init; }

    public override string ToString()
    {
        return PriceMin != PriceMax ?
            string.Format(RangeToPayMinMaxMessage, PriceMin, PriceMax) :
            string.Format(RangeToPayMessage, PriceMax);
    }
}
