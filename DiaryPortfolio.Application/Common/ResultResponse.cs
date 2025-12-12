using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Common
{
    public class ResultResponse<T>
    {
        public T Result { get; }
        public Error Error { get; }

        private ResultResponse(T result, Error error)
        {
            Result = result;
            Error = error;
        }

        public static ResultResponse<T> Success(T value)
        {
            return new ResultResponse<T>(value, Error.None);
        }

        public static ResultResponse<T> Failure(Error error)
        {
            return new ResultResponse<T>(default!, error);
        }

    }

    public sealed record Error(string Code, string Description)
    {
        public static Error None = new(string.Empty, string.Empty);

    }

}
