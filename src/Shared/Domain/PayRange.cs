namespace DentallApp.Shared.Domain;

public class PayRange : ValueObject
{
    public double PriceMin { get; init; }
    public double PriceMax { get; init; }

    public override string ToString()
    {
        return PriceMin != PriceMax ?
            string.Format(RangeToPayMinMaxMessage, PriceMin, PriceMax) :
            string.Format(RangeToPayMessage, PriceMax);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PriceMin; 
        yield return PriceMax;
    }
}
