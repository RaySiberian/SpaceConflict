using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Instance { get; private set; }

    public float MoveSpeedScale;
    public float ReproductionTime;

    public string Enemy = "---Enemy---";
    
    public float EnemyMoveSpeedScale;
    public float EnemyReproductionTime;
    public float BotAnalyzeTime;

    public string Upgrades = "---Upgrades---";

    public int ReproductionLvl;
    public int MoveSpeedLvl;


    public string Levels = "---Levels---";
    public int MaxSceneId;
    public int Coins;
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
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
