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
    private float _additionalAttackPower;
    [SerializeField] private Transform hitBox;
    [SerializeField] private LayerMask enemyLayers;
    
    private Animator _animator;
    private Player _player;

    private float attackCooldown;
    public bool CanAttack { get; set; }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponentInParent<Player>();
        CanAttack = true;
    }

    private void Start()
    {
        _additionalAttackPower = PlayerManager.Instance.additionalAttackPower;
    }

    public void SetAdditionalAttackPower(float dmg)
    {
        _additionalAttackPower = dmg;
    }
    public void AddAttackDmg(float dmg)
    {
        _additionalAttackPower += dmg;
    }
    private void FixedUpdate()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }
    public void StartAttack()
    {
        CanAttack = false;
        // AudioManager.Instance.PlayAttackSoundEffect(); // 현재 쿨다운이 없나요? 소리가 계속 재생되는 것 같아서요.
        Debug.Log("StartAttack()이 호출되었습니다.");
        //1번 레이어(Action Layer)에서 재생하기
        _animator.Play(AnimatorString.PlayerAnimation.Attack, 1);

        attackCooldown = 1f / attackSpeed;
        StartCoroutine(AttackCooldownCoroutine(attackCooldown));
        
        //todo. 추후 애니메이션 이벤트로 처리하기
        // float animationLength = _animator.GetCurrentAnimatorStateInfo(1).length;
        // Invoke(nameof(OnAttackAnimationEnd), animationLength);
    }

    private IEnumerator AttackCooldownCoroutine(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        CanAttack = true;
    }

    public void OnAttackHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);
        AudioManager.Instance.PlayAttackSoundEffect();//현재 구조로는 이 곳에 있어야함. 이후 PlayerAttackState에 State 전환이 완료되면 옮길 예정.
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.ConditionController.TakeDamage(attackPower + _additionalAttackPower);
            }
        }
        _player.TransitionToState(PlayerState.Idle);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 디버깅용: Scene 뷰에 GroundCheck 원을 그려주는 코드
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitBox.position, attackRange);
    }
#endif
}
