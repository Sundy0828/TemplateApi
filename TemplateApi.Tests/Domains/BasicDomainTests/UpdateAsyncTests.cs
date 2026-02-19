namespace TemplateApi.Tests.Domains.BasicDomainTests;

using Moq;
using TemplateApi.Models;

public class UpdateAsyncTests : BasicDomainTestsBase
{
    [Fact]
    public async Task UpdateAsyncReturnsUpdatedModel()
    {
        // Arrange
        var id = "1";
        var inputModel = new BasicModel { Name = "UpdatedItem" };

        MockDao.Setup(d => d.UpdateAsync(id, It.IsAny<BasicModel>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

        // Act
        await Domain.UpdateAsync(id, inputModel);

        // Assert
        MockDao.Verify(d => d.UpdateAsync(id, It.IsAny<BasicModel>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
