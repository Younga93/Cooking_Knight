using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Data")] 
    [SerializeField] public EnemyData _enemyData;

    private IEnemyState _currentState;
    private Dictionary<string, IEnemyState> _states;
    
    public Player Player { get; private set; }
    public EnemyConditionController ConditionController { get; private set; }
    public EnemyMovementController MovementController { get; private set; }
    public Animator Animator { get; private set; }

    
    private void Awake()
    {
        ConditionController = GetComponent<EnemyConditionController>();
        MovementController = GetComponent<EnemyMovementController>();
        Animator = GetComponentInChildren<Animator>();
        Player = GameManager.Instance.Player;
        if (_enemyData != null)
        {
            if(ConditionController != null)
            {
                ConditionController.SetMaxHealth(_enemyData.maxHealth);
            }
        }
    }

    private void Start()
    {
        _states = new Dictionary<string, IEnemyState>();
        _states.Add(EnemyState.Idle, new EnemyIdleState());
        _states.Add(EnemyState.Wander, new EnemyWanderState());
        _states.Add(EnemyState.Flee, new EnemyFleeState());
        _states.Add(EnemyState.Dead, new EnemyDeadState());
        _states.Add(EnemyState.Hit, new EnemyHitState());

        _currentState = _states[EnemyState.Flee];
        TransitionToState(EnemyState.Idle);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void TransitionToState(string stateName)
    {
        // if (_currentState == _states[stateName])
        // {
        //     return;
        // }
        if (_currentState is EnemyDeadState)
        {
            return;
        }
        _currentState.ExitState(this);
        _currentState = _states[stateName];
        _currentState.EnterState(this);
    }

    private void Reward()
    {
        //todo. 리워드 제공 로직
        Debug.Log($"{_enemyData.enemyName} Reward");
    }
    
    public void DestroyOnAnimationEnd()
    {
        Reward();
        Debug.Log($"{_enemyData.enemyName} Destroy");
        Destroy(gameObject);
    }
    public void OnHitAnimationEnd()
    {
        if (ConditionController.GetCurrentHealth() <= 0)
        {
            TransitionToState(EnemyState.Dead);
        }
        else
        {
            TransitionToState(EnemyState.Flee);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ConditionController playerCondition = collision.GetComponent<ConditionController>();
            if (playerCondition != null)
            {
                playerCondition.TakeDamage(_enemyData.attack);
            }
        }
    }
}
