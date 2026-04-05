using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public static Error FromStatus(HttpStatusCode statusCode, string? customMessage = null)
        {
            var code = ((int)statusCode).ToString();
            var baseMessage = statusCode.ToString(); // e.g. "NotFound"

            // Optional: make it more readable ("Not Found")
            var formattedMessage = baseMessage; //SplitCamelCase(baseMessage);

            var finalMessage = string.IsNullOrWhiteSpace(customMessage)
                ? formattedMessage
                : $"{formattedMessage}: {customMessage}";

            return new Error(code, finalMessage);
        }

        private static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex
                .Replace(input, "(\\B[A-Z])", " $1");
        }

    }


}
