namespace TemplateApi.Tests.Domains.BasicDomainTests;

using Moq;

public class DeleteAsyncTests : BasicDomainTestsBase
{
    [Fact]
    public async Task DeleteAsyncCallsDaoOnce()
    {
        // Arrange
        var id = "1";

        MockDao.Setup(d => d.DeleteAsync(id, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

        // Act
        await Domain.DeleteAsync(id);

        // Assert
        MockDao.Verify(d => d.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }
}
