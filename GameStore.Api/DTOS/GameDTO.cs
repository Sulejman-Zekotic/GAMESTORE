using Microsoft.AspNetCore.SignalR;

namespace GameStore.Api;

public record GameDTO
(
  
  int Id ,
  string Name,
  string Genre,
  decimal Price,
  DateOnly ReleaseDate

);
