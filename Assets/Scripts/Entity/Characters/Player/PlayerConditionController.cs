using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditionController : ConditionController
{
    
    public override void TakeDamage(float amount)
    {
        
        base.TakeDamage(amount);
    }

    protected override void OnDeath()
    {
        //todo. Player Death 구현
    }
}
