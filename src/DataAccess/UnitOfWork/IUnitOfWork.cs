namespace DentallApp.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    IAppDbContextTransaction BeginTransaction();
    Task<IAppDbContextTransaction> BeginTransactionAsync();
    Task<int> SaveChangesAsync();
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IPersonRepository PersonRepository { get; }
    IDependentRepository DependentRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IPublicHolidayRepository PublicHolidayRepository { get; }
    IHolidayOfficeRepository HolidayOfficeRepository { get; }
    IEmployeeSpecialtyRepository EmployeeSpecialtyRepository { get; }
}