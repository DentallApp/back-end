namespace DentallApp.Features.Chatbot.Helpers;

public class TemplateCardLoader
{
    private const string BasePath = "./TemplateCards";

    public static async Task<string> LoadDentalServiceCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}/DentalServiceCard.json");

    public static async Task<string> LoadDentistCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}/DentistCard.json");

    public static async Task<string> LoadOfficeCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}/OfficeCard.json");

    public static async Task<string> LoadPatientCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}/PatientCard.json");

    public static async Task<string> LoadAppointmentDateCardAsync()
        => await File.ReadAllTextAsync($"{BasePath}/AppointmentDateCard.json");
}
