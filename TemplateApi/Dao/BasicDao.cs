namespace TemplateApi.Dao;

using MongoDB.Driver;
using TemplateApi.Dao.Interfaces;
using TemplateApi.Models;
using TemplateApi.Paging;
using TemplateApi.Parameters;

public class BasicDao : IBasicDao
{
    private readonly ILogger<BasicDao> _logger;
    private readonly IMongoCollection<BasicModel> _collection;

    public BasicDao(ILogger<BasicDao> logger, IMongoDatabase database)
    {
        _logger = logger;
        _collection = database.GetCollection<BasicModel>("basics");

        SetupIndexes();
    }

    private void SetupIndexes()
    {
        CreateNameIndex();
        CreateLocationDateIndex();
    }

    private void CreateNameIndex()
    {
        _logger.LogInformation("Creating Name index on BasicModel collection");
        var keys = Builders<BasicModel>.IndexKeys.Ascending(x => x.Name);
        var model = new CreateIndexModel<BasicModel>(keys, new CreateIndexOptions { Unique = false });
        _collection.Indexes.CreateOne(model);
        _logger.LogInformation("Created Name index");
    }

    private void CreateLocationDateIndex()
    {
        var keys = Builders<BasicModel>.IndexKeys
            .Ascending(x => x.Location)
            .Ascending(x => x.Date);
        var model = new CreateIndexModel<BasicModel>(keys, new CreateIndexOptions { Unique = false });
        _collection.Indexes.CreateOne(model);
    }

    public async Task<PagedResult<BasicModel>> GetAllAsync(
        GetAllBasicParams parameters,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<BasicModel>.Filter.Empty;

        var totalItems = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var items = await _collection.Find(filter)
            .SortBy(x => x.Location)
            .ThenBy(x => x.Date)
            .Skip(parameters.Skip)
            .Limit(parameters.PageSize)
            .ToListAsync(cancellationToken);

        return PagedResultFactory.Create(
            items,
            parameters.PageNumber,
            parameters.PageSize,
            totalItems);
    }

    public async Task<BasicModel> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching BasicModel by Id: {Id}", id);
        var model = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        if (model == null)
        {
            _logger.LogWarning("No BasicModel found with Id: {Id}", id);
            throw new KeyNotFoundException($"No BasicModel found with Id: {id}");
        }

        return model;
    }


    public async Task<BasicModel> CreateAsync(BasicModel model, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating a new BasicModel with Name: {Name}", model.Name);

        model.Id = Guid.NewGuid().ToString();
        try
        {
            await _collection.InsertOneAsync(model, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to insert BasicModel with Name: {Name}", model.Name);
            throw;
        }

        _logger.LogInformation("Created BasicModel with Id: {Id}", model.Id);
        return model;
    }


    public async Task UpdateAsync(string id, BasicModel model, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating BasicModel with Id: {Id}", id);
        var result = await _collection.ReplaceOneAsync(x => x.Id == id, model, cancellationToken: cancellationToken);

        if (result.MatchedCount == 0)
        {
            _logger.LogWarning("No BasicModel matched for update with Id: {Id}", id);
        }
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting BasicModel with Id: {Id}", id);
        var result = await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
        if (result.DeletedCount == 0)
        {
            _logger.LogWarning("No BasicModel matched for delete with Id: {Id}", id);
        }
    }

}
