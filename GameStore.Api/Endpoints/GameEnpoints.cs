using System;
using GameStore.Api.Data;
using GameStore.Api.DTOS;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Api.Endpoints;

public static class Endpoints
{

    public static void MapGamesEndpoint(this WebApplication app)
    {
        var group=app.MapGroup("/games");
          const string EndpointGetGameName = "GetGame";

                    group.MapGet("/", async(GameStoreContext dbContext ) =>
                    {
                           var games= await dbContext.Games.
                                     Include(game=>game.Genre)
                                    .Select(game=>new GameDTO(game.Id,game.Name,game.Genre.Name,game.Price,game.ReleaseDate))
                                    .AsNoTracking()
                                    .ToListAsync();
                         return Results.Ok(games);
                    } );


                    group.MapGet("/{id}",async(int id,GameStoreContext dbContext )=>{
                    var game= await dbContext.Games.FindAsync(id);
                    return game is null ? Results.NotFound() : Results.Ok(
                        new GameDetailsDTO(game.Id,game.Name,game.GenreId,game.Price,game.ReleaseDate)
                    );
                        
                        }).WithName(EndpointGetGameName);


                    group.MapPost("/",async (CreateGameDTO newGame,GameStoreContext dbContext) =>
                    {
                        Game game= new Game
                        {
                            
                            Name=newGame.Name,
                            GenreId=newGame.GenreId,
                            Price=newGame.Price,
                            ReleaseDate=newGame.ReleaseDate

                        };
                        dbContext.Games.Add(game);
                        await dbContext.SaveChangesAsync();
                        GameDetailsDTO gameDetails=new (
                            game.Id,
                            game.Name,
                            game.GenreId,
                            game.Price,
                            game.ReleaseDate
                        );
                        return Results.CreatedAtRoute(EndpointGetGameName,new {id= game.Id},gameDetails);
                    });
                    group.MapPut("/{id}",async (UpdatedGameDTO updatedgame,int id,GameStoreContext dbContext) =>
                    {
                        var existingGame=await dbContext.Games.FindAsync(id);
                        if(existingGame is null){
                        return Results.NotFound();
                        }
                        existingGame.Name=updatedgame.Name;
                        existingGame.GenreId=updatedgame.GenreId;
                        existingGame.Price=updatedgame.Price;
                        existingGame.ReleaseDate=updatedgame.ReleaseDate;
                        await dbContext.SaveChangesAsync();
                        return Results.NoContent();
                    });
                    group.MapDelete("/{id}",async (int id,GameStoreContext dbContext) =>
                    { 
                        await dbContext.Games
                                        .Where(game=>game.Id==id)
                                        .ExecuteDeleteAsync();
                        return Results.NoContent();
                    });

    }

}
