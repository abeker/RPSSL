using CSharpFunctionalExtensions;
using MediatR;

namespace RPSSL.Application.Common.Commands;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TResult : IResult;