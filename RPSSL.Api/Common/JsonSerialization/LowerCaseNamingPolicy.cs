using System.Text.Json;

namespace RPSSL.Api.Common.JsonSerialization;

public class LowerCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return name.ToLowerInvariant();
    }
}
