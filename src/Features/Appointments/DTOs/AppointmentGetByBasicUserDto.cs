﻿namespace DentallApp.Features.Appointments.DTOs;

public class AppointmentGetByBasicUserDto : AppointmentGetDto
{
    /// <summary>
    /// Obtiene o establece el nombre del parentesco.
    /// Por ejemplo: Hijo/a, Esposo/a, y Otros.
    /// </summary>
    public string KinshipName { get; set; }
    public string Status { get; set; }
    public string DentistName { get; set; }
    public string OfficeName { get; set; }
}