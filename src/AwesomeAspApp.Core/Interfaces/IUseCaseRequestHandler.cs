using AwesomeAspApp.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeAspApp.Core.Interfaces
{
    public interface IUseCaseRequestHandler<in TUseCaseRequest, TUseCaseResponse> where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
    {
        /// <summary>
        ///     The errors that occurred when executing the use case. If empty, the use case succeeded
        /// </summary>
        IEnumerable<Error> Errors { get; }

        Task<TUseCaseResponse> Handle(TUseCaseRequest message);
    }
}
