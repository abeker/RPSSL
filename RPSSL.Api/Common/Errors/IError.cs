using System.ComponentModel.DataAnnotations;

namespace RPSSL.Api.Common.Errors;

public interface IError {
    [Required]
    int Status { get; }
    
    [Required]
    string Code { get; }
    
    [Required]
    string Title { get; }
    
    [Required]
    ErrorSource Source { get; }
}