using System.Text.Json.Serialization;

namespace RPSSL.Infrastructure.ApiClients.CodeChallenge;

public record RandomNumberResponse
{
    [JsonPropertyName("random_number")]
    public int RandomNumber { get; init; }
}