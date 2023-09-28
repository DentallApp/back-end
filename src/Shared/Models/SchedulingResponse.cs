namespace DentallApp.Shared.Models;

/// <summary>
/// Represents the response to appointment scheduling.
/// </summary>
public class SchedulingResponse
{
    /// <summary>
    /// Display text for the choice.
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// Represents a value which will be collected as input if the choice is selected.
    /// </summary>
    public string Value { get; init; }
}
