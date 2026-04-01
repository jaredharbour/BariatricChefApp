using BariatricChefApp.Core.Domain.Enums;
using BariatricChefApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BariatricChefApp.Api.Controllers;

[ApiController]
[Route("api/nutrition")]
[Authorize]
public class NutritionController : ControllerBase
{
    private readonly NutritionService _nutritionService;

    public NutritionController(NutritionService nutritionService)
    {
        _nutritionService = nutritionService;
    }

    [HttpGet("stages")]
    public ActionResult<IEnumerable<object>> GetStageGuidelines()
    {
        var stages = Enum.GetValues<BariatricStage>()
            .Select(stage => _nutritionService.GetGuidelinesForStage(stage));
        return Ok(stages);
    }

    [HttpGet("stages/{stage}")]
    public ActionResult<object> GetStageGuideline(BariatricStage stage)
    {
        return Ok(_nutritionService.GetGuidelinesForStage(stage));
    }
}
