using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace OnlineCoursesPlatform.API.Common
{
    public class CustomProblemDetailsFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;

        public CustomProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
        {
            _options = options.Value;
        }

        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            statusCode ??= 500;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title ?? GetDefaultTitleForStatusCode(statusCode.Value),
                Type = type,
                Detail = detail,
                Instance = instance ?? httpContext?.Request.Path
            };

            AddCommonExtensions(problemDetails, httpContext!);

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            statusCode ??= 400;

            var validationProblemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Title = title ?? GetDefaultTitleForStatusCode(statusCode.Value),
                Detail = detail,
                Instance = instance ?? httpContext?.Request.Path
            };

            AddCommonExtensions(validationProblemDetails, httpContext!);

            return validationProblemDetails;
        }

        private static void AddCommonExtensions(ProblemDetails problemDetails, HttpContext httpContext)
        {
            problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier;
            problemDetails.Extensions["timestamp"] = DateTime.UtcNow;
            problemDetails.Extensions["method"] = httpContext.Request?.Method;
        }

        private string GetDefaultTitleForStatusCode(int statusCode) => statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            409 => "Conflict",
            500 => "Internal Server Error",
            _ => "Error"
        };
    }
}
