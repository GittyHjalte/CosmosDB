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


    public async Task<SupportMessage?> GetSupportMessage(string category)
    {
        try
        {
            var response = await _container.ReadItemAsync<SupportMessage>(category, new PartitionKey(category));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}