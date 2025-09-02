using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConditionController : ConditionController
{
    private Enemy _enemy;

    protected override void Awake()
    {
        base.Awake();
        _enemy = GetComponent<Enemy>();
    }
    
     protected override void OnDeath()
     {        
         // if (_enemy != null)
         // {
         //     _enemy.TransitionToState(EnemyState.Dead);
         // }
         // else
         // {
         //     Debug.LogError("Enemy 스크립트를 찾을 수 없습니다.");
         // }
     }
}
