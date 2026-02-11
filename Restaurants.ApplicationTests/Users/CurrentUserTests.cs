using FluentAssertions;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.User.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.User)]
    [InlineData(UserRoles.Admin)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        //arrange
        var currentUser = new CurrentUser(
           "123",
            "test@test1.com",[UserRoles.User,UserRoles.Admin], "Indian",null);
        //act
        var isInRole=currentUser.IsInRole(roleName);
        //assert
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        //arrange
        var currentUser = new CurrentUser(
           "123",
            "test@test1.com", [UserRoles.User, UserRoles.Admin], "Indian", null);
        //act
        var isInRole = currentUser.IsInRole(UserRoles.Owner);
        //assert
        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        //arrange
        var currentUser = new CurrentUser(
           "123",
            "test@test1.com", [UserRoles.User, UserRoles.Admin], "Indian", null);
        //act
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());
        //assert
        isInRole.Should().BeFalse();
    }

}