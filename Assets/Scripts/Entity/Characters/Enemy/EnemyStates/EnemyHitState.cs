using UnityEngine;

public class EnemyHitState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy._enemyData.enemyName}: Hit 상태 진입 (피격 경직)");
        if (enemy.Animator != null)
        {
            enemy.Animator.SetTrigger(AnimatorString.EnemyParameters.Hit);
        }
        
        enemy.MovementController.SetMoveDirection(Vector2.zero);
        enemy.MovementController.Rigidbody2D.velocity = Vector2.zero;
    }

    public void UpdateState(Enemy enemy)
    {
    }

    public void ExitState(Enemy enemy)
    {
    }
}
