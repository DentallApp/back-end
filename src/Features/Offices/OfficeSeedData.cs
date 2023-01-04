namespace DentallApp.Features.Offices;

public static class OfficeSeedData
{
    public static ModelBuilder CreateDefaultOffices(this ModelBuilder builder)
    {
        builder.AddSeedData(
            new Office
            {
                Id = 1,
                Name = "Mapasingue",
                Address = "Mapasingue oeste Av4ta entre calle 4ta, y, Guayaquil 090101"
            },
            new Office
            {
                Id = 2,
                Name = "El Triunfo",
                Address = "Recinto el Piedrero frente al centro de salud"
            },
            new Office
            {
                Id = 3,
                Name = "Naranjito",
                Address = "Vía principal Naranjito - Bucay, al lado de Ferreteria López"
            }
        );
        return builder;
    }
}
