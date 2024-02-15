using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Profiles;
using Application.Features.Auth.Rules;
using Application.Features.Users.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UsersService;
using AutoMapper;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using NArchitecture.Core.Localization.Resource.Yaml;
using NArchitecture.Core.Mailing;
using NArchitecture.Core.Mailing.MailKit;
using NArchitecture.Core.Security.EmailAuthenticator;
using NArchitecture.Core.Security.JWT;
using NArchitecture.Core.Security.OtpAuthenticator;
using NArchitecture.Core.Security.OtpAuthenticator.OtpNet;
using StarterProject.Application.Tests.Mocks.Configurations;
using StarterProject.Application.Tests.Mocks.FakeDatas;
using StarterProject.Application.Tests.Mocks.Repositories.Auth;
using static Application.Features.Auth.Commands.Login.LoginCommand;

namespace StarterProject.Application.Tests.Features.Auth.Commands.Login;

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
        ITokenHelper<Guid, int> tokenHelper = new JwtHelper<Guid, int>(_configuration);
        IEmailAuthenticatorHelper emailAuthenticatorHelper = new EmailAuthenticatorHelper();
        MailSettings mailSettings =
            _configuration.GetSection("MailSettings").Get<MailSettings>() ?? throw new Exception("Mail settings not found.");
        IMailService mailService = new MailKitMailService(mailSettings);
        IOtpAuthenticatorHelper otpAuthenticatorHelper = new OtpNetOtpAuthenticatorHelper();
        ILocalizationService localizationService = new ResourceLocalizationManager(resources: [])
        {
            AcceptLocales = new[] { "en" }
        };
        IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()));
        #endregion
        AuthBusinessRules authBusinessRules = new(_userRepository, localizationService);
        IAuthService _authService = new AuthManager(
            _userOperationClaimRepository,
            _refreshTokenRepository,
            tokenHelper,
            _configuration,
            mapper
        );
        UserBusinessRules _userBusinessRules = new(_userRepository, localizationService);
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
        _loginCommandHandler = new LoginCommandHandler(_userService, _authService, authBusinessRules, _authententicatorService);
    }

    [Fact]
    public async Task SuccessfulLoginShouldReturnAccessToken()
    {
        _loginCommand.UserForLoginDto = new() { Email = "example@kodlama.io", Password = "123456" };
        LoggedResponse result = await _loginCommandHandler.Handle(_loginCommand, CancellationToken.None);
        Assert.NotNull(result.AccessToken.Token);
    }

    [Fact]
    public async Task AccessTokenShouldHaveValidExpirationTime()
    {
        _loginCommand.UserForLoginDto = new() { Email = "example@kodlama.io", Password = "123456" };
        LoggedResponse result = await _loginCommandHandler.Handle(_loginCommand, CancellationToken.None);
        TokenOptions? tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        bool tokenExpiresInTime =
            DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration + 1) > result.AccessToken.ExpirationDate;
        Assert.True(tokenExpiresInTime, "Access token expiration time is invalid.");
    }

    [Fact]
    public async Task LoginWithWrongPasswordShouldThrowException()
    {
        _loginCommand.UserForLoginDto = new() { Email = "example@kodlama.io", Password = "123456789" };
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
    public void LoginWithInvalidLengthPasswordShouldThrowException()
    {
        _loginCommand.UserForLoginDto = new() { Email = "halit1@kodlama.io", Password = "1" };
        TestValidationResult<LoginCommand> validationResult = _validator.TestValidate(_loginCommand);
        validationResult.ShouldHaveValidationErrorFor(i => i.UserForLoginDto.Password);
    }

    [Fact]
    public void LoginWithNullPasswordShouldThrowException()
    {
        _loginCommand.UserForLoginDto = new() { Email = "halit1@kodlama.io", Password = null! };
        TestValidationResult<LoginCommand> validationResult = _validator.TestValidate(_loginCommand);
        validationResult.ShouldHaveValidationErrorFor(i => i.UserForLoginDto.Password);
    }
}
