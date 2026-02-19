namespace TemplateApi.Tests.Dao.BasicDaoTests;

using MongoDB.Driver;
using Moq;
using TemplateApi.Models;

public class DeleteAsyncTests : BasicDaoTestsBase
{
    [Fact]
    public async Task DeleteAsyncCallsDeleteOne()
    {
        var id = "deleteMe";

        MockCollection.Setup(c => c.DeleteOneAsync(
                It.IsAny<FilterDefinition<BasicModel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Mock.Of<DeleteResult>());

        await Dao.DeleteAsync(id);

        MockCollection.Verify(c => c.DeleteOneAsync(
            It.IsAny<FilterDefinition<BasicModel>>(),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
