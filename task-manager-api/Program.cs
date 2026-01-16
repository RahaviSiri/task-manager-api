using task_manager_api.Config;
using task_manager_api.Hubs;
using task_manager_api.Repository;
using task_manager_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register database settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("Mongodb"));

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});

builder.Services.AddSingleton<TaskManagerService>();
builder.Services.AddSingleton<TaskManagerRepository>();


var app = builder.Build();
app.MapHub<TaskHub>("/taskHub").RequireCors(builder =>
    builder.WithOrigins("http://localhost:3000")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials()
);
// Assign endpoint to get connection with signalR for client side

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");
app.MapControllers();

app.Run();
