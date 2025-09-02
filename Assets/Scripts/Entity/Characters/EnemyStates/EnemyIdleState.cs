using UnityEngine;

public class EnemyIdleState: IEnemyState
{
    private float _idleTimer;
    private const float MIN_IDLE_TIME = 2f;
    private const float MAX_IDLE_TIME = 3f;
    public void EnterState(Enemy enemy)
    {
        Debug.Log($"{enemy._enemyData.enemyName} entered EnemyIdleState");
        _idleTimer = Random.Range(MIN_IDLE_TIME, MAX_IDLE_TIME);
    }

    public void UpdateState(Enemy enemy)
    {
        _idleTimer -= Time.deltaTime;
        if (_idleTimer <= 0f)
        {
            enemy.TransitionToState(EnemyState.Wander);
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log($"{enemy._enemyData.enemyName} exit EnemyIdleState");
    }
}