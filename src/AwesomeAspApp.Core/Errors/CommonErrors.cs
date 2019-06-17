using AwesomeAspApp.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeAspApp.Core.Errors
{
    public static class CommonErrors
    {
        public static Error FieldValidation(IReadOnlyDictionary<string, string> fieldErrors)
        {
            if (!fieldErrors.Any()) throw new ArgumentException("You must give at least one field error.", nameof(fieldErrors));

            return new Error(ErrorTypes.BadRequest.ToString(), "Request validation failed.", (int)ErrorCodes.FieldValidation, fieldErrors);
        }

        public static Error FieldValidation(string name, string error)
        {
            return FieldValidation(new Dictionary<string, string> { { name, error } });
        }
    }
}
