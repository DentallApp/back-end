namespace DentallApp.Features.ChatBot.Helpers;

public class AdaptiveCardsLoader
{
    private static readonly string s_basePath;

    static AdaptiveCardsLoader()
    {
        // This path is used by the chatbot test project.
        s_basePath = Path.Combine(AppContext.BaseDirectory, "AdaptiveCards");
        if (!Directory.Exists(s_basePath))
        {
            // This path is used by the Host Application.
            var path2 = "plugins/DentallApp.ChatBot/AdaptiveCards";
            s_basePath = Path.Combine(AppContext.BaseDirectory, path2);
        }
    }

    public static Task<string> LoadDentalServiceCardAsync()
    {
        return File.ReadAllTextAsync(Path.Combine(s_basePath, "DentalServiceCard.json"));
    }

    public static Task<string> LoadDentistCardAsync()
    {
        return File.ReadAllTextAsync(Path.Combine(s_basePath, "DentistCard.json"));
    }

    public static Task<string> LoadOfficeCardAsync()
    {
        return File.ReadAllTextAsync(Path.Combine(s_basePath, "OfficeCard.json"));
    }

    public static Task<string> LoadPatientCardAsync()
    {
        return File.ReadAllTextAsync(Path.Combine(s_basePath, "PatientCard.json"));
    }

    public static Task<string> LoadAppointmentDateCardAsync()
    {
        return File.ReadAllTextAsync(Path.Combine(s_basePath, "AppointmentDateCard.json"));
    }
}
