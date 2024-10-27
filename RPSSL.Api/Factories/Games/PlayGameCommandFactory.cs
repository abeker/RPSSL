﻿using RPSSL.Api.Contracts.Games;
using RPSSL.Application.Games.PlayGameCommand;

namespace RPSSL.Api.Factories.Games;

public static class PlayGameCommandFactory
{
    public static PlayGameCommand Create(PlayGameTestRequest testRequest) => new(string.Empty, testRequest.Player);
    public static PlayGameCommand Create(PlayGameRequest request) => new(request.Name, request.Player);
}