using Newtonsoft.Json;

namespace RPSSL.Api.Common.Errors;

public record ErrorSource(
    [property: JsonProperty(NullValueHandling = NullValueHandling.Ignore)] string Pointer = null, 
    [property: JsonProperty(NullValueHandling = NullValueHandling.Ignore)] string Parameter = null, 
    [property: JsonProperty(NullValueHandling = NullValueHandling.Ignore)] string UrlSegment = null, 
    [property: JsonProperty(NullValueHandling = NullValueHandling.Ignore)] string Header = null);