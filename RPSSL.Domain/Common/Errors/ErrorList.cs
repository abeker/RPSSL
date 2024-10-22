﻿using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Lists;

namespace RPSSL.Domain.Common.Errors;

public class ErrorList : CombinableList<IError>
{
    public ErrorList() : base()
    {
    }

    public ErrorList(IError error) : base(error)
    {
    }

    public ErrorList(IEnumerable<IError> errors) : base(errors)
    {
    }

    public override string ToString()
    {
        return string.Join(", ", this.Select(error => $"[{error.ErrorCode}] {error.ErrorDescription}"));
    }

    protected override ICombine CreateCombinedList(IEnumerable<IError> items)
        => new ErrorList(items);
}