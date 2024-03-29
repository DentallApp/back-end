﻿namespace DentallApp.Shared.Reasons;

public readonly ref struct ShowScheduleToUserSuccess
{
    public string Message { get; }
    public ShowScheduleToUserSuccess(string dentistSchedule)
        => Message = string.Format(Messages.ShowScheduleToUser, dentistSchedule ?? string.Empty);
}
