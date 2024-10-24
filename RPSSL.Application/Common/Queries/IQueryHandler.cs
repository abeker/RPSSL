using CSharpFunctionalExtensions;
using MediatR;

namespace RPSSL.Application.Common.Queries;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : IResult;