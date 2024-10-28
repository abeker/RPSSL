using System.ComponentModel.DataAnnotations;

namespace RPSSL.Api.Contracts.Games;

public record PlayGameTestRequest([Required] int Player);