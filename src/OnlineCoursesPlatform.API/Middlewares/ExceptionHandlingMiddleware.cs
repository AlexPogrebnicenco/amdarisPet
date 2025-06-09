using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnlineCoursesPlatform.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, ProblemDetailsFactory problemDetailsFactory)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            var statusCode = ex switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                ValidationException => HttpStatusCode.BadRequest,
                ConflictException => HttpStatusCode.Conflict,
                UnauthorizedException => HttpStatusCode.Unauthorized,
                UnauthenticatedException => HttpStatusCode.Unauthorized,
                ForbiddenException => HttpStatusCode.Forbidden,
                BadRequestException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var problemDetails = problemDetailsFactory.CreateProblemDetails(
                context,
                (int)statusCode,
                detail: ex.Message
            );

            context.Response.StatusCode = problemDetails.Status ?? 500;
            context.Response.ContentType = "application/problem+json";

            var json = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(json);
        }
    }
}
