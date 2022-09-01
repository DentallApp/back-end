namespace DentallApp.Extensions;

public static class SoftDeleteDtoExtensions
{
    public static bool IsActive(this ISoftDeleteDto dto)   => !dto.IsDeleted;
    public static bool IsInactive(this ISoftDeleteDto dto) => dto.IsDeleted;
}
