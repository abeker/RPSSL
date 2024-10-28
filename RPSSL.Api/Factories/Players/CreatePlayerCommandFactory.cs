using RPSSL.Api.Contracts.Players;
using RPSSL.Application.Players;
using RPSSL.Application.Players.CreatePlayerCommand;

namespace RPSSL.Api.Factories.Players;

public static class CreatePlayerCommandFactory
{
    public static CreatePlayerCommand Create(CreatePlayerRequest request) => new(request.Name);
}