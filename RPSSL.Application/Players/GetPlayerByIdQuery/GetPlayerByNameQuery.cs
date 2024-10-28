﻿using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Queries;
using RPSSL.Application.Players.Models;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Players.GetPlayerByIdQuery;

public record GetPlayerByNameQuery(string Name) : IQuery<Result<PlayerResponse, ErrorList>>;