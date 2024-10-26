﻿using CSharpFunctionalExtensions;
using RPSSL.Domain.Choices.Services;
using RPSSL.Domain.Common.Lists;
using RPSSL.Domain.Common.Models;
using RPSSL.Domain.Players;
using Entity = RPSSL.Domain.Common.Models.Entity;

namespace RPSSL.Domain.Games;

public class Game : Entity
{
    public PlayerChoice PlayerChoice { get; }
    public PlayerChoice ComputerChoice { get; }
    public GameResult GameResult { get; private set; }
    
    private Game(EntityId id, PlayerChoice playerChoice, PlayerChoice computerChoice) : base(id)
    {
        PlayerChoice = playerChoice;
        ComputerChoice = computerChoice;
    }
    
    public static Result<Game, ErrorList> Create(EntityId id, PlayerChoice choice, PlayerChoice computerChoice)
    {
        return new Game(id, choice, computerChoice);
    }
    
    public Result<Game, ErrorList> PlayRound(IChoiceService choiceService)
    {
        var winner = choiceService.CalculateWinner(PlayerChoice.Choice, ComputerChoice.Choice);
        GameResult = winner.HasNoValue
            ? GameResult.Tie
            : winner.Value == PlayerChoice.Choice 
                ? GameResult.Win 
                : GameResult.Lose;
        
        return this;
    }
}