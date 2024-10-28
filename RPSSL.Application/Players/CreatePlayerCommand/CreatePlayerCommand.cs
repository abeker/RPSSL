﻿using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Commands;
using RPSSL.Application.Players.Models;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Players.CreatePlayerCommand;

public record CreatePlayerCommand(string Name) : ICommand<Result<PlayerResponse, ErrorList>>;