using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Commands;
using RPSSL.Domain.Common.Collections;

namespace RPSSL.Application.Games.PlayGameCommand;

public record PlayGameCommand(string Name, int Choice) : ICommand<Result<PlayGameResponse, ErrorList>>;