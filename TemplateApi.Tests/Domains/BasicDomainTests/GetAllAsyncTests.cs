namespace TemplateApi.Tests.Domains.BasicDomainTests;

using Moq;
using TemplateApi.Models;
using TemplateApi.Paging;
using TemplateApi.Parameters;

public class GetAllAsyncTests : BasicDomainTestsBase
{
    [Fact]
    public async Task GetAllAsyncReturnsPagedResult()
    {
        // Arrange
        var sampleList = new List<BasicModel>
        {
            new() { Id = "1", Name = "Test1" },
            new() { Id = "2", Name = "Test2" }
        };

        var pagedResult = PagedResultFactory.Create(sampleList, 1, 10, sampleList.Count);

        MockDao.Setup(d => d.GetAllAsync(It.IsAny<GetAllBasicParams>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pagedResult);

        var parameters = new GetAllBasicParams
        {
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = await Domain.GetAllAsync(parameters);

        MockDao.Verify(d => d.GetAllAsync(It.IsAny<GetAllBasicParams>(), It.IsAny<CancellationToken>()), Times.Once);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(2, result.TotalItems);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal("Test1", result.Items[0].Name);
        Assert.Equal("Test2", result.Items[1].Name);
    }
}
