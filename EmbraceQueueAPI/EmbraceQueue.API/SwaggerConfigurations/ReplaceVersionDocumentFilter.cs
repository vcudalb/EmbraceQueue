using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace EmbraceQueue.API.SwaggerConfigurations
{
    /// <summary>
    /// A document filter replacing v{version:apiVersion} with the real version of the corresponding swagger doc
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ReplaceVersionDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Applies specific filter to change version parameter with current swagger doc version
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            OpenApiPaths openApiPaths = new OpenApiPaths();

            foreach (var path in swaggerDoc.Paths)
            {
                openApiPaths.Add(path.Key.Replace("v{version}", $"v{swaggerDoc.Info.Version}"), path.Value);
            }

            swaggerDoc.Paths = openApiPaths;
        }
    }
}
