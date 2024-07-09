using server.Hubs;
using server.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserRoomConnection>>(options => 
    new Dictionary<string, UserRoomConnection>());

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chat-application");
});

app.MapControllers();

app.Run();
