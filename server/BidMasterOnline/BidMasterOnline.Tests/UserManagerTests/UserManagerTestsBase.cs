using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Services;
using Moq;

namespace BidMasterOnline.Tests.UserManagerTests
{
    /// <summary>
    /// Base test class for UserManager unit tests.
    /// </summary>
    public class UserManagerTestsBase : ServiceTestsBase
    {
        protected readonly IUserManager service;
        protected readonly Mock<IAuthService> authServiceMock;

        public UserManagerTestsBase()
        {
            authServiceMock = new Mock<IAuthService>();

            service = new UserManager(repositoryMock.Object, authServiceMock.Object);
        }
    }
}
