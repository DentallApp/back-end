namespace DentallApp.Features.ChatBot.Helpers;

public class AdaptiveCardsLoader
{
    private const string BasePath = "./AdaptiveCards/";

    public static Task<string> LoadDentalServiceCardAsync()
    {
        return File.ReadAllTextAsync($"{BasePath}DentalServiceCard.json");
    }

    public static Task<string> LoadDentistCardAsync()
    {
        return File.ReadAllTextAsync($"{BasePath}DentistCard.json");
    }

    public static Task<string> LoadOfficeCardAsync()
    {
        return File.ReadAllTextAsync($"{BasePath}OfficeCard.json");
    }

    public static Task<string> LoadPatientCardAsync()
    {
        return File.ReadAllTextAsync($"{BasePath}PatientCard.json");
    }

    public static Task<string> LoadAppointmentDateCardAsync()
    {
        return File.ReadAllTextAsync($"{BasePath}AppointmentDateCard.json");
    }
}
