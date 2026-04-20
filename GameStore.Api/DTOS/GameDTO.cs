using System;

namespace GameStore.Api.DTOS;

public record GameDTO
(
    int id,
    string Name,
    string  Genre,
    decimal Price,
    DateOnly ReleaseDate
);
