﻿namespace DentallApp.Features.PersonalInformation.Genders;

[Route("gender")]
[ApiController]
public class GenderController : ControllerBase
{
    private readonly DbContext _context;

    public GenderController(DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<GenderGetDto>> Get()
        => await _context.Set<Gender>()
                         .Select(gender => gender.MapToGenderGetDto())
                         .ToListAsync();
}
