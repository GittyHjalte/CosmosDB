using Share.Models;
using Microsoft.Azure.Cosmos;
namespace API.Services;

public class SupportService
{
    private readonly Container _container;
    private readonly ILogger<SupportService> _logger;

    public SupportService(CosmosClient cosmosClient, string databaseName, string containerName, ILogger<SupportService> logger)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
        _logger = logger;
    }

    public async Task AddSupportMessage(SupportMessage message)
    {
        try
        {
            // Cosmos kræver 'id' - generer hvis det mangler
            message.Id ??= Guid.NewGuid().ToString();

            // Cosmos kræver at partition key-feltet er sat
            message.Category ??= "default";

            await _container.CreateItemAsync(message, new PartitionKey(message.Category));
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex, "CosmosDB Error: {Message}", ex.Message);
            throw;
        }
    }


    public async Task<IEnumerable<SupportMessage>> GetSupportMessage(string category)
    {
        var query = string.IsNullOrEmpty(category)
            ? "SELECT * FROM c"
            : $"SELECT * FROM c WHERE c.category = @category";

        var queryDef = new QueryDefinition(query);

        if (!string.IsNullOrEmpty(category))
        {
            queryDef.WithParameter("@category", category);
        }

        var results = new List<SupportMessage>();
        using (FeedIterator<SupportMessage> iterator = _container.GetItemQueryIterator<SupportMessage>(queryDef))
        {
            while (iterator.HasMoreResults)
            {
                FeedResponse<SupportMessage> response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }
        }

        return results;
    }
    }