using AwesomeAspApp.Core.Dto;
using AwesomeAspApp.Core.Errors;
using AwesomeAspApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;

namespace AwesomeAspApp.Extensions
{
    public static class ResponseExtensions
    {
        private static IImmutableDictionary<string, int> ErrorStatusCodes { get; } =
                new Dictionary<ErrorType, HttpStatusCode>
                {
                    {ErrorType.ValidationError, HttpStatusCode.BadRequest},
                    {ErrorType.Authentication, HttpStatusCode.Unauthorized},
                }.ToImmutableDictionary(x => x.Key.ToString(), x => (int)x.Value);

        public static ActionResult ToActionResult(this IUseCaseErrors status)
        {
            if (!status.HasError)
                return new OkResult();

            return ToActionResult(status.Error!);
        }

        public static ActionResult ToActionResult(this Error error)
        {
            var httpCode = ErrorStatusCodes[error.Type];
            return new ObjectResult(error) { StatusCode = httpCode };
        }
    }
}