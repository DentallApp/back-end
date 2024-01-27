namespace DentallApp.Shared.Reasons;

public class ShowScheduleToUserSuccess
{
    public string Message { get; }
    public ShowScheduleToUserSuccess(string dentistSchedule)
        => Message = string.Format(Messages.ShowScheduleToUser, dentistSchedule ?? string.Empty);
}
