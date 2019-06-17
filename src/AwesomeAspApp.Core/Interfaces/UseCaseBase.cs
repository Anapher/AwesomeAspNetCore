using AwesomeAspApp.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeAspApp.Core.Interfaces
{
    public abstract class UseCaseStatus
    {
        protected List<Error> _errors;

        /// <summary>
        ///     The errors that occurred when executing this UseCase. If empty, the UseCase succeeded
        /// </summary>
        public IEnumerable<Error> Errors => _errors;

        /// <summary>
        ///     This adds one error to the Errors collection
        /// </summary>
        /// <param name="error">The error that should be added</param>
        protected void AddError(Error error)
        {
            _errors.Add(error);
        }

        /// <summary>
        ///     Returns the error: adds the error to the collection and returns default(T).
        /// </summary>
        /// <typeparam name="T">The use case response type</typeparam>
        /// <param name="error">The error that occurred.</param>
        /// <returns>Always return default(T)</returns>
        protected T ReturnError<T>(Error error)
        {
            AddError(error);
            return default;
        }
    }
}
