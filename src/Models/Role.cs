﻿namespace DentallApp.Models;

public class Role : ModelBase
{
    public string Name { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
