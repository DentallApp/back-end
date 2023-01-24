// ASPNETCORE_ENVIRONMENT must be set from the .env file before initializing a new instance of WebApplicationBuilder. 
// If the environment isn't set, it defaults to Production, which disables most debugging features.
var envVars  = new EnvLoader().Load();
var settings = new EnvBinder(envVars).Bind<AppSettings>();
var builder  = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServices()
                .AddRepositories()
                .AddHelpers();

builder.Services.AddSingleton(settings)
                .AddSingleton<IDbConnector>(new MariaDbConnector(settings.ConnectionString));

builder.Services.AddHttpClient()
                .AddControllers(options => options.SuppressAsyncSuffixInActionNames = false)
                .AddCustomInvalidModelStateResponse()
                .AddNewtonsoftJson();


builder.Services.AddDbContext(settings);
builder.Services.AddSendGrid(options => options.ApiKey = settings.SendGridApiKey);
builder.Services.AddSwagger();
builder.Services.AddAuthenticationJwtBearer(settings);
builder.Services.AddBotServices(builder.Configuration);
builder.Services.AddQuartzJobs(settings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "DentallApi V1"));
    IdentityModelEventSource.ShowPII = true;
}

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