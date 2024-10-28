using System.ComponentModel.DataAnnotations;

namespace RPSSL.Api.Contracts.Players;

/// <summary>
/// The request to create a new player.
/// </summary>
public record CreatePlayerRequest
{
    /// <summary>
    /// The name of the player. This is a required field.
    /// </summary>
    [Required]
    public string Name { get; init; }
}