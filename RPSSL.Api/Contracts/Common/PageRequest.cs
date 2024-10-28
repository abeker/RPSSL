using System.ComponentModel.DataAnnotations;

namespace RPSSL.Api.Contracts.Common;

public record PageRequest(
    [Required]
    [Range(0, int.MaxValue)] 
    int Index, 
    
    [Required]
    [Range(0, int.MaxValue)] 
    int Size);