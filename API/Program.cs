using CosmosDB.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton(s =>
{
    var cosmosClientOptions = new CosmosClientOptions
    {
        HttpClientFactory = () =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (req, cert, chain, errors) => true
            };
            return new HttpClient(handler);
        }
    };

    var cosmosClient = new CosmosClient(
        builder.Configuration["CosmosDb:Account"],
        builder.Configuration["CosmosDb:Key"],
        cosmosClientOptions);

    var logger = s.GetRequiredService<ILogger<SupportService>>();

    return new SupportService(
        cosmosClient,
        builder.Configuration["CosmosDb:DatabaseName"],
        builder.Configuration["CosmosDb:ContainerName"],
        logger);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();