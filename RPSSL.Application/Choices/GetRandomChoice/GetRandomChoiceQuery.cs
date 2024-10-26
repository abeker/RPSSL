using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Queries;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Application.Choices.GetRandomChoice;

public record GetRandomChoiceQuery : IQuery<Result<RandomChoiceResponse, ErrorList>>;