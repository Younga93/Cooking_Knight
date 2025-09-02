using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventHandler : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        // 부모 객체에서 Enemy 스크립트를 찾아서 참조
        _enemy = GetComponentInParent<Enemy>();
    }

    // 애니메이션 이벤트가 호출할 메서드
    public void OnDeadAnimationEnd()
    {
        // 부모의 Enemy 스크립트에 있는 DestroyOnAnimationEnd() 메서드 호출
        _enemy.DestroyOnAnimationEnd();
    }

    public void OnHitAnimationEnd()
    {
        _enemy.OnHitAnimationEnd();
    }
}