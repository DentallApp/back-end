namespace DentallApp.DataAccess;

public partial class AppDbContext : CustomDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
                // See https://github.com/DentallApp/back-end/issues/25.
                warnings.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
        optionsBuilder.AddDelegateDecompiler();
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

        modelBuilder.ApplyConfiguration(new GeneralTreatmentConfiguration())
                    .ApplyConfiguration(new SpecificTreatmentConfiguration())
                    .ApplyConfiguration(new RoleConfiguration())
                    .ApplyConfiguration(new GenderConfiguration())
                    .ApplyConfiguration(new KinshipConfiguration())
                    .ApplyConfiguration(new DependentConfiguration())
                    .ApplyConfiguration(new OfficeConfiguration())
                    .ApplyConfiguration(new EmployeeConfiguration())
                    .ApplyConfiguration(new AppointmentStatusConfiguration())
                    .ApplyConfiguration(new WeekDayConfiguration())
                    .ApplyConfiguration(new EmployeeScheduleConfiguration())
                    .ApplyConfiguration(new OfficeScheduleConfiguration())
                    .ApplyConfiguration(new PublicHolidayConfiguration());

        modelBuilder.CreateDefaultUserAccounts();
    }
}