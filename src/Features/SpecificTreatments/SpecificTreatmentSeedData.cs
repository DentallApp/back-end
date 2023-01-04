namespace DentallApp.Features.SpecificTreatments;

public static class SpecificTreatmentSeedData
{
    public static ModelBuilder CreateDefaultSpecificTreatments(this ModelBuilder builder)
    {
        builder.AddSeedData(
             // Ortodoncia/Brackets
             new SpecificTreatment { Id = 1, GeneralTreatmentId = 1, Name = "Ortodoncia metálica", Price = 80 },
             new SpecificTreatment { Id = 2, GeneralTreatmentId = 1, Name = "Ortodoncia autoligado", Price = 150 },
             new SpecificTreatment { Id = 3, GeneralTreatmentId = 1, Name = "Ortodoncia estética", Price = 200 },
             new SpecificTreatment { Id = 4, GeneralTreatmentId = 1, Name = "Controles ortodoncia", Price = 20 },
             new SpecificTreatment { Id = 5, GeneralTreatmentId = 1, Name = "Reposición de Brackets", Price = 3 },
             new SpecificTreatment { Id = 6, GeneralTreatmentId = 1, Name = "Reposición de tubos", Price = 5 },
             new SpecificTreatment { Id = 7, GeneralTreatmentId = 1, Name = "Microtornillos", Price = 100 },
             new SpecificTreatment { Id = 8, GeneralTreatmentId = 1, Name = "Ligas interdentales", Price = 2 },

             // Calces/resinas
             new SpecificTreatment { Id = 9, GeneralTreatmentId = 2, Name = "Caries compuestas y complejas", Price = 15 },
             new SpecificTreatment { Id = 10, GeneralTreatmentId = 2, Name = "Sellantes", Price = 10 },

             // Tratamientos de conductos/Endodoncia
             new SpecificTreatment { Id = 11, GeneralTreatmentId = 3, Name = "Endodoncia central a canino Bio", Price = 90 },
             new SpecificTreatment { Id = 12, GeneralTreatmentId = 3, Name = "Endodoncia central a canino Necro", Price = 90 },
             new SpecificTreatment { Id = 13, GeneralTreatmentId = 3, Name = "Endodoncia central a canino Retratamiento", Price = 100 },
             new SpecificTreatment { Id = 14, GeneralTreatmentId = 3, Name = "Endodoncia de premolares Bio", Price = 120 },
             new SpecificTreatment { Id = 15, GeneralTreatmentId = 3, Name = "Endodoncia de premolares Necro", Price = 150 },
             new SpecificTreatment { Id = 16, GeneralTreatmentId = 3, Name = "Endodoncia de premolares Retratamiento", Price = 160 },
             new SpecificTreatment { Id = 17, GeneralTreatmentId = 3, Name = "Endodoncia molar Bio", Price = 180 },
             new SpecificTreatment { Id = 18, GeneralTreatmentId = 3, Name = "Endodoncia premolar Necro", Price = 200 },
             new SpecificTreatment { Id = 19, GeneralTreatmentId = 3, Name = "Endodoncia premolar Retratamiento", Price = 200 },

             // Cirugía de terceros molares
             new SpecificTreatment { Id = 20, GeneralTreatmentId = 4, Name = "Exodoncias simples de central a canino", Price = 10 },
             new SpecificTreatment { Id = 21, GeneralTreatmentId = 4, Name = "Exodoncias en niños", Price = 10 },
             new SpecificTreatment { Id = 22, GeneralTreatmentId = 4, Name = "Exodoncias complejas", Price = 15 },
             new SpecificTreatment { Id = 23, GeneralTreatmentId = 4, Name = "Exodoncia 3er molar superior erupcionado", Price = 20 },
             new SpecificTreatment { Id = 24, GeneralTreatmentId = 4, Name = "Exodoncia 3er molar superior retenido", Price = 30 },
             new SpecificTreatment { Id = 25, GeneralTreatmentId = 4, Name = "Exodoncia 3er molar inferior erupcionado", Price = 30 },
             new SpecificTreatment { Id = 26, GeneralTreatmentId = 4, Name = "Exodoncia 3er molar inferior retenido", Price = 50 },

             // Implantes
             new SpecificTreatment { Id = 27, GeneralTreatmentId = 5, Name = "Implantes", Price = 800 },

             // Diseño de sonrisa
             new SpecificTreatment { Id = 28, GeneralTreatmentId = 6, Name = "Carillas resinas", Price = 20 },
             new SpecificTreatment { Id = 29, GeneralTreatmentId = 6, Name = "Carillas ceromero", Price = 80 },
             new SpecificTreatment { Id = 30, GeneralTreatmentId = 6, Name = "Lentes de contacto Emax", Price = 180 },

             // Blanqueamiento
             new SpecificTreatment { Id = 31, GeneralTreatmentId = 7, Name = "Blanqueamiento", Price = 60 },

             // Prótesis fijas/removibles
             new SpecificTreatment { Id = 32, GeneralTreatmentId = 8, Name = "Postes fibra de vidrio", Price = 30 },
             new SpecificTreatment { Id = 33, GeneralTreatmentId = 8, Name = "IKER", Price = 60 },
             new SpecificTreatment { Id = 34, GeneralTreatmentId = 8, Name = "Prótesis parciales acrílicas", Price = 100 },
             new SpecificTreatment { Id = 35, GeneralTreatmentId = 8, Name = "Prótesis total acrílica", Price = 120 },
             new SpecificTreatment { Id = 36, GeneralTreatmentId = 8, Name = "Meriland metal porcelana", Price = 150 },
             new SpecificTreatment { Id = 37, GeneralTreatmentId = 8, Name = "Meriland ceromero", Price = 100 },
             new SpecificTreatment { Id = 38, GeneralTreatmentId = 8, Name = "Meriland zirconio", Price = 300 },
             new SpecificTreatment { Id = 39, GeneralTreatmentId = 8, Name = "Meriland Emax", Price = 220 },
             new SpecificTreatment { Id = 40, GeneralTreatmentId = 8, Name = "Coronas metal porcelana", Price = 80 },
             new SpecificTreatment { Id = 41, GeneralTreatmentId = 8, Name = "Corona disilicato", Price = 200 },
             new SpecificTreatment { Id = 42, GeneralTreatmentId = 8, Name = "Corona zirconio", Price = 230 },
             new SpecificTreatment { Id = 43, GeneralTreatmentId = 8, Name = "Corona ceromero", Price = 80 },
             new SpecificTreatment { Id = 44, GeneralTreatmentId = 8, Name = "Incrustación ceromero", Price = 70 },
             new SpecificTreatment { Id = 45, GeneralTreatmentId = 8, Name = "Incrustación zirconio", Price = 150 },
             new SpecificTreatment { Id = 46, GeneralTreatmentId = 8, Name = "Incrustación Emax", Price = 120 },

             // Periodoncia
             new SpecificTreatment { Id = 47, GeneralTreatmentId = 10, Name = "Gingivectomias y corte de frenillo (por arcada)", Price = 30 }
        );
        return builder;
    }
}
