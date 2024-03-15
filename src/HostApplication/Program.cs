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
    .AddSingleton(appSettings)
    .AddInfrastructureServices()
    .AddDbContext(hostAppName: typeof(PluginStartup).Namespace)
    .AddCoreServices()
    .RegisterAutoDependencies();

builder.Services
    .AddHttpClient()
    .AddControllersAsService()
    .AddHttpContextAccessor();

builder.Services.AddSwagger();
builder.Services.AddAuthenticationJwtBearer(appSettings);
builder.Services.AddValidators();

builder.Services
    .AddExceptionHandler<ReferenceConstraintExceptionHandler>()
    .AddExceptionHandler<UniqueConstraintExceptionHandler>()
    .AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "DentallApi V1"));
    IdentityModelEventSource.ShowPII = true;
}
else
{
    app.UseExceptionHandler("/");
}

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