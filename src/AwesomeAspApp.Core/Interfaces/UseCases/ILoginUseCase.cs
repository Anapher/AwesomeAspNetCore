﻿using AwesomeAspApp.Core.Dto.UseCaseRequests;
using AwesomeAspApp.Core.Dto.UseCaseResponses;

namespace AwesomeAspApp.Core.Interfaces.UseCases
{
    public interface ILoginUseCase : IUseCaseRequestHandler<LoginRequest, LoginResponse>
    {
    }
}
