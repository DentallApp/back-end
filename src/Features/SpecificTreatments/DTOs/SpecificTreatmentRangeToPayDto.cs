namespace DentallApp.Features.SpecificTreatments.DTOs;

public class SpecificTreatmentRangeToPayDto
{
    public double PriceMin { get; set; }
    public double PriceMax { get; set; }

    public override string ToString()
        => PriceMin != PriceMax ?
                    $"El rango a pagar es de ${PriceMin} a ${PriceMax}" :
                    $"El valor a pagar es de ${PriceMax}";
}
