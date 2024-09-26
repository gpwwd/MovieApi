﻿using MovieApiMvc.Models.Dtos.GetDtos;

namespace MovieApiMvc.Models.Dtos.PostDtos;

public record PostMovieDto
{
    public required string Name { get; init; }
    public string? AlternativeName { get; init; } = string.Empty;
    public RatingPostDto? Rating { get; init; }//сделать контроллер рейтингов, предоставить возможность выбирать только из 10 вариантов
    public required string Type { get; init; }
    public required int Year { get; init; }
    public BudgetPostDto Budget { get; init; } = new BudgetPostDto();//создаем новый
    public int MovieLength { get; init; }
    public List<string> GenresNames { get; init; } = null!;
    public List<string> CountriesNames { get; init; } = null!;
    public int Top250 { get; init; }
    public bool IsSeries { get; init; }
    public ImagePostDto? ImageInfo { get; init; }// создаем новую картинку
    public string? Description { get; init; }
    public string? ShortDescription { get; init; }
}