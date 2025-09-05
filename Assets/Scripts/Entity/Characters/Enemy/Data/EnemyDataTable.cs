using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTable_", menuName = "Enemy Data/Create New Table")]
public class EnemyDataTable : ScriptableObject
{
    public List<EnemyData> enemies;
}
