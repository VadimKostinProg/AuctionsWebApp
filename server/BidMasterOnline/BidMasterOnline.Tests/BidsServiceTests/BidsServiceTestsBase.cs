using BidMasterOnline.Application.ServiceContracts;
using BidMasterOnline.Application.Services;
using Moq;

namespace BidMasterOnline.Tests.BidsServiceTests
{
    /// <summary>
    /// Base test class for BidsService unit tests.
    /// </summary>
    public abstract class BidsServiceTestsBase : ServiceTestsBase
    {
        protected readonly IBidsService service;
        protected readonly Mock<IJWTService> jwtServiceMock;

        public BidsServiceTestsBase()
        {
            this.jwtServiceMock = new Mock<IJWTService>();

            service = new BidsService(this.repositoryMock.Object, jwtServiceMock.Object);
        }
    }
}
