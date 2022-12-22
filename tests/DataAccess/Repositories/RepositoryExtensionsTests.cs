namespace DentallApp.Tests.DataAccess.Repositories;

[TestClass]
public class RepositoryExtensionsTests
{
    private IRepository<UserRole> _repository;

    [TestInitialize]
    public void TestInitialize()
    {
        _repository = Mock.Create<IRepository<UserRole>>();
    }

    [TestMethod]
    public void UpdateEntities_WhenNumberOfElementsAreEqual_ShouldMergeSequenceOfElements()
    {
        const int count  = 2;
        const int userId = 1;
        // These are the current roles of an employee.
        var currentUserRoles = new List<UserRole>()
        {
            new() { Id = 2, UserId = userId, RoleId = RolesId.Admin },
            new() { Id = 1, UserId = userId, RoleId = RolesId.Secretary }
        };
        // These are the new role IDs obtained from the web client.
        // Duplicate roles must be removed.
        var newRolesId = new List<int>() 
        { 
            RolesId.Admin, 
            RolesId.Dentist, 
            RolesId.Dentist 
        };

        // Update employee roles.
        _repository.UpdateEntities(key: userId, source: ref currentUserRoles, identifiers: ref newRolesId);

        Assert.AreEqual(expected: count, actual: currentUserRoles.Count);
        Assert.AreEqual(expected: count, actual: newRolesId.Count);
        Assert.AreEqual(expected: RolesId.Dentist, actual: currentUserRoles[0].RoleId);
        Assert.AreEqual(expected: RolesId.Admin,   actual: currentUserRoles[1].RoleId);
        Assert.AreEqual(expected: RolesId.Dentist, actual: newRolesId[0]);
        Assert.AreEqual(expected: RolesId.Admin,   actual: newRolesId[1]);
    }

    [TestMethod]
    public void UpdateEntities_WhenNewEntriesDoNotContainsTheSecondaryForeignKey_ShouldDeleteCurrentEntity()
    {
        const int userId = 1;
        // These are the current roles of an employee.
        var currentUserRoles = new List<UserRole>()
        {
            new() { Id = 1, UserId = userId, RoleId = RolesId.Secretary },
            new() { Id = 3, UserId = userId, RoleId = RolesId.Dentist },
            new() { Id = 2, UserId = userId, RoleId = RolesId.Admin }
    };
        var data = new List<UserRole>(currentUserRoles);
        // These are the new role IDs obtained from the web client.
        var newRolesId = new List<int>()
        {
            RolesId.Dentist,
            RolesId.Secretary
        };
        Mock.Arrange(() => _repository.Delete(Arg.IsAny<UserRole>()))
            .DoInstead((UserRole entity) =>
            {
                var index = currentUserRoles.FindIndex(userRole => userRole.Id == entity.Id);
                currentUserRoles.RemoveAt(index);
            });
        
        // Update employee roles.
        _repository.UpdateEntities(key: userId, source: ref data, identifiers: ref newRolesId);

        Assert.AreEqual(expected: 2, actual: currentUserRoles.Count);
        Assert.AreEqual(expected: RolesId.Secretary, actual: currentUserRoles[0].RoleId);
        Assert.AreEqual(expected: RolesId.Dentist,   actual: currentUserRoles[1].RoleId);
    }


    [TestMethod]
    public void UpdateEntities_WhenSourceDoNotContainsTheNewId_ShouldInsertCurrentEntity()
    {
        const int userId = 1;
        // These are the current roles of an employee.
        var currentUserRoles = new List<UserRole>()
        {
            new() { Id = 1, UserId = userId, RoleId = RolesId.Secretary }
        };
        var data = new List<UserRole>(currentUserRoles);
        // These are the new role IDs obtained from the web client.
        var newRolesId = new List<int>()
        {
            RolesId.Admin,
            RolesId.Dentist,
            RolesId.Secretary
        };
        Mock.Arrange(() => _repository.Insert(Arg.IsAny<UserRole>()))
            .DoInstead((UserRole entity) => currentUserRoles.Add(entity));

        // Update employee roles.
        _repository.UpdateEntities(key: userId, source: ref data, identifiers: ref newRolesId);

        Assert.AreEqual(expected: 3, actual: currentUserRoles.Count);
        Assert.AreEqual(expected: RolesId.Secretary, actual: currentUserRoles[0].RoleId);
        Assert.AreEqual(expected: RolesId.Dentist,   actual: currentUserRoles[1].RoleId);
        Assert.AreEqual(expected: RolesId.Admin,     actual: currentUserRoles[2].RoleId);
    }
}
