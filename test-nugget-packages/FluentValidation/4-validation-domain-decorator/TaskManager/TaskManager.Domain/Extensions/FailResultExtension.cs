using FluentResults;

namespace TaskManager.Domain.Extensions
{
    public static class FailResultExtension
    {
        public static TResult ToGenericFailedResult<TResult>(this Result value) where TResult : class
        {
            if (typeof(TResult).GenericTypeArguments.Length == 0)
                return value as TResult;

            var returnType = typeof(TResult).GenericTypeArguments[0];
            var toResultMethod = value.GetType().GetMethod("ToResult");
            var toResultGenericMethod = toResultMethod.MakeGenericMethod(returnType);
            var result = toResultGenericMethod.Invoke(value, new object[] { });
            return (result as TResult);
        }
    }
}
