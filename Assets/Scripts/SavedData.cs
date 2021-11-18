using System;

[Serializable]
public class SavedData
{
    public float MoveSpeedScale;
    public float ReproductionTime;

    public float EnemyMoveSpeedScale;
    public float EnemyReproductionTime;
    public float BotAnalyzeTime;

    public int ReproductionLvl;
    public int MoveSpeedLvl;

    public SavedData(Data data)
    {
        MoveSpeedScale = data.MoveSpeedScale;
        ReproductionTime = data.ReproductionTime;

        EnemyMoveSpeedScale = data.EnemyMoveSpeedScale;
        EnemyReproductionTime = data.EnemyReproductionTime;
        BotAnalyzeTime = data.BotAnalyzeTime;

        ReproductionLvl = data.ReproductionLvl;
        MoveSpeedLvl = data.MoveSpeedLvl;
    }
}