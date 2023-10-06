namespace DentallApp.Infrastructure.Persistence;

public partial class AppDbContext
{
    public void AddDbFunctions(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDbFunction(() => DBFunctions.AddTime(default, default))
            .HasTranslation(args => new SqlFunctionExpression(
                functionName: "ADDTIME",
                arguments: new[] { args.ToArray()[0], args.ToArray()[1] },
                nullable: false,
                argumentsPropagateNullability: new[] { false, false },
                type: typeof(DateTime),
                typeMapping: null));

        modelBuilder
            .HasDbFunction(() => DBFunctions.ToDateTime(default))
            .HasTranslation(args => new SqlFunctionExpression(
                functionName: "CONVERT",
                arguments: new[] { args.ToArray()[0], new SqlFragmentExpression("datetime") },
                nullable: false,
                argumentsPropagateNullability: new[] { false, false },
                type: typeof(DateTime),
                typeMapping: null));

        modelBuilder
            .HasDbFunction(() => DBFunctions.DateDiff(default, default))
            .HasTranslation(args => new SqlFunctionExpression(
                functionName: "DATEDIFF",
                arguments: new[] { args.ToArray()[0], args.ToArray()[1] },
                nullable: false,
                argumentsPropagateNullability: new[] { false, false },
                type: typeof(int),
                typeMapping: null));

        modelBuilder
            .HasDbFunction(() => DBFunctions.GetDate(default))
            .HasTranslation(args => new SqlFunctionExpression(
                functionName: "DATE",
                arguments: new[] { args.ToArray()[0] },
                nullable: false,
                argumentsPropagateNullability: new[] { false },
                type: typeof(DateTime),
                typeMapping: null));
    }
}
