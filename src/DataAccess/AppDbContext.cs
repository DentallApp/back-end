namespace DentallApp.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<GeneralTreatment> GeneralTreatments { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}