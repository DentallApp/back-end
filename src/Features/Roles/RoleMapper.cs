namespace DentallApp.Features.Roles;

public static class RoleMapper
{
    [Decompile]
    public static RoleGetDto MapToRoleGetDto(this Role role)
        => new()
        {
            Id = role.Id,
            Name = role.Name
        };
}
