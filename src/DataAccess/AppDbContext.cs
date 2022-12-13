namespace DentallApp.DataAccess;

public partial class AppDbContext : CustomDbContext
{
    private readonly IWebHostEnvironment _env;
    public AppDbContext(IWebHostEnvironment env, DbContextOptions<AppDbContext> options) : base(options) => _env = env;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddDelegateDecompiler();

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
                    .AddEntity<OfficeSchedule>();

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
                    .ApplyConfiguration(new OfficeScheduleConfiguration());

        modelBuilder.CreateDefaultUserAccounts(_env);
    }
}