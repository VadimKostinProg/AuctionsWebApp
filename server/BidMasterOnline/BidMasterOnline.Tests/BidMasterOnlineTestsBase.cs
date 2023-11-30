using AutoFixture;
using BidMasterOnline.Domain.Entities;

namespace BidMasterOnline.Tests
{
    /// <summary>
    /// Base test class for all unit test.
    /// </summary>
    public abstract class BidMasterOnlineTestsBase
    {
        protected readonly Fixture fixture = new();

        public Category GetTestCategory(bool isDeleted = false)
        {
            return new Category
            {
                Id = Guid.NewGuid(),
                Name = fixture.Create<string>(),
                Description = fixture.Create<string>(),
                IsDeleted = isDeleted
            };
        }

        public IEnumerable<Category> GetTestCategories(int count = 10, bool isDeleted = false)
        {
            for (int i = 0; i < count; i++)
            {
                yield return this.GetTestCategory(isDeleted);
            }
        }
    }
}
