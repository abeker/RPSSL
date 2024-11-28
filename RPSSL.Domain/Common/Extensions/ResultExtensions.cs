using CSharpFunctionalExtensions;
using RPSSL.Domain.Common.Collections;

namespace RPSSL.Domain.Common.Extensions;

public static class ResultExtensions
{
    public static Result<(T1, T2), ErrorList> CombineToTuple<T1, T2>(this Result<T1, ErrorList> r1, Result<T2, ErrorList> r2) =>
        r1.Match(
            r1v => r2.Map(rest => (r1v, rest)),
            r1e => r2.Match(_ => r1e, r2e => new ErrorList(r1e.Concat(r2e).Distinct())));
}