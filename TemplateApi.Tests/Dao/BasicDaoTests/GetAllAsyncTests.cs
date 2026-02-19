namespace TemplateApi.Tests.Dao.BasicDaoTests;

using MongoDB.Driver;
using Moq;
using TemplateApi.Models;
using TemplateApi.Parameters;

public class GetAllAsyncTests : BasicDaoTestsBase
{
    [Fact]
    public async Task GetAllAsyncReturnsAllModels()
    {
        var models = new List<BasicModel>
        {
            new() { Id = "1", Name = "A" },
            new() { Id = "2", Name = "B" }
        };

        var mockCursor = CreateMockCursor(models);

        MockCollection
            .Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<BasicModel>>(),
                It.IsAny<FindOptions<BasicModel, BasicModel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        var result = await Dao.GetAllAsync(new GetAllBasicParams() { });

        Assert.Equal(2, result.Items.Count);
        Assert.Equal("1", result.Items[0].Id);
        Assert.Equal("2", result.Items[1].Id);
    }
}
