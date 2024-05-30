using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Server;

[AttributeUsage(AttributeTargets.Parameter)]
public class MatrixPathAttribute : Attribute;

// This filter is registered with SwaggerGen and will update the parameter values 
public class MatrixPathParameterFilter : IParameterFilter
{
    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (context.ParameterInfo.GetCustomAttribute<MatrixPathAttribute>() is { })
        {
            parameter.In = ParameterLocation.Path;
            parameter.Style = ParameterStyle.Matrix;
            parameter.Explode = false;
            parameter.AllowEmptyValue = false;
            parameter.Schema = new OpenApiSchema
            {
                Type = "array",
                Items = new OpenApiSchema { Type = "string" }
            };
        }
    }
}
