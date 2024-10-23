using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RPSSL.Api.Common.JsonSerialization;
using RPSSL.Api.Contracts.Error;

namespace RPSSL.Api.Common.Errors;

public class ErrorResponseFactory : IErrorResponseFactory
{
    /// <summary>
    /// Creates an ObjectResult from a list of errors.
    /// The status code is taken from the highest error group (e.g. 5xx from [4xx, 5xx]) 
    /// and from there the lowest error code (e.g. 501 from [501, 502, 504]).
    /// </summary>
    /// <param name="errorList"></param>
    /// <returns></returns>
    public ActionResult From(ApiErrorList errorList)
    {
        if (TryGetRedirectResponse(errorList, out var redirectUrl))
            return new RedirectResult(redirectUrl);

        var statusCode = GetLowestErrorCodeInHighestGroup(errorList);
        var response = new ErrorResponse(errorList);
        return new ObjectResult(response) { StatusCode = statusCode };
    }

    public Task From(ApiErrorList errorList, HttpContext httpContext)
    {
        if (TryGetRedirectResponse(errorList, out var redirectUrl))
        {
            httpContext.Response.Redirect(redirectUrl);
            return Task.CompletedTask;
        }

        var statusCode = GetLowestErrorCodeInHighestGroup(errorList);
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        var response = new ErrorResponse(errorList);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new LowerCaseNamingPolicy()
        };
        var json = JsonSerializer.Serialize(response, options);
        return httpContext.Response.WriteAsync(json);
    }

    private bool TryGetRedirectResponse(ApiErrorList errorList, out string redirectUrl)
    {
        var redirectResponse = errorList.OfType<IRedirectResponse>().SingleOrDefault();
        if (redirectResponse is not null)
        {
            redirectUrl = redirectResponse.RedirectUrl;
            return true;
        }

        redirectUrl = string.Empty;
        return false;
    }

    private int GetLowestErrorCodeInHighestGroup(ApiErrorList errorList)
    {
        return errorList
            .GroupBy(apiError => Convert.ToInt32(Math.Floor((decimal)apiError.Status / 100)))
            .OrderByDescending(group => group.Key)
            .First()
            .OrderBy(apiError => apiError.Status)
            .First()
            .Status;
    }
}
