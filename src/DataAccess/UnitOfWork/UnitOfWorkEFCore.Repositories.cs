﻿namespace DentallApp.DataAccess.UnitOfWork;

public partial class UnitOfWorkEFCore
{
    private IUserRepository _userRepository;
    public IUserRepository UserRepository 
        => _userRepository ??= new UserRepository(_context);

    private IUserRoleRepository _userRoleRepository;
    public IUserRoleRepository UserRoleRepository 
        => _userRoleRepository ??= new UserRoleRepository(_context);

    private IHolidayOfficeRepository _holidayOfficeRepository;
    public IHolidayOfficeRepository HolidayOfficeRepository
        => _holidayOfficeRepository ??= new HolidayOfficeRepository(_context);

    private IEmployeeSpecialtyRepository _employeeSpecialtyRepository;
    public IEmployeeSpecialtyRepository EmployeeSpecialtyRepository
        => _employeeSpecialtyRepository ??= new EmployeeSpecialtyRepository(_context);
}
