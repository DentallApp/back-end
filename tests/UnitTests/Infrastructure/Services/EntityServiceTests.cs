namespace UnitTests.Infrastructure.Services;

public class EntityServiceTests
{
    private IRepository<UserRole> _repository;
    private IEntityService<UserRole> _userRoleService;

    [SetUp]
    public void TestInitialize()
    {
        _repository = Mock.Create<IRepository<UserRole>>();
        _userRoleService = new EntityService<UserRole>(_repository);
    }

    [Test]
    public void Update_WhenNumberOfElementsAreEqual_ShouldMergeSequenceOfElements()
    {
        // Arrange
        const int count  = 2;
        const int userId = 1;
        // These are the current roles of an employee.
        var currentUserRoles = new List<UserRole>()
        {
            new() { Id = 2, UserId = userId, RoleId = (int)Role.Predefined.Admin },
            new() { Id = 1, UserId = userId, RoleId = (int)Role.Predefined.Secretary }
        };
        // These are the role IDs obtained from the web client.
        // Duplicate roles must be removed.
        var rolesId = new List<int>() 
        { 
            (int)Role.Predefined.Admin, 
            (int)Role.Predefined.Dentist, 
            (int)Role.Predefined.Dentist 
        };

        // Act
        // Update employee roles.
        _userRoleService.Update(userId, ref currentUserRoles, ref rolesId);

        // Asserts
        currentUserRoles.Should().HaveCount(count);
        rolesId.Should().HaveCount(count);
        currentUserRoles[0].RoleId.Should().Be((int)Role.Predefined.Dentist);
        currentUserRoles[1].RoleId.Should().Be((int)Role.Predefined.Admin);
        rolesId[0].Should().Be((int)Role.Predefined.Dentist);
        rolesId[1].Should().Be((int)Role.Predefined.Admin);
    }

    [Test]
    public void Update_WhenIdentifiersDoNotContainsTheSecondaryKey_ShouldRemoveCurrentEntity()
    {
        // Arrange
        const int userId = 1;
        // These are the current roles of an employee.
        var currentUserRoles = new List<UserRole>()
        {
            new() { Id = 1, UserId = userId, RoleId = (int)Role.Predefined.Secretary },
            new() { Id = 3, UserId = userId, RoleId = (int)Role.Predefined.Dentist },
            new() { Id = 2, UserId = userId, RoleId = (int)Role.Predefined.Admin }
        };
        // Create a temporary collection because the original collection cannot be modified while iterating.
        var data = new List<UserRole>(currentUserRoles);
        // These are the role IDs obtained from the web client.
        var rolesId = new List<int>()
        {
            (int)Role.Predefined.Dentist,
            (int)Role.Predefined.Secretary
        };
        Mock.Arrange(() => _repository.Remove(Arg.IsAny<UserRole>()))
            .DoInstead((UserRole entity) =>
            {
                var index = currentUserRoles.FindIndex(userRole => userRole.Id == entity.Id);
                currentUserRoles.RemoveAt(index);
            });
        
        // Act
        // Update employee roles.
        _userRoleService.Update(userId, ref data, ref rolesId);

        // Asserts
        currentUserRoles.Should().HaveCount(2);
        currentUserRoles[0].RoleId.Should().Be((int)Role.Predefined.Secretary);
        currentUserRoles[1].RoleId.Should().Be((int)Role.Predefined.Dentist);
    }


    [Test]
    public void Update_WhenSourceDoNotContainsTheIdentifier_ShouldAddCurrentEntity()
    {
        // Arrange
        const int userId = 1;
        // These are the current roles of an employee.
        var currentUserRoles = new List<UserRole>()
        {
            new() { Id = 1, UserId = userId, RoleId = (int)Role.Predefined.Secretary }
        };
        // Create a temporary collection because the original collection cannot be modified while iterating.
        var data = new List<UserRole>(currentUserRoles);
        // These are the role IDs obtained from the web client.
        var rolesId = new List<int>()
        {
            (int)Role.Predefined.Admin,
            (int)Role.Predefined.Dentist,
            (int)Role.Predefined.Secretary
        };
        Mock.Arrange(() => _repository.Add(Arg.IsAny<UserRole>()))
            .DoInstead((UserRole entity) => currentUserRoles.Add(entity));

        // Act
        // Update employee roles.
        _userRoleService.Update(userId, ref data, ref rolesId);

        // Asserts
        currentUserRoles.Should().HaveCount(3);
        currentUserRoles[0].RoleId.Should().Be((int)Role.Predefined.Secretary);
        currentUserRoles[1].RoleId.Should().Be((int)Role.Predefined.Dentist);
        currentUserRoles[2].RoleId.Should().Be((int)Role.Predefined.Admin);
    }
}
