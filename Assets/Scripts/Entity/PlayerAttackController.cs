using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackPower = 20f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    
    private Animator _animator;
    private Player _player;

    private float attackCooldown;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponentInParent<Player>();
    }

    private void FixedUpdate()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
            if(attackCooldown<= 0) Debug.Log("Attack available");
        }
    }

    public bool CanAttack()
    {
        return true;
        // return attackCooldown <= 0;

    }
    public void StartAttack()
    {
        Debug.Log("StartAttack()이 호출되었습니다.");
        //1번 레이어(Action Layer)에서 재생하기
        _animator.Play(AnimatorString.PlayerAnimation.Attack, 1);

        attackCooldown = 1f / attackSpeed;
        
        //todo. 추후 애니메이션 이벤트로 처리하기
        // float animationLength = _animator.GetCurrentAnimatorStateInfo(1).length;
        // Invoke(nameof(OnAttackAnimationEnd), animationLength);
    }

    public void OnAttackHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            ConditionController enemyCondition = enemy.GetComponent<ConditionController>();
            if (enemyCondition != null)
            {
                enemyCondition.TakeDamage(attackPower);
            }
        }

        OnAttackAnimationEnd();
    }

    public void OnAttackAnimationEnd()
    {
        _animator.Play("Empty", 1);
        _player.TransitionToActionState(PlayerState.Action.Idle);
    }
}
