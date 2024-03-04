namespace DentallApp.HostApplication.Extensions;

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

    /// <inheritdoc />
    /// <remarks>
    /// This custom class was created so that when DirectLine channel sends a request to the bot, it does not throw an exception:
    /// <see cref="SecurityTokenSignatureKeyNotFoundException" />.
    /// <para>See issue <see href="https://github.com/DentallApp/back-end/issues/135">#135</see>.</para>
    /// </remarks>
    private class CustomJwtBearerHandler : JwtBearerHandler
    {
        /// <summary>
        /// Path associated to the bot.
        /// </summary>
        private const string BotPath = "/messages";

        public CustomJwtBearerHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder)
            : base(options, logger, encoder) { }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
            => Context.Request.Path.StartsWithSegments(BotPath) ? 
                AuthenticateResult.NoResult() : 
                await base.HandleAuthenticateAsync();
    }
}