﻿using System;
using AwesomeAspApp.Core.Interfaces.Services;
using AwesomeAspApp.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AwesomeAspApp.Infrastructure.Auth
{
    internal sealed class JwtValidator : IJwtValidator
    {
        private readonly IJwtHandler _jwtTokenHandler;
        private readonly AuthSettings _options;

        public JwtValidator(IJwtHandler jwtTokenHandler, IOptions<AuthSettings> options)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _options = options.Value;
        }

        public ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            if (_options.SecretKey == null)
                throw new InvalidOperationException("The secret key must not be null.");

            return _jwtTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                ValidateLifetime = false // we check expired tokens here
            });
        }
    }
}