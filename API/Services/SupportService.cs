using System.ComponentModel;
using Core.Models;
using Microsoft.Azure.Cosmos;
using Container = Microsoft.Azure.Cosmos.Container;

namespace CosmosDB.Services;

public class SupportService
{
    private readonly Container _container;
    private readonly ILogger<SupportService> _logger;

    public SupportService(CosmosClient cosmosClient, string databaseName, string containerName, ILogger<SupportService> logger)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
        _logger = logger;
    }

    public async Task AddSupportMessage(SupportMessage supportMessage)
    {
        try
        {
            var partitionKey = new PartitionKey(supportMessage.category);

            await _container.UpsertItemAsync(supportMessage, partitionKey);
        }
        catch (CosmosException ex)
        {
            _logger.LogError($"CosmosDB Error: {ex.StatusCode} - {ex.Message}");
            _logger.LogError($"ActivityId: {ex.ActivityId}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<SupportMessage?> GetSupportMessage(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<SupportMessage>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
    public async Task<List<SupportMessage>> GetAllSupportMessages()
    {
        var query = _container.GetItemQueryIterator<SupportMessage>("SELECT * FROM c");
        var results = new List<SupportMessage>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

}