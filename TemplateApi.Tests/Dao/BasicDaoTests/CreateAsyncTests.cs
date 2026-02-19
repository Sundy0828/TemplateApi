namespace TemplateApi.Tests.Dao.BasicDaoTests;

using Moq;
using TemplateApi.Models;

public class CreateAsyncTests : BasicDaoTestsBase
{
    [Fact]
    public async Task CreateAsyncCallsInsertOne()
    {
        var model = new BasicModel { Name = "New" };

        await Dao.CreateAsync(model);

        MockCollection.Verify(c => c.InsertOneAsync(
            It.Is<BasicModel>(m => m.Name == "New" && !string.IsNullOrEmpty(m.Id)),
            null,
            It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
