using UnityEngine;

public class EnemyDeadState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy._enemyData.enemyName} entered EnemyDeadState");
        enemy.MovementController.SetMoveDirection(Vector2.zero);
        enemy.MovementController.Rigidbody2D.velocity = Vector2.zero;
        enemy.MovementController.Rigidbody2D.isKinematic = true; // 물리 영향 무시
        
        Collider2D[] colliders = enemy.GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
        
        enemy.Animator.SetTrigger(AnimatorString.EnemyParameters.Dead);
    }

    public void UpdateState(Enemy enemy)
    {
        //
    }

    public void ExitState(Enemy enemy)
    {
        //
    }
}