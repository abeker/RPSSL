using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using RPSSL.Application.Choices.Persistence;
using RPSSL.Application.Common.Commands;
using RPSSL.Application.Common.Extensions;
using RPSSL.Domain.Choices;
using RPSSL.Domain.Choices.Services;
using RPSSL.Domain.Common.Extensions;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Games;
using RPSSL.Domain.Players;
using RPSSL.Domain.Players.Persistence;

namespace RPSSL.Application.Games.PlayGameCommand;

public class PlayGameCommandHandler(IPlayerRepository playerRepository, IChoiceService choiceService, IRandomNumberRepository randomNumberRepository, ILogger<PlayGameCommandHandler> logger) 
    : ICommandHandler<PlayGameCommand, Result<PlayGameResponse, ErrorList>>
{
    public async Task<Result<PlayGameResponse, ErrorList>> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Player '{PlayerName}' plays choice '{Choice}'", request.Name, request.Choice.ToString());
        
        var playerName = PlayerName.Create(string.IsNullOrWhiteSpace(request.Name) ? Player.Anonymous.Name.Value : request.Name);
        var playerChoice = request.Choice.TryConvertToEnum<Choice>();

        var playerChoiceResult = await playerName.CombineToTuple(playerChoice)
            .Bind(async tuple => await playerRepository.GetByName(tuple.Item1)
                .Bind(playerResult => PlayerChoice.Create(playerResult, tuple.Item2)));

        var computerChoiceResult = await randomNumberRepository.GenerateAsync(cancellationToken)
            .Bind(PositiveNumber.Create)
            .Bind(choiceService.GetByRandomNumber)
            .Bind(randomChoice => PlayerChoice.Create(Player.Computer, randomChoice));

        return playerChoiceResult.CombineToTuple(computerChoiceResult)
            .Bind(tuple => Game.Create(EntityId.Create(), tuple.Item1, tuple.Item2))
            .Bind(game => game.PlayRound(choiceService))
            .Map(game => new PlayGameResponse(game.GameResult.ToString().ToLowerInvariant(), (int)game.PlayerChoice.Choice,
                (int)game.ComputerChoice.Choice))
            .Tap(result => logger.LogInformation("Player '{PlayerName}' {GameResult}", request.Name, result.Results))
            .TapError(err => logger.LogError(err.ToString()));
    }
}