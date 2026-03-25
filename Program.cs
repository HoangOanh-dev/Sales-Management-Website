using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()  // Allow any origin
                   .AllowAnyMethod()  // Allow any method
                   .AllowAnyHeader();  // Allow any header
        });
});

// Add logging services
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error"); // Custom error handling
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins"); // Use CORS policy
app.UseRouting();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();
