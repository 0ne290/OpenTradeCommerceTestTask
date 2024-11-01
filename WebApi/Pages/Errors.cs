using Microsoft.AspNetCore.Http.Extensions;

namespace WebApi.Pages;

public static class Errors
{
    public static async Task Return500(HttpContext context)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(
            $"{{ \"error\": {{ \"message\": \"Processing of the request failed. Please provide technical support with the URL, time and request ID\", \"url\": \"{context.Request.GetEncodedUrl()}\", \"time\": \"{DateTime.Now}\", \"requestId\": \"{context.TraceIdentifier}\", \"technicalSupportContact\": \"\" }} }}");
    }
}