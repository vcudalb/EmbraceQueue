using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace EmbraceQueue.API.SwaggerConfigurations
{
    /// <summary>
    /// An operation filter removing version from parameters
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RemoveVersionOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Applies specific filter to remove version from parameters
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");

            if (versionParameter != null)
            {
                operation.Parameters.Remove(versionParameter);
            }
        }
    }
}
