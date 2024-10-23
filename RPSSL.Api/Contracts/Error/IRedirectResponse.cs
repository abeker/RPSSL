using System.ComponentModel.DataAnnotations;

namespace RPSSL.Api.Contracts.Error;

public interface IRedirectResponse
{
    [Required]
    public string Title { get; }
    [Required]
    public string RedirectUrl { get; }
}
