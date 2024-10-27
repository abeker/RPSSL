using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Commands;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Players;

public record CreatePlayerCommand(string Name) : ICommand<UnitResult<ErrorList>>;