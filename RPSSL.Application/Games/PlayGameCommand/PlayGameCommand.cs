using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Commands;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Games.PlayGameCommand;

public record PlayGameCommand(string Name, int Choice) : ICommand<Result<PlayGameResponse, ErrorList>>;