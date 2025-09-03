using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// [System.Serializable]
// public class EnemyCount //이름 진짜 맘에 안드네
// {
//     public EnemyData enemyData;
//     public int count;
// }

[CreateAssetMenu(fileName = "Stage_", menuName = "Stage Data/Create New")]
public class StageData : ScriptableObject
{
    [Header("Information")]
    public int stageId;
    public int maxEnemyCount;
    public float respawnTime;
    [Header("Enemies")]
    public List<EnemyData> enemies;
    [Header("Spawn Area")]
    public Vector2 spawnCenter;
    public Vector2 spawnSize;

    public EnemyData GetRandomEnemyFromList()
    {
        int randomIndex = Random.Range(0, enemies.Count);
        return enemies[randomIndex];
    }
    // public void OnValidate()
    // {
    //     maxEnemyCount = 0;
    //     foreach(var enemy in enemies)
    //     {
    //         maxEnemyCount += enemy.count;
    //     }
    // }
}
