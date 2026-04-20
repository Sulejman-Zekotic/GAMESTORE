using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace GameStore.Api;

public record GameDetailsDTO
(
  
  int Id ,
  string Name,
  int GenreId,
  decimal Price,
  DateOnly ReleaseDate

);
