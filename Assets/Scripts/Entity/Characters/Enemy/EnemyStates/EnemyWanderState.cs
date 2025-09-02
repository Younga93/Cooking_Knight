using UnityEngine;

public class EnemyWanderState : IEnemyState
{
    private Vector2 _wanderDirection;
    private float _wanderTimer;
    private const float MIN_WANDER_TIME = 2f;
    private const float MAX_WANDER_TIME = 3f;
    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy._enemyData.enemyName} entered EnemyWanderState");
        
        enemy.MovementController.SetCurrentSpeed(enemy._enemyData.walkSpeed);
        enemy.Animator.SetBool(AnimatorString.EnemyParameters.IsWalking, true);
        
        SetRandomWanderDirection();
    }

    public void UpdateState(Enemy enemy)
    {
        //todo. 도망 행동 구현하기.
        _wanderTimer -= Time.deltaTime;
        if (_wanderTimer <= 0f)
        {
            enemy.TransitionToState(EnemyState.Idle);
            return;
        }

        enemy.MovementController.SetMoveDirection(_wanderDirection);
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log($"{enemy._enemyData.enemyName} exit EnemyWanderState");
        enemy.MovementController.SetMoveDirection(Vector2.zero);
        enemy.Animator.SetBool(AnimatorString.EnemyParameters.IsWalking, false);
    }

    private void SetRandomWanderDirection()
    {
        float randomX = Random.Range(0, 2) == 0 ? -1f : 1f;
        _wanderDirection = new Vector2(randomX, 0);
        
        _wanderTimer = Random.Range(MIN_WANDER_TIME, MAX_WANDER_TIME);
    }
}