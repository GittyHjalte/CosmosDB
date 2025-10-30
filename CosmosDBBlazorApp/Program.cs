using CosmosDBBlazorApp.Components;
using CosmosDBBlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddScoped<SupportMessageService>();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// HttpClient for API
builder.Services.AddHttpClient<SupportMessageService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5203/");
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication/Authorization (if any)
// app.UseAuthentication();
// app.UseAuthorization();

// Apply CORS policy
app.UseCors("AllowAll");

// ⚠️ Anti-forgery middleware REQUIRED for interactive server components
app.UseAntiforgery();

// Map interactive Blazor components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();