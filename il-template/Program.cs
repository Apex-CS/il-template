using FastEndpoints;
using FastEndpoints.Swagger;

using il_template.Data;
using il_template.Shared;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

using NSwag;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContextPool<ApplicationContext>((_, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddFastEndpoints();

builder.Services.SwaggerDocument(options =>
{
    options.EnableJWTBearerAuth = false;
    options.DocumentSettings = s =>
    {
        s.Title = "Fast API";
        s.Version = "v1";
        s.AddAuth("Bearer",
            new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            });
    };
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");

app.UseFastEndpoints(config => config.Endpoints.Configurator = x =>
{
    x.Options(b => b.AddEndpointFilter<UserDataFilter>());
}).UseSwaggerGen();

app.Run();