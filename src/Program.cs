var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServices()
                .AddRepositories()
                .AddHelpers();

// Sets environment variables from a .env file.
new EnvLoader().Load();
var settings = new EnvBinder().Bind<AppSettings>();

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
builder.Services.AddBotServices();
builder.Services.AddQuartzJobs(settings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "DentallApi V1"));
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