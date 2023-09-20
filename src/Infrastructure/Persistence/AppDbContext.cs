namespace DentallApp.Infrastructure.Persistence;

public partial class AppDbContext : CustomDbContext
{
    private readonly IWebHostEnvironment _env;

    public AppDbContext(IWebHostEnvironment env, DbContextOptions<AppDbContext> options) : base(options) => _env = env;

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
        modelBuilder.AddEntity<GeneralTreatment>()
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
                    .AddEntity<HolidayOffice>();

        AddSqlFunctions(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Create seed data.
        modelBuilder.CreateDefaultRoles()
                    .CreateDefaultKinships()
                    .CreateDefaultAppointmentStatus()
                    .CreateDefaultWeekDays()
                    .CreateDefaultGenders();

        if (_env.IsDevelopment())
        {
            modelBuilder.CreateDefaultGeneralTreatments()
                        .CreateDefaultSpecificTreatments()
                        .CreateDefaultOffices()
                        .CreateDefaultOfficeSchedules()
                        .CreateDefaultEmployeeSchedules()
                        .CreateDefaultUserAccounts();
        }
    }
}