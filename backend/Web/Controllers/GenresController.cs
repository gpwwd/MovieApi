using Application.Dtos.GetDtos;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GenreDto>>> GetAll()
    {
        var genres = await _genreService.GetAllAsync();
        return Ok(genres);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GenreDto>> GetById(Guid id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        return Ok(genre);
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<GenreDto>> GetByName(string name)
    {
        var genre = await _genreService.GetByNameAsync(name);
        return Ok(genre);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<GenreDto>> Create([FromBody] string name)
    {
        var genre = await _genreService.CreateAsync(name);
        return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Update(Guid id, [FromBody] string name)
    {
        await _genreService.UpdateAsync(id, name);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _genreService.DeleteAsync(id);
        return NoContent();
    }
} 