using CSharpFunctionalExtensions;
using RPSSL.Application.Common.Queries;
using RPSSL.Domain.Common.Collections;

namespace RPSSL.Application.Choices.GetRandomChoice;

public record GetRandomChoiceQuery : IQuery<Result<RandomChoiceResponse, ErrorList>>;