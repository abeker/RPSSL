using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Queries;
using RPSSL.Application.Players.Models;
using RPSSL.Domain.Common.Collections;

namespace RPSSL.Application.Players.GetPlayerByName;

public record GetPlayerByNameQuery(string Name) : IQuery<Result<PlayerResponse, ErrorList>>;