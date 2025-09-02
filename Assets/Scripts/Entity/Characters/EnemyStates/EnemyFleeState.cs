using UnityEngine;

public class EnemyFleeState : IEnemyState
{
    private float _fleeTimer;
    private const float FLEE_TIME = 3f;
    
    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy._enemyData.enemyName} entered FleeState");
        enemy.MovementController.SetCurrentSpeed(enemy._enemyData.runSpeed);
        _fleeTimer = FLEE_TIME;
    }

    public void UpdateState(Enemy enemy)
    {
        //todo. 도망 행동 구현하기.
        Vector2 fleeDirection = (enemy.transform.position - enemy.Player.transform.position).normalized;

        enemy.MovementController.SetMoveDirection(fleeDirection);
        
        _fleeTimer -= Time.deltaTime;
        if (_fleeTimer <= 0)
        {
            enemy.TransitionToState(EnemyState.Idle);
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log($"{enemy._enemyData.enemyName} exit FleeState");
        enemy.MovementController.SetMoveDirection(Vector2.zero);
    }
}
