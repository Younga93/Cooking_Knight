using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public void EnterState(Player player)
    {
        // Debug.Log("PlayerActionJumpState entered");
        player.MovementController.Jump();
        AudioManager.Instance.PlayJumpSoundEffect();
        player.PlayerAnimator.SetTrigger(AnimatorString.PlayerParameters.Jump);
        player.PlayerAnimator.SetBool(AnimatorString.PlayerParameters.IsGrounded, false);
    }

    public void UpdateState(Player player)
    {
        if (player.IsGrounded() && player.MovementController.Rigidbody2D.velocity.y <= 0f)
        {
            if (player.CurrentMovementInput.magnitude > 0)
            {
                player.TransitionToState(PlayerState.Walk);
            }
            else
            {
                player.TransitionToState(PlayerState.Idle);
            }
        }
    }

    public void ExitState(Player player)
    {
        // Debug.Log("PlayerActionJumpState Exited");
    }
}
