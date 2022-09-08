namespace DentallApp.DataAccess;

public partial class AppDbContext
{
    public DateTime AddTime(DateTime dateTime, TimeSpan addTime) 
        => throw new InvalidOperationException();

    public DateTime ToDateTime(DateTime dateTime) 
        => throw new InvalidOperationException();

    public void AddSqlFunctions(ModelBuilder modelBuilder)
    {
        var addTimeMethodInfo = typeof(AppDbContext) // Your DB Context
            .GetRuntimeMethod(nameof(AppDbContext.AddTime), new[] { typeof(DateTime), typeof(TimeSpan) });

        modelBuilder.HasDbFunction(() => AddTime(default, default))
           .HasTranslation(args => new SqlFunctionExpression(
                   functionName: "ADDTIME",
                   arguments: new[] { args.ToArray()[0], args.ToArray()[1] },
                   nullable: false,
                   argumentsPropagateNullability: new[] { false, false },
                   type: typeof(DateTime),
                   typeMapping: null));

        modelBuilder.HasDbFunction(() => ToDateTime(default))
           .HasTranslation(args => new SqlFunctionExpression(
                   functionName: "CONVERT",
                   arguments: new[] { args.ToArray()[0], new SqlFragmentExpression("datetime") },
                   nullable: false,
                   argumentsPropagateNullability: new[] { false, false },
                   type: typeof(DateTime),
                   typeMapping: null));
    }
}
