namespace DentallApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddServices()
                .AddRepositories()
                .AddHelpers();

        new EnvLoader().Load();
        var settings = new EnvBinder().Bind<AppSettings>();

        services.AddSingleton(settings);

        services.AddHttpClient()
                .AddControllers(options => options.SuppressAsyncSuffixInActionNames = false)
                .AddCustomInvalidModelStateResponse()
                .AddNewtonsoftJson();


        services.AddDbContext(settings);
        services.AddSendGrid(options => options.ApiKey = settings.SendGridApiKey);
        services.AddSwagger();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AccessTokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddAuthorization();
        services.AddBotServices();
        services.AddHostedService<TimedHostedService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
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
           .UseSwagger()
           .UseSwaggerUI(options =>
            {
               options.SwaggerEndpoint("v1/swagger.json", "DentallApi V1");
            })
           .UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            });
    }
}
