using System.ComponentModel.DataAnnotations;

namespace RPSSL.Api.Common.Errors;

public interface IError {
    [Required]
    public int Status { get; }
    
    [Required]
    public string Code { get; }
    
    [Required]
    public string Title { get; }
    
    [Required]
    public ErrorSource Source { get; }
}