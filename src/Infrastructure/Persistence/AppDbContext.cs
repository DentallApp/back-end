namespace DentallApp.Infrastructure.Persistence;

public partial class AppDbContext(
    IEnumerable<IEntityTypeConfigurator> entityTypeConfigurators,
    IWebHostEnvironment env,
    DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
                // See https://github.com/DentallApp/back-end/issues/25.
                warnings.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
        optionsBuilder.AddDelegateDecompiler();
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Execute the entity configurations that expose the plugins.
        foreach (IEntityTypeConfigurator entityTypeConfigurator in entityTypeConfigurators)
        {
            entityTypeConfigurator.ConfigureEntities(modelBuilder);
        }

        modelBuilder
            .AddEntity<GeneralTreatment>()
            .AddEntity<SpecificTreatment>()
            .AddEntity<Person>()
            .AddEntity<Gender>()
            .AddEntity<User>()
            .AddEntity<UserRole>()
            .AddEntity<Role>()
            .AddEntity<Dependent>()
            .AddEntity<Kinship>()
            .AddEntity<Employee>()
            .AddEntity<Office>()
            .AddEntity<Appointment>()
            .AddEntity<AppointmentStatus>()
            .AddEntity<EmployeeSchedule>()
            .AddEntity<WeekDay>()
            .AddEntity<FavoriteDentist>()
            .AddEntity<OfficeSchedule>()
            .AddEntity<PublicHoliday>()
            .AddEntity<OfficeHoliday>();

        AddDbFunctions(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Create seed data.
        modelBuilder
            .CreateDefaultRoles()
            .CreateDefaultKinships()
            .CreateDefaultAppointmentStatus()
            .CreateDefaultWeekDays()
            .CreateDefaultGenders();

        if (env.IsDevelopment())
        {
            modelBuilder
                .CreateDefaultGeneralTreatments()
                .CreateDefaultSpecificTreatments()
                .CreateDefaultOffices()
                .CreateDefaultOfficeSchedules()
                .CreateDefaultEmployeeSchedules()
                .CreateDefaultUserAccounts();
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetCurrentDateToEachEntity();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetCurrentDateToEachEntity()
    {
        foreach (var entityEntry in GetEntityEntries())
        {
            if (entityEntry.Entity is IAuditableEntity auditableEntity)
            {
                auditableEntity.UpdatedAt = DateTime.Now;
                if (entityEntry.State == EntityState.Added)
                {
                    auditableEntity.CreatedAt = DateTime.Now;
                }
            }
        }
    }

    private IEnumerable<EntityEntry> GetEntityEntries()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => 
              e.Entity is IAuditableEntity && 
             (e.State == EntityState.Added || e.State == EntityState.Modified));

        return entries;
    }
}
