using System.ComponentModel.DataAnnotations;

namespace RPSSL.Api.Contracts.Players;

public record CreatePlayerRequest([Required] string Name);