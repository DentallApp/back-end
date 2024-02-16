namespace DentallApp.Shared.ValueObjects;

public class PayRange : ValueObject
{
    public double PriceMin { get; init; }
    public double PriceMax { get; init; }

    public override string ToString()
    {
        return PriceMin != PriceMax ?
            new PaymentSuccess(PriceMin, PriceMax).Message :
            new PaymentSuccess(PriceMax).Message;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PriceMin; 
        yield return PriceMax;
    }
}
