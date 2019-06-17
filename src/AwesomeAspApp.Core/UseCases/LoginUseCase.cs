using AwesomeAspApp.Core.Dto;
using AwesomeAspApp.Core.Dto.UseCaseRequests;
using AwesomeAspApp.Core.Dto.UseCaseResponses;
using AwesomeAspApp.Core.Errors;
using AwesomeAspApp.Core.Interfaces;
using AwesomeAspApp.Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeAspApp.Core.UseCases
{
    public class LoginUseCase : UseCaseStatus, ILoginUseCase
    {
        public async Task<LoginResponse> Handle(LoginRequest message)
        {
            if (string.IsNullOrEmpty(message.Username))
                return ReturnError<LoginResponse>(CommonErrors.FieldValidation(nameof(message.Username), "The username must not be empty."));

            if (string.IsNullOrEmpty(message.Password))
                return ReturnError<LoginResponse>(CommonErrors.FieldValidation(nameof(message.Password), "The password must not be empty."));


        }
    }
}
