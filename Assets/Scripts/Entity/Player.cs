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
    
    [Header("Ground check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;
    
    
    private void Awake()
    {
        MovementController = GetComponent<PlayerMovementController>();
        AttackController = GetComponent<PlayerAttackController>();
        PlayerCondition = GetComponent<PlayerCondition>();
        
        PlayerInputActions = new PlayerInput();
        
        //이동 상태 머신 초기화
        _movementStates = new Dictionary<string, IPlayerMovementState>();
        _movementStates.Add(PlayerState.Movement.Idle, new PlayerMovementIdleState());
        _movementStates.Add(PlayerState.Movement.Walk, new PlayerMovementWalkState());
        
        //액션 상태 머신 초기화
        _actionStates = new Dictionary<string, IPlayerActionState>();
        _actionStates.Add(PlayerState.Action.Idle, new PlayerActionIdleState());
        _actionStates.Add(PlayerState.Action.Jump, new PlayerActionJumpState());
        _actionStates.Add(PlayerState.Action.Attack, new PlayerActionAttackState());
    }

     private void Start()
     {
         _movementState = _movementStates[PlayerState.Movement.Idle];
         _movementState.EnterState(this);

         _actionState = _actionStates[PlayerState.Action.Idle];
         _actionState.EnterState(this);
     }

     private void OnEnable()
     {
         PlayerInputActions.Player.Move.performed += OnMove;
         PlayerInputActions.Player.Move.canceled += OnMove;
         PlayerInputActions.Player.Jump.performed += OnJump;
         PlayerInputActions.Player.Attack.performed += OnAttack;
         
         PlayerInputActions.Enable();
     }

     private void OnDisable()
     {
         PlayerInputActions.Player.Move.performed -= OnMove;
         PlayerInputActions.Player.Move.canceled -= OnMove;
         PlayerInputActions.Player.Jump.performed -= OnJump;
         PlayerInputActions.Player.Attack.performed -= OnAttack;

         PlayerInputActions.Disable();
     }
     
     private void Update()
     {
         _movementState.UpdateState(this);
         _actionState.UpdateState(this);
     }
     
     public void OnMove(InputAction.CallbackContext context)
     {
         Vector2 movementInput = context.ReadValue<Vector2>();
         if (movementInput.magnitude > 0)
         {
             TransitionToMovementState(PlayerState.Movement.Walk);
         }
         else
         {
             TransitionToMovementState(PlayerState.Movement.Idle);
         }
         MovementController.SetMovementInput(movementInput);
     }

     public void OnJump(InputAction.CallbackContext context)
     {
         TransitionToActionState(PlayerState.Action.Jump);
     }

     public void OnAttack(InputAction.CallbackContext context)
     {
         TransitionToActionState(PlayerState.Action.Attack);
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
        if (_actionState == _actionStates[stateName]) return;
        
        _actionState.ExitState(this);
        _actionState = _actionStates[stateName];
        _actionState.EnterState(this);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
