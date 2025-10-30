using API.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton(s =>
{
    var logger = s.GetRequiredService<ILogger<SupportService>>();

    var connectionString = builder.Configuration["CosmosDb:ConnectionString"];
    
    var cosmosClient = new CosmosClient(connectionString);
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

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();