using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Rules;
using Application.Features.Users.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Core.Security.EmailAuthenticator;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Core.Security.OtpAuthenticator.OtpNet;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using StarterProject.Application.Tests.Mocks.Configurations;
using StarterProject.Application.Tests.Mocks.FakeDatas;
using StarterProject.Application.Tests.Mocks.Repositories.Auth;
using static Application.Features.Auth.Commands.Login.LoginCommand;

namespace StarterProject.Application.Tests.Features.Auth.Commands.Login
{
    public class LoginTests
    {
        private readonly LoginCommand _loginCommand;
        private readonly LoginCommandHandler _loginCommandHandler;
        private readonly LoginCommandValidator _validator;
        private readonly IConfiguration _configuration;

        public LoginTests(
            OperationClaimFakeData operationClaimFakeData,
            RefreshTokenFakeData refreshTokenFakeData,
            UserFakeData userFakeData
        )
        {
            _configuration = MockConfiguration.GetConfigurationMock();
            #region Mock Repositories
            IUserOperationClaimRepository _userOperationClaimRepository = new MockUserOperationClaimRepository(
                operationClaimFakeData
            ).GetMockUserOperationClaimRepository();
            IRefreshTokenRepository _refreshTokenRepository = new MockRefreshTokenRepository(
                refreshTokenFakeData
            ).GetMockRefreshTokenRepository();
            IEmailAuthenticatorRepository _userEmailAuthenticatorRepository =
                MockEmailAuthenticatorRepository.GetEmailAuthenticatorRepositoryMock();
            IOtpAuthenticatorRepository _userOtpAuthenticatorRepository = MockOtpAuthRepository.GetOtpAuthenticatorMock();
            IUserRepository _userRepository = new MockUserRepository(userFakeData).GetUserMockRepository();
            #endregion
            #region Mock Helpers
            ITokenHelper tokenHelper = new JwtHelper(_configuration);
            IEmailAuthenticatorHelper emailAuthenticatorHelper = new EmailAuthenticatorHelper();
            IMailService mailService = new MailKitMailService(_configuration);
            IOtpAuthenticatorHelper otpAuthenticatorHelper = new OtpNetOtpAuthenticatorHelper();
            #endregion
            AuthBusinessRules _authBusinessRules = new(_userRepository, _userEmailAuthenticatorRepository);
            IAuthService _authService = new AuthManager(
                _userOperationClaimRepository,
                _refreshTokenRepository,
                tokenHelper,
                _configuration,
                _authBusinessRules
            );
            UserBusinessRules _userBusinessRules = new(_userRepository);
            IUserService _userService = new UserManager(_userRepository, _userBusinessRules);
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
