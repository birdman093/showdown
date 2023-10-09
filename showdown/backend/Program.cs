using backend.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        builder.WithOrigins("https://localhost:3000", "http://localhost:3000")
            .AllowCredentials()
            //.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            //.SetIsOriginAllowedToAllowWildcardSubdomains());
});

var webSocketOptions = new WebSocketOptions{};
webSocketOptions.AllowedOrigins.Add("http://localhost:3000");
webSocketOptions.AllowedOrigins.Add("https://localhost:3000");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseWebSockets(webSocketOptions);

//app.UseAuthorization();
app.MapControllers();
app.MapHub<DraftHub>("/hub");

app.Run();

