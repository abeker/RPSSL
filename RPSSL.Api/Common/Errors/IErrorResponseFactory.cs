using Microsoft.AspNetCore.Mvc;

namespace RPSSL.Api.Common.Errors;

/// <summary>
/// Factory for constructing ObjectResults with error descriptions in the response body
/// </summary>
public interface IErrorResponseFactory
{
    ActionResult From(ApiErrorList validation);

    Task From(ApiErrorList errorList, HttpContext httpContext);
}
