﻿namespace DentallApp.DataAccess.UnitOfWork;

public partial class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}
