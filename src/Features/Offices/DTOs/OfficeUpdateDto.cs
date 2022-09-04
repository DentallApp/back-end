namespace DentallApp.Features.Offices.DTOs;

public class OfficeUpdateDto : OfficeInsertDto, ISoftDeleteDto
{
    /// <summary>
    /// Un valor que indica sí el consultorio debe ser eliminado temporalmente.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Un valor que indica sí la casilla de verificación (checkbox) está marcada.
    /// Con esta propiedad se puede verificar sí las cuentas de los empleados deben ser eliminadas temporalmente cuando el consultorio queda inactivo.
    /// </summary>
    public bool IsCheckboxTicked { get; set; }

    /// <summary>
    /// Un valor que indica sí la casilla de verificación (checkbox) está desmarcada.
    /// </summary>
    [JsonIgnore]
    public bool IsCheckboxUnticked => !IsCheckboxTicked;
}
