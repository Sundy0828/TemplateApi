namespace TemplateApi.Tests.Dao.BasicDaoTests;

using MongoDB.Driver;
using Moq;
using TemplateApi.Models;

public class UpdateAsyncTests : BasicDaoTestsBase
{
    [Fact]
    public async Task UpdateAsyncCallsReplaceOne()
    {
        var id = "xyz";
        var model = new BasicModel { Id = id, Name = "Updated" };

        MockCollection.Setup(c => c.ReplaceOneAsync(
                It.IsAny<FilterDefinition<BasicModel>>(),
                model,
                It.IsAny<ReplaceOptions>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Mock.Of<ReplaceOneResult>());

        await Dao.UpdateAsync(id, model);

        MockCollection.Verify(c => c.ReplaceOneAsync(
            It.IsAny<FilterDefinition<BasicModel>>(),
            model,
            It.IsAny<ReplaceOptions>(),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
