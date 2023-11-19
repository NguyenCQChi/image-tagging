using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace authentication;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        this._provider = provider;
    }
    
    public void Configure(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Jwt Authorization Header using the bearer scheme. \r\n\r\n" +
                          "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
                          "Example: \"Bearer 12345abcdef\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
        options.EnableAnnotations();
        foreach(var desc in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(desc.GroupName, new OpenApiInfo
            {
                Version = desc.ApiVersion.ToString(),
                Title = $"Authentication {desc.ApiVersion}",
                Description = "Authentication Microservice",
                TermsOfService = new Uri("https://docs.oracle.com/cd/E19957-01/819-7168/gddvy/index.html"),
                Contact = new OpenApiContact
                {
                    Name = "Manjot Singh Randhawa",
                    Email = "mrandhawa40@my.bcit.ca"
                },
                License = new OpenApiLicense
                {
                    Name = "GNU General Public License",
                    Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html")
                },
            });
        }
    }
}