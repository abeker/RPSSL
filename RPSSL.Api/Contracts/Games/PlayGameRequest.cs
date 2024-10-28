using System.ComponentModel.DataAnnotations;

namespace RPSSL.Api.Contracts.Games;

public record PlayGameRequest([Required] string Name, [Required] int Player);