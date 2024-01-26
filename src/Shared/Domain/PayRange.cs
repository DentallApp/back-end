namespace DentallApp.Shared.Domain;

public class PayRange : ValueObject
{
    public double PriceMin { get; init; }
    public double PriceMax { get; init; }

    public override string ToString()
    {
        return PriceMin != PriceMax ?
            string.Format(Messages.RangeToPayMinMax, PriceMin, PriceMax) :
            string.Format(Messages.RangeToPay, PriceMax);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PriceMin; 
        yield return PriceMax;
    }
}
