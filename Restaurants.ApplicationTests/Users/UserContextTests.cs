using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUserTest_WithAuthenticatedUser_shouldReturnCurrentUser()
        {
            //arrange
            var dateOfBirth = new DateOnly(1980, 01, 01);
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "123"),
                new Claim(ClaimTypes.Email, "test@test.com"),
                new Claim(ClaimTypes.Role, UserRoles.Admin),
                new Claim(ClaimTypes.Role, UserRoles.User),
                new Claim("Nationality", "Indian"),
                new Claim("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd")),
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
            httpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });
            var userContext = new UserContext(httpContextAccessor.Object);

            //act
            var currentUser = userContext.GetCurrentUser();
            //assert
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("123");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().Contain(UserRoles.Admin, UserRoles.User);
            currentUser.Nationality.Should().Be("Indian");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);
        }

        [Fact()]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            //arrange
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext)null);
            var userContext = new UserContext(httpContextAccessor.Object);
            //act
            Action act = () => userContext.GetCurrentUser();
            //assert
            act.Should().Throw<InvalidOperationException>().WithMessage("User context is not present"); 
        }
    }
}