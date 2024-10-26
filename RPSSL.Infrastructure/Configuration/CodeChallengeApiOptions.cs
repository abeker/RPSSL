using Microsoft.Extensions.Options;

namespace RPSSL.Infrastructure.Configuration;

public class CodeChallengeApiOptions : IValidateOptions<CodeChallengeApiOptions>
{
    public const string CodeChallengeApi = nameof(CodeChallengeApi);

    public string BaseAddress { get; init; } = string.Empty;
    
    public ValidateOptionsResult Validate(string name, CodeChallengeApiOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.BaseAddress)) {
            return ValidateOptionsResult.Fail($"Env variable: {nameof(options.BaseAddress)} not defined");
        }
        
        return ValidateOptionsResult.Success;
    }
}