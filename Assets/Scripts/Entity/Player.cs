using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Inventory _inventory;
    
    //Player State: Attack, Movement
    private IPlayerMovementState _movementState;
    private IPlayerActionState _actionState;
    
    //Player 상태 캐싱 //Key 값은 Animation이랑 맞춰서 쓰면 될듯.
    private Dictionary<string, IPlayerMovementState> _movementStates;
    private Dictionary<string, IPlayerActionState> _actionStates;

    //Player Controller들
    public PlayerMovementController MovementController { get; private set; }
    public PlayerAttackController AttackController { get; private set; }
    public ConditionController ConditionController { get; private set; }
    
    public PlayerInput PlayerInputActions { get; private set; } 
    
    public Animator PlayerAnimator { get; private set; }
    
    [Header("Ground check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;
    
    
    private void Awake()
    {
        MovementController = GetComponent<PlayerMovementController>();
        AttackController = GetComponentInChildren<PlayerAttackController>();
        ConditionController = GetComponent<ConditionController>();
        
        PlayerInputActions = new PlayerInput();
        
        PlayerAnimator = GetComponentInChildren<Animator>();
        
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
         
         _inventory = GetComponent<Inventory>();
     }

     private void OnEnable()
     {
         PlayerInputActions.Player.Move.performed += OnMove;
         PlayerInputActions.Player.Move.canceled += OnMove;
         PlayerInputActions.Player.Jump.performed += OnJump;
         PlayerInputActions.Player.Attack.performed += OnAttack;
         PlayerInputActions.Player.Attack.canceled += OnAttack;
         
         PlayerInputActions.Enable();
     }

     private void OnDisable()
     {
         PlayerInputActions.Player.Move.performed -= OnMove;
         PlayerInputActions.Player.Move.canceled -= OnMove;
         PlayerInputActions.Player.Jump.performed -= OnJump;
         PlayerInputActions.Player.Attack.performed -= OnAttack;
         PlayerInputActions.Player.Attack.canceled -= OnAttack;

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
         if (IsGrounded())
         {
             // 땅에 닿았을 때만
             // 애니메이터 isGrounded 파라미터를 true로 설정
             PlayerAnimator.SetBool(AnimatorString.Parameters.IsGrounded, true);
             
             TransitionToActionState(PlayerState.Action.Jump);
         }
     }

     public void OnAttack(InputAction.CallbackContext context)
     {
         // if (context.started) //첫 프레임에만 실행하도록
         // {
             // if (AttackController.CanAttack()) // && !(_actionState is PlayerActionDeadState))
             // {
                 TransitionToActionState(PlayerState.Action.Attack);
             // }
         // }
     }

     public void OnHit()
     {
         //todo 피격상태로 전환하기
     }

     public void OnDead()
     {
         //todo 사망상태로 전환하기
     }
     
     public void TransitionToMovementState(string stateName)
    {
        if (_movementState == _movementStates[stateName]) return;
        
        IsGrounded();
        _movementState.ExitState(this);
        _movementState = _movementStates[stateName];
        _movementState.EnterState(this);
    }
    
    public void TransitionToActionState(string stateName)
    {
        if (_actionState == _actionStates[stateName]) return;

        if (PlayerAnimator != null)
        {
            Debug.Log("PlayerAnimator trigger들 초기화됨");
            PlayerAnimator.ResetTrigger(AnimatorString.Parameters.Jump);
            PlayerAnimator.ResetTrigger(AnimatorString.Parameters.Hit);
            PlayerAnimator.ResetTrigger(AnimatorString.Parameters.Dead);
        }
        _actionState.ExitState(this);
        _actionState = _actionStates[stateName];
        _actionState.EnterState(this);
    }
    
    public bool IsGrounded()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            PlayerAnimator.SetBool(AnimatorString.Parameters.IsGrounded, true);
        }
        return isGrounded;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 디버깅용: Scene 뷰에 GroundCheck 원을 그려주는 코드
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
#endif
}
