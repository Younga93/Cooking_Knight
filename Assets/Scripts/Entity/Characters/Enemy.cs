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
    public ConditionController ConditionController { get; private set; }
    public EnemyMovementController MovementController { get; private set; }
    public Animator Animator { get; private set; }

    
    private void Awake()
    {
        ConditionController = GetComponent<ConditionController>();
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

        _currentState.ExitState(this);
        _currentState = _states[stateName];
        _currentState.EnterState(this);
    }
}
