namespace DentallApp.Features.Roles;

public class RolesId
{
    public const int Unverified      = 1;
    public const int BasicUser       = 2;
    public const int Secretary       = 3;
    public const int Dentist         = 4;
    public const int Admin           = 5;
    public const int Superadmin      = 6;
}

public class RolesName
{
    public const string Unverified        = "Sin Verificar";
    public const string BasicUser         = "Usuario basico";
    public const string Secretary         = "Secretaria";
    public const string Dentist           = "Odontologo";
    public const string Admin             = "Administrador";
    public const string Superadmin        = "Superadministrador";
}

public class NumberRoles
{
    public const int MinRole = 1;
    public const int MaxRole = 3;
}
