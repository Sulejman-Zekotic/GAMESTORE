namespace GameStore.Api.DTOS;

public record  CreateGameDTO
(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

