// ASPNETCORE_ENVIRONMENT must be set from the .env file before initializing a new instance of WebApplicationBuilder. 
// If the environment isn't set, it defaults to Production, which disables most debugging features.
var envVars     = new EnvLoader().Load();
var appSettings = new EnvBinder(envVars).Bind<AppSettings>();
var builder     = WebApplication.CreateBuilder(args);

// Add services to the container.

ISqlCollection sqlCollection = new YeSqlLoader().LoadFromDefaultDirectory();
builder.Services.AddSingleton(sqlCollection);
builder.Configuration.AddEnvironmentVariables();
PluginStartup.Configure(builder);
builder.Services
    .AddInfrastructureServices()
    .AddFeatureServices()
    .RegisterAutoDependencies();

var databaseSettings = new EnvBinder(envVars).Bind<DatabaseSettings>();
builder.Services
    .AddSingleton(appSettings)
    .AddSingleton<IDbConnectionFactory>(new MariaDbConnectionFactory(databaseSettings.DbConnectionString));

builder.Services
    .AddScoped<IDbConnection>(serviceProvider => new MySqlConnection(databaseSettings.DbConnectionString));

builder.Services
    .AddHttpClient()
    .AddControllers(options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
        options.Filters.Add<TranslateResultToActionResultAttribute>();
    })
    .AddJsonOptions(jsonOpts =>
    {
        jsonOpts.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
    })
    .AddCustomInvalidModelStateResponse()
    .AddApplicationParts();

builder.Services.AddDbContext(databaseSettings);
builder.Services.AddSwagger();
builder.Services.AddAuthenticationJwtBearer(appSettings);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "DentallApi V1"));
    IdentityModelEventSource.ShowPII = true;
}

app.UseExceptionHandling();
app.UseRequestLocalization(appSettings.Language);

app.UseDefaultFiles()
   .UseStaticFiles()
   .UseWebSockets()
   .UsePathBase(new PathString("/api"))
   .UseRouting()
   .UseCors(options =>
    {
       options.AllowAnyOrigin();
       options.AllowAnyMethod();
       options.AllowAnyHeader();
    })
   .UseAuthentication()
   .UseAuthorization()
   .UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();