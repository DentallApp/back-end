namespace DentallApp.Shared.Reasons;

public readonly ref struct PaymentSuccess
{
    public string Message { get; }

    public PaymentSuccess(double price)
        => Message = string.Format(Messages.PayableValue, price);

    public PaymentSuccess(double priceMin, double priceMax)
        => Message = string.Format(Messages.RangeToPayMinMax, priceMin, priceMax);
}
