using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    // 단순히 Player만 들고 있을 싱글턴입니다.
    // 다른 곳에 넣는 것 보다 염예찬 튜터님의 코드처럼 Player 만을 들고있을 녀석을 따로 만드는게 깔끔해보이더라구요.
    
    [SerializeField] private Dictionary<SceneType, Vector2> spawnPositions = new();

    public Player player;

    public float additionalAttackPower;
    public float additionalMaxHealth;
    public void AddAttackPower(float dmg)
    {
        additionalAttackPower += dmg;
        player.AttackController.SetAdditionalAttackPower(additionalAttackPower);
    }

    public void AddAdditionalMaxHealth(float amount)
    {
        additionalMaxHealth += amount;
        player.ConditionController.AddMaxHealth(amount);
    }
    // public void SpawnPlayer(SceneType type){
    //  Instantiate(player, spawnPositions[type], Quaternion.identity);
    // }
    //
    // public void DeSpawnPlayer(){
    //     Destroy(player);
    // }
}
