using GameStore.Api;
using GameStore.Api.DTOS;

const string EndpointGetGameName = "GetGame";
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDTO> games = [
  new GameDTO(1,"Street Fighter I","Fighting",19.99M,new DateOnly(1992,7,12)),  
  new GameDTO(2,"Max Payne 3","RPG",22.99M,new DateOnly(2012,4,16)),  
  new GameDTO(3,"EFtubal","Sport",40.99M,new DateOnly(2024,1,2)),  
  new GameDTO(4,"Mario Bros","Platformer",5M,new DateOnly(1987,11,24)),  
];
app.MapGet("/games", () => games);


app.MapGet("/games/{id}",(int id)=>{
    
    
   var game= games.Find(game=>game.Id==id);
   return game is null ? Results.NotFound() : Results.Ok(game);
    
    }).WithName(EndpointGetGameName);


app.MapPost("/games",(CreateGameDTO newGame) =>
{
    GameDTO game = new GameDTO(
        (games.Last().Id + 1),
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
        );
    games.Add(game);
    return Results.CreatedAtRoute(EndpointGetGameName,new {id= game.Id},game);
});
app.MapPut("games/{id}",(UpdatedGameDTO updatedgame,int id) =>
{
    var gameIndex=games.FindIndex(game=>game.Id==id);
    if(gameIndex==-1){
     return Results.NotFound();
    }
    games[gameIndex]=new GameDTO(id,updatedgame.Name,updatedgame.Genre,updatedgame.Price,updatedgame.ReleaseDate);
    return Results.Ok(games[gameIndex]);
});
app.MapDelete("games/{id}",(int id) =>
{ 
   games.RemoveAll(game=>game.Id==id);
      return Results.NoContent();
});
app.Run();
