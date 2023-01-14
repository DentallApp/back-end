namespace DentallApp.Extensions;

public static class AuthenticationJwtBearer
{
    public static IServiceCollection AddAuthenticationJwtBearer(this IServiceCollection services, AppSettings settings)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddCustomJwtBearer(options =>
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
        return services;
    }

    private static AuthenticationBuilder AddCustomJwtBearer(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
        => builder.AddScheme<JwtBearerOptions, CustomJwtBearerHandler>(JwtBearerDefaults.AuthenticationScheme, displayName: null, configureOptions);

    private class CustomJwtBearerHandler : JwtBearerHandler
    {
        public CustomJwtBearerHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        // This class was created so that when DirectLine channel sends a request to the bot, it does not throw an exception:
        // SecurityTokenSignatureKeyNotFoundException.
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
            => Context.Request.Path.StartsWithSegments("/messages") ? 
                AuthenticateResult.NoResult() : 
                await base.HandleAuthenticateAsync();
    }
}