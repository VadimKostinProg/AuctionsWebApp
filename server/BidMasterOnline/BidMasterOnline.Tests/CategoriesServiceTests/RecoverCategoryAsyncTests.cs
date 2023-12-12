using BidMasterOnline.Domain.Entities;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace BidMasterOnline.Tests.CategoriesServiceTests
{
    /// <summary>
    /// Class for ICategoryService.RecoverCategoryAsync method unit tests.
    /// </summary>
    public class RecoverCategoryAsyncTests : CategoriesServiceTestsBase
    {
        [Fact]
        public async Task RecoverCategoryAsync_IncorrectId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var idToPass = Guid.NewGuid();

            repositoryMock.Setup(x => x.GetByIdAsync<Category>(idToPass, false))
                .ReturnsAsync(null as Category);

            // Assert
            var action = async () =>
            {
                // Act
                await service.RecoverCategoryAsync(idToPass);
            };

            await action.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task RecoverCategoryAsync_CategoryIsNotDeleted_ThrowsInvalidOperationException()
        {
            // Arrange
            var categoryToRecover = this.GetTestCategory();

            var idToPass = categoryToRecover.Id;

            repositoryMock.Setup(x => x.GetByIdAsync<Category>(idToPass, false))
                .ReturnsAsync(categoryToRecover);

            // Assert
            var action = async () =>
            {
                // Act
                await service.RecoverCategoryAsync(idToPass);
            };

            await action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task RecoverCategoryAsync_NameAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var categoryToRecover = this.GetTestCategory(isDeleted: true);

            var idToPass = categoryToRecover.Id;

            repositoryMock.Setup(x => x.GetByIdAsync<Category>(idToPass, false))
                .ReturnsAsync(categoryToRecover);
            repositoryMock.Setup(x => x.AnyAsync<Category>(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(true);

            // Assert
            var action = async () =>
            {
                // Act
                await service.RecoverCategoryAsync(idToPass);
            };

            await action.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task RecoverCategoryAsync_ValidId_SuccessfullCategoryRecovering()
        {
            // Arrange
            var categoryToRecover = this.GetTestCategory(isDeleted: true);

            var idToPass = categoryToRecover.Id;

            repositoryMock.Setup(x => x.GetByIdAsync<Category>(idToPass, false))
                .ReturnsAsync(categoryToRecover);
            repositoryMock.Setup(x => x.AnyAsync<Category>(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(false);
            repositoryMock.Setup(x => x.UpdateAsync<Category>(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);

            // Assert
            var action = async () =>
            {
                // Act
                await service.RecoverCategoryAsync(idToPass);
            };

            await action.Should().NotThrowAsync();
        }
    }
}
