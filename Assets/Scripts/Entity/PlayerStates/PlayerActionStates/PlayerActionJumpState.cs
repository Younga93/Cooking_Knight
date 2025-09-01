using UnityEngine;

public class PlayerActionJumpState : IPlayerActionState
{
    public void EnterState(Player player)
    {
        Debug.Log("PlayerActionJumpState entered");
        player.MovementController.Jump();
        player.PlayerAnimator.SetTrigger(AnimatorString.Parameters.Jump);
        player.PlayerAnimator.SetBool(AnimatorString.Parameters.IsGrounded, false);
    }

    public void UpdateState(Player player)
    {
        //점프 중에도 이동 가능하게.
        Vector2 movementInput = player.PlayerInputActions.Player.Move.ReadValue<Vector2>();
        player.MovementController.SetMovementInput(movementInput);

        //땅에 붙어있거나, 아래로 하강할때만.
        if (player.IsGrounded() && player.MovementController.Rigidbody2D.velocity.y <= 0f)
        {
            Debug.Log("PlayerActionJumpState it is grounded");

            // 상태 머신을 Idle로 전환
            player.TransitionToActionState(PlayerState.Action.Idle);
        }
    }

    public void ExitState(Player player)
    {
        Debug.Log("PlayerActionJumpState Exited");
    }
}
