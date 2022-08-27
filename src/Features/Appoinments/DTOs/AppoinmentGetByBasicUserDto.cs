namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentGetByBasicUserDto : AppoinmentGetDto
{
    /// <summary>
    /// Obtiene o establece el nombre del parentesco.
    /// Por ejemplo: Hijo/a, Esposo/a, y Otros.
    /// </summary>
    public string KinshipName { get; set; }
}
