namespace DentallApp.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task Rollback();
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IPersonRepository PersonRepository { get; }
}