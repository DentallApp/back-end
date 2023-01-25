namespace DentallApp.Features.FavoriteDentists;

[AuthorizeByRole(RolesName.BasicUser)]
[Route("favorite-dentist")]
[ApiController]
public class FavoriteDentistController : ControllerBase
{
    private readonly IFavoriteDentistService _favoriteDentistService;
    private readonly IFavoriteDentistRepository _favoriteDentistRepository;

    public FavoriteDentistController(IFavoriteDentistService favoriteDentistService, 
                                     IFavoriteDentistRepository favoriteDentistRepository)
    {
        _favoriteDentistService = favoriteDentistService;
        _favoriteDentistRepository = favoriteDentistRepository;
    }

    /// <summary>
    /// Crea un nuevo odontólogo favorito para el usuario básico.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Response>> Post([FromBody]FavoriteDentistInsertDto favoriteDentistInsertDto)
    {
        var response = await _favoriteDentistService.CreateFavoriteDentistAsync(User.GetUserId(), favoriteDentistInsertDto);
        return response.Success ? CreatedAtAction(nameof(Post), response) : BadRequest(response);
    }

    /// <summary>
    /// Obtiene los odontólogos favoritos del usuario básico y además, 
    /// incluye en el resultado a los odontólogos que no son preferidos por el usuario básico.
    /// </summary>
    [HttpGet("dentist")]
    public async Task<IEnumerable<DentistGetDto>> GetListOfDentists()
        => await _favoriteDentistRepository.GetListOfDentistsAsync(User.GetUserId());

    /// <summary>
    /// Obtiene únicamente los odontólogos favoritos del usuario básico.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<FavoriteDentistGetDto>> GetFavoriteDentists()
        => await _favoriteDentistRepository.GetFavoriteDentistsAsync(User.GetUserId());

    /// <summary>
    /// Elimina un odontólogo favorito de un usuario básico.
    /// </summary>
    [HttpDelete("{favoriteDentistId}")]
    public async Task<ActionResult<Response>> DeleteByFavoriteDentistId(int favoriteDentistId)
    {
        var response = await _favoriteDentistService.RemoveByFavoriteDentistIdAsync(User.GetUserId(), favoriteDentistId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Elimina un odontólogo favorito de un usuario básico.
    /// </summary>
    [HttpDelete("dentist/{dentistId}")]
    public async Task<ActionResult<Response>> DeleteByUserIdAndDentistId(int dentistId)
    {
        var response = await _favoriteDentistService.RemoveByUserIdAndDentistIdAsync(User.GetUserId(), dentistId);
        return response.Success ? Ok(response) : NotFound(response);
    }
}
