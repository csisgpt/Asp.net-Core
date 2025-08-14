using InsuranceSuite.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InsuranceSuite.Api.Middleware;

public static class ExceptionHandlingMiddleware
{
    public static void UseExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var status = exception switch
                {
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    ConflictException => (int)HttpStatusCode.Conflict,
                    FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var problem = new ProblemDetails
                {
                    Status = status,
                    Title = exception?.GetType().Name,
                    Detail = exception?.Message
                };

                context.Response.StatusCode = status;
                context.Response.ContentType = "application/problem+json";
                await context.Response.WriteAsJsonAsync(problem);
            });
        });
    }
}
