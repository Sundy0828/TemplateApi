namespace TemplateApi.Tests.Domains.BasicDomainTests;

using Moq;
using TemplateApi.Models;

public class CreateAsyncTests : BasicDomainTestsBase
{
    [Fact]
    public async Task CreateAsyncReturnsCreatedModel()
    {
        // Arrange
        var inputModel = new BasicModel { Name = "NewItem" };
        var createdModel = new BasicModel { Id = "1", Name = "NewItem" };

        MockDao.Setup(d => d.CreateAsync(inputModel, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdModel);

        // Act
        var result = await Domain.CreateAsync(inputModel);

        MockDao.Verify(d => d.CreateAsync(inputModel, It.IsAny<CancellationToken>()), Times.Once);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
        Assert.Equal("NewItem", result.Name);
    }
}
