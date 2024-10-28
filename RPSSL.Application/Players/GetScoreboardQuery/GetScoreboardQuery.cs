using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Queries;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Players.GetScoreboardQuery;

public record GetScoreboardQuery(int Index, int Size) : IQuery<Result<ScoreboardResponse, ErrorList>>;