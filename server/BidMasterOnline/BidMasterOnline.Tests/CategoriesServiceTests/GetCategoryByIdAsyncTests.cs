﻿using BidMasterOnline.Domain.Entities;
using FluentAssertions;
using Moq;

namespace BidMasterOnline.Tests.CategoriesServiceTests
{
    /// <summary>
    /// Class for ICategoryService.GetCategoryByIdAsync method unit tests.
    /// </summary>
    public class GetCategoryByIdAsyncTests : CategoriesServiceTestsBase
    {
        [Fact]
        public async Task GetCategoryByIdAsync_IncorrectId_ThrowsKeyNotFoundExcpetion()
        {
            // Arrange
            var idToPass = Guid.NewGuid();

            repositoryMock.Setup(x => x.GetByIdAsync<Category>(idToPass, true))
                .ReturnsAsync(null as Category);

            // Assert
            var action = async () =>
            {
                // Act
                var category = await service.GetCategoryByIdAsync(idToPass);
            };

            await action.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ValidId_ReturnsCategory()
        {
            // Arrange
            var category = this.GetTestCategory();

            var idToPass = category.Id;

            repositoryMock.Setup(x => x.GetByIdAsync<Category>(idToPass, true))
                .ReturnsAsync(category);

            // Act
            var actualCategory = await service.GetCategoryByIdAsync(idToPass);

            // Assert
            actualCategory.Should().NotBeNull();
            actualCategory.Id.Should().Be(idToPass);
        }
    }
}
