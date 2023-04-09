using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UserService;
using Application.Tests.Mocks.Configurations;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Core.Security.EmailAuthenticator;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Core.Security.OtpAuthenticator.OtpNet;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using Moq;
using StarterProject.Tests.Mocks.FakeDatas;
using StarterProject.Tests.Mocks.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Auth.Commands.Login.LoginCommand;

namespace StarterProject.Tests.Features.Auth.Commands.Login
{
    public class LoginTests
    {
        private readonly LoginCommand _loginCommand;
        private readonly LoginCommandHandler _loginCommandHandler;
        private readonly LoginCommandValidator _validator;
        private readonly IConfiguration _configuration;

        public LoginTests()
        {
            _configuration = MockConfiguration.GetConfigurationMock();
            #region Mock Repositories
            IUserOperationClaimRepository _userOperationClaimRepository =
                MockUserOperationClaimRepository.GetMockUserOperationClaimRepository();
            IRefreshTokenRepository _refreshTokenRepository = MockRefreshTokenRepository.GetMockRefreshTokenRepository();
            IEmailAuthenticatorRepository _userEmailAuthenticatorRepository =
                MockEmailAuthenticatorRepository.GetEmailAuthenticatorRepositoryMock();
            IOtpAuthenticatorRepository _userOtpAuthenticatorRepository = MockOtpAuthRepository.GetOtpAuthenticatorMock();
            IUserRepository _userRepository = MockUserRepository.GetUserMockRepository();
            #endregion
            #region Mock Helpers
            ITokenHelper tokenHelper = new JwtHelper(_configuration);
            IEmailAuthenticatorHelper emailAuthenticatorHelper = new EmailAuthenticatorHelper();
            IMailService mailService = new MailKitMailService(_configuration);
            IOtpAuthenticatorHelper otpAuthenticatorHelper = new OtpNetOtpAuthenticatorHelper();
            #endregion
            AuthBusinessRules _authBusinessRules = new AuthBusinessRules(_userRepository, _userEmailAuthenticatorRepository);
            IAuthService _authService = new AuthManager(
                _userOperationClaimRepository,
                _refreshTokenRepository,
                tokenHelper,
                _configuration
            );
            IUserService _userService = new UserManager(_userRepository);
            IAuthenticatorService _authententicatorService = new AuthenticatorManager(
                emailAuthenticatorHelper,
                _userEmailAuthenticatorRepository,
                mailService,
                otpAuthenticatorHelper,
                _userOtpAuthenticatorRepository
            );
            _validator = new LoginCommandValidator();
            _loginCommand = new LoginCommand();
            _loginCommandHandler = new LoginCommandHandler(_userService, _authService, _authBusinessRules, _authententicatorService);
        }

        [Fact]
        public async Task SuccessfullLoginShouldReturnAccessToken()
        {
            _loginCommand.UserForLoginDto = new() { Email = "halit@kodlama.io", Password = "123456" };
            var result = await _loginCommandHandler.Handle(_loginCommand, CancellationToken.None);
            Assert.NotNull(result.AccessToken.Token);
        }

        [Fact]
        public async Task AccessTokenShouldHaveValidExpirationTime()
        {
            _loginCommand.UserForLoginDto = new() { Email = "halit@kodlama.io", Password = "123456" };
            var result = await _loginCommandHandler.Handle(_loginCommand, CancellationToken.None);
            var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
            bool tokenExpiresInTime = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration + 1) > result.AccessToken.Expiration;
            Assert.True(tokenExpiresInTime, "Access token expiration time is invalid.");
        }

        [Fact]
        public async Task LoginWithWrongPasswordShouldThrowException()
        {
            _loginCommand.UserForLoginDto = new() { Email = "halit@kodlama.io", Password = "123456789" };
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _loginCommandHandler.Handle(_loginCommand, CancellationToken.None);
            });
        }

        [Fact]
        public async Task LoginWithWrongEmailShouldThrowException()
        {
            _loginCommand.UserForLoginDto = new() { Email = "halit1@kodlama.io", Password = "123456" };
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _loginCommandHandler.Handle(_loginCommand, CancellationToken.None);
            });
        }

        [Fact]
        public async Task LoginWithInvalidLengthPasswordShouldThrowException()
        {
            _loginCommand.UserForLoginDto = new() { Email = "halit1@kodlama.io", Password = "1" };
            var validationResult = _validator.TestValidate(_loginCommand);
            validationResult.ShouldHaveValidationErrorFor(i => i.UserForLoginDto.Password);
        }

        [Fact]
        public async Task LoginWithNullPasswordShouldThrowException()
        {
            _loginCommand.UserForLoginDto = new() { Email = "halit1@kodlama.io", Password = null };
            var validationResult = _validator.TestValidate(_loginCommand);
            validationResult.ShouldHaveValidationErrorFor(i => i.UserForLoginDto.Password);
        }
    }
}
