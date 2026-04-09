using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
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

    public sealed record Error(
        [property: JsonConverter(typeof(JsonNumberEnumConverter<HttpStatusCode>))] HttpStatusCode? Status, 
        string Description, 
        object? CustomObject = null)
    {
        public static Error None = new(null, string.Empty, null);
        //public static Error FromStatus(
        //    HttpStatusCode statusCode, 
        //    string? customMessage = null, 
        //    object? customObject = null)
        //{
        //    //var code = ((int)statusCode).ToString();
        //    var baseMessage = statusCode.ToString(); // e.g. "NotFound"

        //    // Optional: make it more readable ("Not Found")
        //    var formattedMessage = baseMessage; //SplitCamelCase(baseMessage);

        //    var finalMessage = string.IsNullOrWhiteSpace(customMessage)
        //        ? formattedMessage
        //        : $"{formattedMessage}: {customMessage}";

        //    return new Error(statusCode, finalMessage, customObject);
        //}

        private static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex
                .Replace(input, "(\\B[A-Z])", " $1");
        }

    }


}
