namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class GeneralTreatmentSeedData
{
    public static ModelBuilder CreateDefaultGeneralTreatments(this ModelBuilder builder)
    {
        builder.AddSeedData(
             new GeneralTreatment
             {
                 Id = 1,
                 Name = "Ortodoncia/brackets",
                 Description = "Es la ciencia y arte que se encarga de ubicar las piezas dentales.",
                 ImageUrl = "ortodoncia.png",
                 Duration = 40
             },
             new GeneralTreatment
             {
                 Id = 2,
                 Name = "Calces/resinas",
                 Description = "Su finalidad es la de restaurar o reparar una unidad dentaria (diente) que presenta una cavidad producida por la caries.",
                 ImageUrl = "calce.png",
                 Duration = 40
             },
             new GeneralTreatment
             {
                 Id = 3,
                 Name = "Tratamiento de conductos/endodoncia",
                 Description = "Es un procedimiento que tiene como finalidad preservar las piezas dentales dañadas, evitando así su pérdida.",
                 ImageUrl = "endodoncia.png",
                 Duration = 180
             },
             new GeneralTreatment
             {
                 Id = 4,
                 Name = "Cirugia de tercero molares",
                 Description = "Es el proceso de cirugía oral más frecuentemente derivado por las unidades de salud bucodental de Atención Primaria.",
                 ImageUrl = "cirugia_molares.png",
                 Duration = 90
             },
             new GeneralTreatment
             {
                 Id = 5,
                 Name = "Implantes dentales",
                 Description = "Son elementos metálicos que se ubican quirúrgicamente en los huesos maxilares, debajo de las encías.",
                 ImageUrl = "implantes.png",
                 Duration = 180
             },
             new GeneralTreatment
             {
                 Id = 6,
                 Name = "Diseño de sonrisa",
                 Description = "Es el proceso por el cual se llevan a cabo determinados procesos hasta conseguir el resultado que busca el paciente en lo que a resultados estéticos se refiere.",
                 ImageUrl = "diseno_sonrisa.png",
                 Duration = 40
             },
             new GeneralTreatment
             {
                 Id = 7,
                 Name = "Blanqueamiento",
                 Description = "Es un tratamiento que se aplica a los dientes que han cambiado de color, siendo uno de los tratamiento estéticos más conservadores.",
                 ImageUrl = "blanqueamiento.png",
                 Duration = 40
             },
             new GeneralTreatment
             {
                 Id = 8,
                 Name = "Prótesis fijas/removibles",
                 Description = "Son aquellas en las que sustituimos uno o varios dientes perdidos y se fijan atornilladas o cementadas sobre el implante.",
                 ImageUrl = "protesis_fijas.png",
                 Duration = 40
             },
             new GeneralTreatment
             {
                 Id = 9,
                 Name = "Profilaxis/fluorización",
                 Description = "Es una limpieza con técnicas y herramientas que nos permiten eliminar el sarro, como el detartraje, y la placa bacteriana en todas las zonas de la boca.",
                 ImageUrl = "profilaxis.png",
                 Duration = 40
             },
             new GeneralTreatment
             {
                 Id = 10,
                 Name = "Periodoncia",
                 Description = "Es la especialidad de la odontología que trata las enfermedades de las encías y del hueso que sostiene los dientes.",
                 ImageUrl = "periodoncia.png",
                 Duration = 40
             },
             new GeneralTreatment
             {
                 Id = 11,
                 Name = "Odontopediatria",
                 Description = "Es una rama de la Odontología que atiende y trata las distintas enfermedades bucodentales desde la infancia más temprana hasta finalizar el crecimiento.",
                 ImageUrl = "odontopediatria.png",
                 Duration = 40
             }
        );
        return builder;
    }
}