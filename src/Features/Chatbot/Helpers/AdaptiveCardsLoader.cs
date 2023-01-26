namespace DentallApp.Features.Chatbot.Helpers;

public class AdaptiveCardsLoader
{
    private const string BasePath = "./Features/Chatbot/AdaptiveCards/";

    public static async Task<string> LoadDentalServiceCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}DentalServiceCard.json");

    public static async Task<string> LoadDentistCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}DentistCard.json");

    public static async Task<string> LoadOfficeCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}OfficeCard.json");

    public static async Task<string> LoadPatientCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}PatientCard.json");

    public static async Task<string> LoadAppointmentDateCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}AppointmentDateCard.json");
}
