using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Middlewares
{

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var d in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(d.GroupName, new OpenApiInfo
                {
                    Title = "My API",
                    Version = d.ApiVersion.ToString()
                });
            }
        }
    }
}
