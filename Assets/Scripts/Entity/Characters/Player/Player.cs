using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Player State: Attack, Movement
    private IPlayerState _currentState;
    
    //Player 상태 캐싱 //Key 값은 Animation이랑 맞춰서 쓰면 될듯.
    //todo. 리팩토링: State 두종류로 나누지 말고 그냥 하나로 합치기. //했는데, 
    private Dictionary<string, IPlayerState> _states;

    private Vector2 _currentMovementInput;
    public Vector2 CurrentMovementInput => _currentMovementInput;
    
    //Player Controller들
    public PlayerMovementController MovementController { get; private set; }
    public PlayerAttackController AttackController { get; private set; }
    public PlayerConditionController ConditionController { get; private set; }
    
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
        ConditionController = GetComponent<PlayerConditionController>();
        
        PlayerInputActions = new PlayerInput();
        
        PlayerAnimator = GetComponentInChildren<Animator>();
        
        //상태 머신 초기화
        _states = new Dictionary<string, IPlayerState>();
        _states.Add(PlayerState.Idle, new PlayerIdleState());
        _states.Add(PlayerState.Walk, new PlayerWalkState());
        _states.Add(PlayerState.Jump, new PlayerJumpState());
        _states.Add(PlayerState.Attack, new PlayerAttackState());
        _states.Add(PlayerState.Hit, new PlayerHitState());
        _states.Add(PlayerState.Dead, new PlayerDeadState());
    }

     private void Start()
     {
         _currentState = _states[PlayerState.Idle];
         _currentState.EnterState(this);
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
         _currentState.UpdateState(this);
     }
     
     public void OnMove(InputAction.CallbackContext context)
     {
         _currentMovementInput = context.ReadValue<Vector2>();
     }

     public void OnJump(InputAction.CallbackContext context)
     {
         if (IsGrounded())
         {
             // 땅에 닿았을 때만
             // 애니메이터 isGrounded 파라미터를 true로 설정
             PlayerAnimator.SetBool(AnimatorString.PlayerParameters.IsGrounded, true);
             
             TransitionToState(PlayerState.Jump);
         }
     }

     public void OnAttack(InputAction.CallbackContext context)
     {
         if (AttackController.CanAttack() && !(_currentState is PlayerDeadState))
         {
             TransitionToState(PlayerState.Attack);
         }
     }

     public void OnHit()
     {
         //todo 피격상태로 전환하기
         TransitionToState(PlayerState.Hit);
     }

     public void OnDead()
     {
         //todo 사망상태로 전환하기
         TransitionToState(PlayerState.Dead);
     }
     
     public void TransitionToState(string stateName)
    {
        if (_currentState is PlayerDeadState) return;
        
        IsGrounded();
        _currentState.ExitState(this);
        _currentState = _states[stateName];
        _currentState.EnterState(this);
    }
    
    // public void TransitionToActionState(string stateName)
    // {
    //     if (_currentState == _states[stateName]) return;
    //
    //     if (PlayerAnimator != null)
    //     {
    //         Debug.Log("PlayerAnimator trigger들 초기화됨");
    //         PlayerAnimator.ResetTrigger(AnimatorString.PlayerParameters.Jump);
    //         PlayerAnimator.ResetTrigger(AnimatorString.PlayerParameters.Hit);
    //         PlayerAnimator.ResetTrigger(AnimatorString.PlayerParameters.Dead);
    //     }
    //     _currentState.ExitState(this);
    //     _currentState = _states[stateName];
    //     _currentState.EnterState(this);
    // }
    
    public bool IsGrounded()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            PlayerAnimator.SetBool(AnimatorString.PlayerParameters.IsGrounded, true);
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
