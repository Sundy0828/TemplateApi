namespace TemplateApi.Tests.Dao.BasicDaoTests;

using MongoDB.Driver;
using Moq;
using TemplateApi.Models;

public class GetByIdAsyncTests : BasicDaoTestsBase
{
    [Fact]
    public async Task GetByIdAsyncReturnsModel()
    {
        var expected = new BasicModel { Id = "123", Name = "Test" };

        var mockCursor = CreateMockCursor([expected]);

        MockCollection
            .Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<BasicModel>>(),
                It.IsAny<FindOptions<BasicModel, BasicModel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        var result = await Dao.GetByIdAsync("123");

        Assert.NotNull(result);
        Assert.Equal("123", result.Id);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task GetByIdAsyncCallsDaoOnceWithCorrectId()
    {
        var expected = new BasicModel { Id = "123", Name = "Test" };

        var mockCursor = CreateMockCursor([null!]);

        MockCollection
            .Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<BasicModel>>(),
                It.IsAny<FindOptions<BasicModel, BasicModel>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            Dao.GetByIdAsync("2"));
    }
}
