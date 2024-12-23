﻿using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Commands;
using RPSSL.Application.Players.Models;
using RPSSL.Domain.Common.Collections;

namespace RPSSL.Application.Players.CreatePlayer;

public record CreatePlayerCommand(string Name) : ICommand<Result<PlayerResponse, ErrorList>>;