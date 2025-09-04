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
    public int totalEnemyToDefeatCount;
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

    public Vector2 GetRandomSpawnPosition()
    {
        float halfWidth = spawnSize.x / 2f;
        float halfHeight = spawnSize.y / 2f;
        
        float randomX = Random.Range(spawnCenter.x - halfWidth, spawnCenter.x + halfWidth);
        float randomY = Random.Range(spawnCenter.y - halfHeight, spawnCenter.y + halfHeight);
        
        return new Vector2(randomX, randomY);
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
