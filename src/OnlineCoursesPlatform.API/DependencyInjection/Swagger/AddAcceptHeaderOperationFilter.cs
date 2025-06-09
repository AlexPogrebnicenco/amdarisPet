using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineCoursesPlatform.API.Swagger
{
    public class AddAcceptHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }
}
