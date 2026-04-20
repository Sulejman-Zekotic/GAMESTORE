using System.Collections.Concurrent;
using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.DTOS;
using GameStore.Api.Endpoints;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.AddGameStoreDb();
var app = builder.Build();
app.MapGamesEndpoint();
app.MigrateDb();
app.Run();
