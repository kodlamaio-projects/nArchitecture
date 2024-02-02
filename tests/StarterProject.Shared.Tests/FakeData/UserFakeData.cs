using Shared.Implementations;
using Shared.Interfaces;

namespace StarterProject.Shared.Tests.FakeData;
internal class UserFakeData
{

    public string Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    private static List<UserFakeData> _users = new();




    public static IResult CreateUser(string firstName, string lastName, string email)
    {
        List<string> errorMessages = new();

        if (string.IsNullOrWhiteSpace(firstName))
            errorMessages.Add("First name cannot be empty!");

        if (string.IsNullOrWhiteSpace(lastName))
            errorMessages.Add("Last name cannot be empty!");

        if (string.IsNullOrWhiteSpace(email))
            errorMessages.Add("Email cannot be empty!");


        if (errorMessages.Count > 0)
            return Result.Failure(errors: errorMessages);

        _users.Add(new UserFakeData
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = firstName,
            LastName = lastName,
            Email = email
        });

        return Result.Success(message: "User has been created.");
    }

    public static UserFakeData Create(string firstName, string lastName, string email)
    {
        var user = new UserFakeData
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        _users.Add(user);

        return user;
    }

    public static IResult<UserFakeData> GetUsers()
    {
        return _users.Count > 0 ? Result<UserFakeData>.Success(statusCode: 200, values: _users)
                                : Result<UserFakeData>.Failure(statusCode: 200,error: "There is no any user!");
    }


    public static IResult<UserFakeData> GetUserById(string id)
    {
        var user = _users.SingleOrDefault(_ => _.Id == id);

        return user is null ? Result<UserFakeData>.Failure(statusCode: 404, error: "User not found!")
                            : Result<UserFakeData>.Success(statusCode: 200, value: user);
    }





}
