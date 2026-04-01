namespace BariatricChefApp.Core.Domain.Enums;

/// <summary>
/// Represents the dietary progression stages after bariatric surgery.
/// Patients advance through stages as healing progresses, typically over 8–12 weeks.
/// </summary>
public enum BariatricStage
{
    PreOp = 0,
    Stage1_ClearLiquid = 1,
    Stage2_FullLiquid = 2,
    Stage3_Pureed = 3,
    Stage4_SoftFood = 4,
    Stage5_Regular = 5
}
