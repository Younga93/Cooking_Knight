using UnityEngine;
using UnityEngine.Serialization;

public enum EnemyType
{
    Fleeing,
    Aggressive,
    Hostile
}

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemy Data/Create New")]
public class EnemyData : ScriptableObject
{
    [Header("Information")]
    public string enemyId;
    public string enemyName;
    public EnemyType EnemyType;     //enemy 패턴 타입
    [Header("Condition")]
    public float maxHealth;
    [FormerlySerializedAs("speed")] 
    public float walkSpeed;
    public float runSpeed;
    public float attack;
    [Header("Data")]
    public GameObject prefab;       //일단 프리팹으로 하는데 이미지만 갈아끼우는 방법으로 할 순 없으려나~ 고민해봐야겠음.
}
