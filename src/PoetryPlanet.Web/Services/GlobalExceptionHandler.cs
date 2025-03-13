using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PoetryPlanet.Web.Services;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "服务错误",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "由于系统发生错误，请稍后刷新页面重试，给您带来的不便深表歉意，紧急情况请联系管理员。"
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        httpContext.Response.Redirect(Consts.RouterError);
        return await Task.FromResult(true);
    }
}