namespace DentallApp.Domain.ValueObjects;

public record class PayRange
{
    private PayRange() { }

    public double PriceMin { get; private set; }
    public double PriceMax { get; private set; }
    public string Description
    {
        get
        {
            return PriceMin != PriceMax ?
                string.Format(RangeToPayMinMaxMessage, PriceMin, PriceMax) :
                string.Format(RangeToPayMessage, PriceMax);
        }
    }

    public static PayRange Create(double priceMin, double priceMax)
    {
        return new PayRange { PriceMin = priceMin, PriceMax = priceMax };
    }
}
