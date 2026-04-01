using BariatricChefApp.Api.DTOs;
using BariatricChefApp.Api.Mapping;
using BariatricChefApp.Core.Domain.Enums;
using BariatricChefApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BariatricChefApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RecipesController : ControllerBase
{
    private readonly RecipeService _recipeService;
    private readonly NutritionService _nutritionService;
    private readonly ILogger<RecipesController> _logger;

    public RecipesController(
        RecipeService recipeService,
        NutritionService nutritionService,
        ILogger<RecipesController> logger)
    {
        _recipeService = recipeService;
        _nutritionService = nutritionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RecipeDto>>> GetRecipes(
        [FromQuery] BariatricStage? stage,
        [FromQuery] SurgeryType? surgeryType,
        CancellationToken cancellationToken)
    {
        if (stage is null)
            return BadRequest("The 'stage' query parameter is required.");

        var recipes = await _recipeService.GetRecipesForPatientAsync(stage.Value, surgeryType, cancellationToken);
        return Ok(recipes.ToDtoList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDto>> GetRecipe(
        string id,
        [FromQuery] string stage,
        CancellationToken cancellationToken)
    {
        var recipe = await _recipeService.GetRecipeByIdAsync(id, stage, cancellationToken);
        if (recipe is null) return NotFound();
        return Ok(recipe.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<RecipeDto>> CreateRecipe(
        [FromBody] RecipeCreateRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var recipe = request.ToDomain();
        recipe.CreatedByUserId = userId;

        var validation = _nutritionService.Validate(recipe, recipe.AllowedStages.FirstOrDefault());
        if (!validation.IsValid)
        {
            _logger.LogWarning("Recipe nutrition validation failed: {Issues}", string.Join("; ", validation.Issues));
            Response.Headers["X-Nutrition-Warnings"] = string.Join("; ", validation.Issues);
        }

        var created = await _recipeService.CreateRecipeAsync(recipe, cancellationToken);
        return CreatedAtAction(nameof(GetRecipe), new { id = created.Id, stage = created.Stage }, created.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RecipeDto>> UpdateRecipe(
        string id,
        [FromQuery] string stage,
        [FromBody] RecipeUpdateRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var existing = await _recipeService.GetRecipeByIdAsync(id, stage, cancellationToken);
        if (existing is null) return NotFound();
        if (existing.CreatedByUserId != userId) return Forbid();

        var recipe = request.ToDomain();
        var updated = await _recipeService.UpdateRecipeAsync(id, stage, recipe, cancellationToken);
        if (updated is null) return NotFound();
        return Ok(updated.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(
        string id,
        [FromQuery] string stage,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var existing = await _recipeService.GetRecipeByIdAsync(id, stage, cancellationToken);
        if (existing is null) return NotFound();
        if (existing.CreatedByUserId != userId) return Forbid();

        await _recipeService.DeleteRecipeAsync(id, stage, cancellationToken);
        return NoContent();
    }

    /// <summary>Resolves the authenticated user's stable identifier from B2C claims.</summary>
    private string GetUserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? throw new UnauthorizedAccessException("User identifier claim not found.");
}
