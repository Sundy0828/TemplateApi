namespace TemplateApi.Dao.Interfaces;

using TemplateApi.Models;
using TemplateApi.Paging;
using TemplateApi.Parameters;

public interface IBasicDao
{
    public Task<PagedResult<BasicModel>> GetAllAsync(GetAllBasicParams parameters, CancellationToken cancellationToken = default);
    public Task<BasicModel> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    public Task<BasicModel> CreateAsync(BasicModel model, CancellationToken cancellationToken = default);
    public Task UpdateAsync(string id, BasicModel model, CancellationToken cancellationToken = default);
    public Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
