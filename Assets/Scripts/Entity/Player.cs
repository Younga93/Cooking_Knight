using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //, IShopObserver?
{
    //Player State: Attack, Movement
    private IPlayerMovementState _movementState;
    private IPlayerActionState _actionState;
    
    //Player 상태 캐싱 //Key 값은 Animation이랑 맞춰서 쓰면 될듯.
    private Dictionary<string, IPlayerMovementState> _movementStates;
    private Dictionary<string, IPlayerActionState> _actionStates;

    //Player Controller들
    public PlayerMovementController MovementController { get; private set; }
    public PlayerAttackController AttackController { get; private set; }
    public PlayerCondition PlayerCondition { get; private set; }

    private void Awake()
    {
        MovementController = GetComponent<PlayerMovementController>();
        AttackController = GetComponent<PlayerAttackController>();
        PlayerCondition = GetComponent<PlayerCondition>();
        
        _movementStates = new Dictionary<string, IPlayerMovementState>();
        _actionStates = new Dictionary<string, IPlayerActionState>();
        
        _movementStates.Add("Idle", new PlayerMovementIdleState());
    }

     private void Start()
     {
         // todo. 현재 상태 idle로 초기화. 이후 Enter까지.
         _movementState = _movementStates["Idle"];
         _movementState.EnterState(this);
         // _actionState
     }

     private void Update()
     {
         _movementState.UpdateState(this);
     }

    public void TransitionToMovementState(string stateName)
    {
        if (_movementState == _movementStates[stateName]) return;
        
        _movementState.ExitState(this);
        _movementState = _movementStates[stateName];
        _movementState.EnterState(this);
    }
    
    public void TransitionToActionState(IPlayerActionState newActionState)
    {
        //todo.
        //현재 무브먼트 exit
        //현재 무브먼트 매개변수로 변경한 뒤
        //새 무브먼트로 enter
    }
}
