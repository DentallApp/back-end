namespace DentallApp.Domain.ValueObjects;

public record class PayRange
{
    public double PriceMin { get; private set; }
    public double PriceMax { get; private set; }

    public PayRange() { }

    public PayRange(double priceMin, double priceMax) 
    { 
        PriceMin = priceMin;
        PriceMax = priceMax;
    }

    public string Description
    {
        get
        {
            return PriceMin != PriceMax ?
                string.Format(RangeToPayMinMaxMessage, PriceMin, PriceMax) :
                string.Format(RangeToPayMessage, PriceMax);
        }
    }
}
