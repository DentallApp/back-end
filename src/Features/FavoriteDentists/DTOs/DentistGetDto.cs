namespace DentallApp.Features.FavoriteDentists.DTOs;

public class DentistGetDto : FavoriteDentistDto
{
    public int DentistId { get; set; }

    /// <summary>
    /// Permite verificar si el odontólogo es el preferido por el usuario básico.
    /// </summary>
    public bool IsFavorite { get; set; }
}
