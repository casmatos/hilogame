using HiLoGame.Shared.DTO;
using HILoGame.Shared.Models;
using HILoGame.WebApi.Middlewares;
using HILoGame.WebApi.Services;
using HILoGame.WebApi.Settings;
using HILoGameWebApi.SignalR;
using Microsoft.AspNetCore.ResponseCompression;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Reflection;

const string POLICY_APP = "policyGame";

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddCors(options =>
{
    options.AddPolicy(name: POLICY_APP,
                builder =>
                {
                    builder.WithOrigins("https://localhost:7228", "https://localhost:7094")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
});

services.AddControllers();
services.AddScoped<ExeptionMiddleware>();

var configuration = builder.Configuration;

services.AddSignalR();

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
services.Configure<HiLoDatabaseSettings>(builder.Configuration.GetSection("HiLoDatabase"));

services.AddScoped<IBaseRepository<Player>, PlayerRepository>();
services.AddScoped<IBaseRepository<Room>, RoomRepository>();

services.AddTransient<IBasePlayerService, PlayerService>();
services.AddTransient<IBaseRoomService, RoomService>();

services.AddAutoMapper(Assembly.GetExecutingAssembly());

services.AddResponseCompression(config =>
{
    config.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
            new[] { "application/octet-stream" }
        );
});


var app = builder.Build();

app.UseResponseCompression();
app.UseCors(POLICY_APP);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<MisteryNumberHub>("/misterynumber");

await app.RunAsync();
