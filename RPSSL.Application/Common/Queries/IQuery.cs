using CSharpFunctionalExtensions;
using MediatR;

namespace RPSSL.Application.Common.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse> 
    where TResponse : IResult
{
}