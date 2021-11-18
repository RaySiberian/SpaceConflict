using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBuffs", menuName = "ScriptableObjects/Buff", order = 1)]
public class Data : ScriptableObject
{
    public float MoveSpeedScale;
    public float ReproductionTime;

    public string Enemy = "---Enemy---";
    
    public float EnemyMoveSpeedScale;
    public float EnemyReproductionTime;
    public float BotAnalyzeTime;

    public string Upgrades = "---Upgrades---";

    public int ReproductionLvl;
    public int MoveSpeedLvl;

    public void Save()
    {
        SaveSystem.SaveCurrentData(this);
    }

    public void Load()
    {
        SavedData savedData = SaveSystem.LoadData();
        MoveSpeedScale = savedData.MoveSpeedScale;
        ReproductionTime = savedData.ReproductionTime;
        
        EnemyMoveSpeedScale = savedData.EnemyMoveSpeedScale;
        EnemyReproductionTime = savedData.EnemyReproductionTime;
        BotAnalyzeTime = savedData.BotAnalyzeTime;
        
        ReproductionLvl = savedData.ReproductionLvl;
        MoveSpeedLvl = savedData.MoveSpeedLvl;
    }
    
}
