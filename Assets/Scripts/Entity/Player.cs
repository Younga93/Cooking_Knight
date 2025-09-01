using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    
    public PlayerInput PlayerInputActions { get; private set; } 

    private void Awake()
    {
        MovementController = GetComponent<PlayerMovementController>();
        AttackController = GetComponent<PlayerAttackController>();
        PlayerCondition = GetComponent<PlayerCondition>();
        
        PlayerInputActions = new PlayerInput();
        
        _movementStates = new Dictionary<string, IPlayerMovementState>();
        _actionStates = new Dictionary<string, IPlayerActionState>();
        
        _movementStates.Add("Idle", new PlayerMovementIdleState()); //todo. 하드코딩 부분 Constants으로 변경하기 
        _movementStates.Add("Walk", new PlayerMovementWalkState());
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

     private void OnEnable()
     {
         PlayerInputActions.Player.Move.performed += OnMove;
         
         PlayerInputActions.Enable();
     }

     private void OnDisable()
     {
         PlayerInputActions.Player.Move.performed -= OnMove;

         PlayerInputActions.Disable();
     }

     public void OnMove(InputAction.CallbackContext context)
     {
         Vector2 movementInput = context.ReadValue<Vector2>();
         if (movementInput.magnitude > 0)
         {
             TransitionToMovementState("Walk");
         }
         else
         {
             TransitionToMovementState("Idle");
         }
         MovementController.SetMovementInput(movementInput);
     }
     public void TransitionToMovementState(string stateName)
    {
        if (_movementState == _movementStates[stateName]) return;
        
        _movementState.ExitState(this);
        _movementState = _movementStates[stateName];
        _movementState.EnterState(this);
    }
    
    public void TransitionToActionState(string stateName)
    {
        //todo.
        //현재 무브먼트 exit
        //현재 무브먼트 매개변수로 변경한 뒤
        //새 무브먼트로 enter
    }
}
