namespace TemplateApi.Domains;

using TemplateApi.Dao.Interfaces;
using TemplateApi.Domains.Interfaces;
using TemplateApi.Models;
using TemplateApi.Paging;
using TemplateApi.Parameters;

public class BasicDomain(ILogger<BasicDomain> logger, IBasicDao dao) : IBasicDomain
{
    private readonly ILogger<BasicDomain> _logger = logger;
    private readonly IBasicDao _dao = dao;

    public async Task<PagedResult<BasicModel>> GetAllAsync(GetAllBasicParams parameters, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all BasicModels. PageNumber: {PageNumber}, PageSize: {PageSize}", parameters.PageNumber, parameters.PageSize);

        try
        {
            var result = await _dao.GetAllAsync(parameters, cancellationToken);
            _logger.LogInformation("Fetched {Count} BasicModels", result.Items.Count);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch BasicModels");
            throw;
        }
    }

    public async Task<BasicModel> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching BasicModel by Id: {Id}", id);

        try
        {
            var model = await _dao.GetByIdAsync(id, cancellationToken);
            _logger.LogInformation("Fetched BasicModel with Id: {Id}", id);

            return model;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch BasicModel by Id: {Id}", id);
            throw;
        }
    }

    public async Task<BasicModel> CreateAsync(BasicModel model, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating a new BasicModel with Name: {Name}", model.Name);

        try
        {
            var created = await _dao.CreateAsync(model, cancellationToken);
            _logger.LogInformation("Created BasicModel with Id: {Id}", created.Id);
            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create BasicModel with Name: {Name}", model.Name);
            throw;
        }
    }

    public async Task UpdateAsync(string id, BasicModel model, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating BasicModel with Id: {Id}", id);

        try
        {
            model.Id = id;
            await _dao.UpdateAsync(id, model, cancellationToken);
            _logger.LogInformation("Updated BasicModel with Id: {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update BasicModel with Id: {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting BasicModel with Id: {Id}", id);

        try
        {
            await _dao.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Deleted BasicModel with Id: {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete BasicModel with Id: {Id}", id);
            throw;
        }
    }
}
