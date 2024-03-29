﻿namespace DentallApp.Shared.Interfaces.Persistence.Repositories;

public interface IUserRepository
{
    Task<User> GetFullUserProfileAsync(string userName);
    Task<bool> UserExistsAsync(string userName);
}
