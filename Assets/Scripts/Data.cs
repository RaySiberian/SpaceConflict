using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBuffs", menuName = "ScriptableObjects/Buff", order = 1)]
public class Data : ScriptableObject
{
    public float MoveSpeedScale;
    public float ReproductionTime;
    public float EnemyMoveSpeedScale;
    public float EnemyReproductionTime;
}
