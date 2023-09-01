namespace DentallApp.Features.SpecificTreatments.DTOs;

public class SpecificTreatmentRangeToPayDto
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
