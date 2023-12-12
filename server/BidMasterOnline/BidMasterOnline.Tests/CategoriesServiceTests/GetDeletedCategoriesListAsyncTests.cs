using BidMasterOnline.Application.DTO;
using BidMasterOnline.Application.Specifications;
using BidMasterOnline.Domain.Entities;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace BidMasterOnline.Tests.CategoriesServiceTests
{
    /// <summary>
    /// Class for ICategoryService.GetDeletedCategoriesListAsync method unit tests.
    /// </summary>
    public class GetDeletedCategoriesListAsyncTests : CategoriesServiceTestsBase
    {
        [Fact]
        public async Task GetDeletedCategoriesListAsync_NullSpecification_ThrowsArgumentNullException()
        {
            // Arrange
            var categories = this.GetTestCategories(count: 20, isDeleted: true);

            CategorySpecificationsDTO specification = null;

            // Assert
            var action = async () =>
            {
                // Act
                var categoriesList = await this.service.GetDeletedCategoriesListAsync(specification);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetDeletedCategoriesListAsync_SpecificationsPassed_ReturnsCategoriesWithApplyedSpecifications()
        {
            // Arrange
            var categories = this.GetTestCategories(count: 20, isDeleted: true).ToList();

            const string name = "test name";

            categories[0].Name = categories[1].Name = categories[2].Name = name;

            CategorySpecificationsDTO specificationsDTO = new()
            {
                Name = name,
                SortField = "Name",
                PageNumber = 1,
                PageSize = 3
            };

            var expectedCategoriesList = categories.Where(x => x.Name == name)
                .OrderBy(x => x.Name)
                .Take(3)
                .ToList();

            var expectedCategoriesDTO = expectedCategoriesList.Select(x => mapper.Map<CategoryDTO>(x)).ToList();

            var expectedTotalPages = (long)Math.Ceiling((double)categories.Count / specificationsDTO.PageSize);

            repositoryMock.Setup(x => x.GetAsync<Category>(It.IsAny<ISpecification<Category>>(), false))
                .ReturnsAsync(expectedCategoriesList);
            repositoryMock.Setup(x => x.CountAsync<Category>(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(categories.Count);

            // Act
            var categoriesList = await service.GetDeletedCategoriesListAsync(specificationsDTO);

            // Assert
            categoriesList.Should().NotBeNull();
            categoriesList.List.Should().BeEquivalentTo(expectedCategoriesDTO);
            categoriesList.TotalPages.Should().Be(expectedTotalPages);
        }
    }
}
