using System;
using GameStore.Api.Data;
using GameStore.Api.DTOS;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenreEndpoints
{
   public static void MapGenreEndpoints(this WebApplication app)
    {   
          var group=app.MapGroup("/genres");
           group.MapGet("/",async (GameStoreContext dbContext)=>{
        
                      var genres= await dbContext.Genres
                                    .Select(genre=>new GenreDTO(genre.id,genre.Name))
                                    .AsNoTracking()
                                    .ToListAsync();
                         return Results.Ok(genres);
      });
 
    }
}
