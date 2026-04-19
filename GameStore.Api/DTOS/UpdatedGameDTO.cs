namespace GameStore.Api.DTOS;

public record  UpdatedGameDTO
(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

