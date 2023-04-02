namespace DentallApp.DataAccess.EntityConfigurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasIndex(person => person.Names);
        builder.HasIndex(person => person.LastNames);
        builder.HasIndex(person => person.Document);
    }
}
