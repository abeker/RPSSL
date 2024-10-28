using System.ComponentModel.DataAnnotations;

namespace RPSSL.Api.Contracts.Games;

/// <summary>
/// The request to play a game round.
/// </summary>
public record PlayGameRequest
{
    /// <summary>
    /// The name of the player. This is a required field.
    /// </summary>
    [Required]
    public string PlayerName { get; init; }

    /// <summary>
    /// A player's choice represented as an integer. This is a required field.
    /// </summary>
    [Required]
    public int PlayerChoiceId { get; init; }
}