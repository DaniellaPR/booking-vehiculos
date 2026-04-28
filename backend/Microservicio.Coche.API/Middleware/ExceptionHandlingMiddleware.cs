using System.Net;
using System.Text.Json;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.Exceptions;

namespace Microservicios.Coche.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await Write(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (ValidationException ex)
        {
            await Write(context, HttpStatusCode.BadRequest, ex.Message, ex.Errors.ToList());
        }
        catch (BusinessException ex)
        {
            await Write(context, HttpStatusCode.Conflict, ex.Message);
        }
        catch (UnauthorizedBusinessException ex)
        {
            await Write(context, HttpStatusCode.Unauthorized, ex.Message);
        }
        catch (Exception ex)
        {
            // Desenvuelve AggregateException
            var inner = ex is AggregateException agg ? agg.InnerException ?? ex : ex;
            _logger.LogError(inner, "Tipo real: {tipo}", inner.GetType().FullName);

            if (inner is NotFoundException nfe)
                await Write(context, HttpStatusCode.NotFound, nfe.Message);
            else if (inner is ValidationException ve)
                await Write(context, HttpStatusCode.BadRequest, ve.Message, ve.Errors.ToList());
            else if (inner is BusinessException be)
                await Write(context, HttpStatusCode.Conflict, be.Message);
            else
                await Write(context, HttpStatusCode.InternalServerError, "Error interno del servidor");
        }
    }

    private static async Task Write(HttpContext ctx, HttpStatusCode code, string message, List<string>? errors = null)
    {
        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode = (int)code;
        var body = JsonSerializer.Serialize(
            ApiResponse<object>.Fail(message, errors),
            new JsonSerializerOptions { PropertyNamingPolicy = null }
        );
        await ctx.Response.WriteAsync(body);
    }
}