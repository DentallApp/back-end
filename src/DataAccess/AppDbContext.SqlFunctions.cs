namespace DentallApp.DataAccess;

public partial class AppDbContext
{
    public DateTime AddTime(DateTime dateTime, TimeSpan addTime) 
        => throw new InvalidOperationException();

    public DateTime ToDateTime(DateTime dateTime) 
        => throw new InvalidOperationException();

    public int DateDiff(DateTime dateTime1, DateTime dateTime2)
        => throw new InvalidOperationException();

    public DateTime GetDate(DateTime? dateTime) 
        => throw new InvalidOperationException();

    public void AddSqlFunctions(ModelBuilder modelBuilder)
    {
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

        modelBuilder.HasDbFunction(() => DateDiff(default, default))
           .HasTranslation(args => new SqlFunctionExpression(
                   functionName: "DATEDIFF",
                   arguments: new[] { args.ToArray()[0], args.ToArray()[1] },
                   nullable: false,
                   argumentsPropagateNullability: new[] { false, false },
                   type: typeof(int),
                   typeMapping: null));

        modelBuilder.HasDbFunction(() => GetDate(default))
           .HasTranslation(args => new SqlFunctionExpression(
                   functionName: "DATE",
                   arguments: new[] { args.ToArray()[0] },
                   nullable: false,
                   argumentsPropagateNullability: new[] { false },
                   type: typeof(DateTime),
                   typeMapping: null));
    }
}
