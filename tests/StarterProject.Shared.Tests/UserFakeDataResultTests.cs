using Shared.Interfaces;
using StarterProject.Shared.Tests.FakeData;

namespace StarterProject.Shared.Tests;

public class UserFakeDataResultTests
{

    [Fact]
    public void CreateUser_When_ParametersAreNullOrWhiteSpace_ShouldBeReturn_Failed()
    {
        IResult createdResult = UserFakeData.CreateUser(" ", " ", " ");

        Assert.False(createdResult.IsSuccess);

        Assert.Equal(3, createdResult.Messages!.Count());

        Assert.Null(createdResult.Message);

        Assert.Null(createdResult.StatusCode);
    }


    [Fact]
    public void CreateUser_When_ParametersAreNotNullOrWhiteSpace_ShouldBeReturn_Successed()
    {
        IResult createdResult = UserFakeData.CreateUser("John", "Doe", "john.doe@mail.com");

        Assert.True(createdResult.IsSuccess);

        Assert.Null(createdResult.Messages!);

        Assert.NotNull(createdResult.Message);

        Assert.Equal("User has been created.", createdResult.Message);

        Assert.Null(createdResult.StatusCode);
    }


    [Fact]
    public void GetUser_When_ValuesCountIsZero_ShouldBeReturn_Failed()
    {
        var usersResult = UserFakeData.GetUsers();

        Assert.False(usersResult.IsSuccess);

        Assert.Null(usersResult.Values);

        Assert.Equal(200, usersResult.StatusCode);

        Assert.Equal("There is no any user!", usersResult.Message);

    }

    [Fact]
    public void GetUser_When_ValuesIsNotNull_ShouldBeReturn_Successed()
    {
        UserFakeData.CreateUser("John", "Doe", "john.doe@mail.com");

        var usersResult = UserFakeData.GetUsers();

        Assert.True(usersResult.IsSuccess);

        Assert.NotNull(usersResult.Values);

        Assert.Equal(200, usersResult.StatusCode);

    }

    [Fact]
    public void GetUserById_When_ValueIsNull_ShouldBeReturn_Failed()
    {
        var user = UserFakeData.Create("John", "Doe", "john.doe@mail.com");

        var usersResult = UserFakeData.GetUserById(Guid.NewGuid().ToString());

        Assert.False(usersResult.IsSuccess);

        Assert.Null(usersResult.Value);

        Assert.Equal(404, usersResult.StatusCode);

        Assert.Equal("User not found!", usersResult.Message);


    }

    [Fact]
    public void GetUserById_When_ValueIsNotNull_ShouldBeReturn_Successed()
    {
        var user = UserFakeData.Create("John", "Doe", "john.doe@mail.com");

        var usersResult = UserFakeData.GetUserById(user.Id);

        Assert.True(usersResult.IsSuccess);

        Assert.NotNull(usersResult.Value);

        Assert.Equal(200, usersResult.StatusCode);



    }
}
