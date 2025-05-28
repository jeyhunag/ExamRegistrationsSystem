using Microsoft.AspNetCore.Diagnostics;
using ExamRegistrationSystem.Dto;

namespace ExamRegistrationSystem.Exceptions;

public static class CustomExceptionHandler
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                var response = new ResponseDto
                {
                    IsSuccess = false,
                    Message = exception is CustomExceptionHandler
                        ? exception.Message
                        : "Gözlənilməz xəta baş verdi."
                };

                await context.Response.WriteAsJsonAsync(response);
            });
        });
    }
}
