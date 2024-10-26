using CSharpFunctionalExtensions;
using MediatR;

namespace RPSSL.Application.Common.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse> where TResponse : IResult;