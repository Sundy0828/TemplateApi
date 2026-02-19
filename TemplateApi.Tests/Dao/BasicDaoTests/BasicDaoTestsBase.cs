namespace TemplateApi.Tests.Dao.BasicDaoTests;

using TemplateApi.Dao;
using TemplateApi.Models;
using TemplateApi.Tests.Dao;

public abstract class BasicDaoTestsBase : MongoDaoTestsBase<BasicModel, BasicDao>
{
    protected BasicDaoTestsBase()
        : base("basics", (logger, db) => new BasicDao(logger, db))
    {
    }
}
