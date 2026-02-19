namespace TemplateApi.Tests.Domains.BasicDomainTests;

using Microsoft.Extensions.Logging;
using Moq;
using TemplateApi.Dao.Interfaces;
using TemplateApi.Domains;

public abstract class BasicDomainTestsBase
{
    protected Mock<IBasicDao> MockDao { get; }
    protected Mock<ILogger<BasicDomain>> MockLogger { get; }
    protected BasicDomain Domain { get; }

    protected BasicDomainTestsBase()
    {
        MockDao = new Mock<IBasicDao>();
        MockLogger = new Mock<ILogger<BasicDomain>>();
        Domain = new BasicDomain(MockLogger.Object, MockDao.Object);
    }
}
