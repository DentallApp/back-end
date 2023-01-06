namespace DentallApp.Features.SpecificTreatments.DTOs;

public class SpecificTreatmentRangeToPayDto
{
    public double PriceMin { get; set; }
    public double PriceMax { get; set; }

    public override string ToString()
        => PriceMin != PriceMax ?
                    string.Format(RangeToPayMinMaxMessage, PriceMin, PriceMax) :
                    string.Format(RangeToPayMessage, PriceMax);
}
