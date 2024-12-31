using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HQ.Api.Filters;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParameters = context.MethodInfo
            .GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(IFormFileCollection));

        foreach (var parameter in fileParameters)
        {
            var schema = new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            };

            var parameterToRemove = operation.Parameters.FirstOrDefault(p => p.Name == parameter.Name);
            if (parameterToRemove != null)
            {
                operation.Parameters.Remove(parameterToRemove);
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = parameter.Name,
                In = ParameterLocation.Query,
                Schema = schema,
                Required = true
            });
        }
    }
}