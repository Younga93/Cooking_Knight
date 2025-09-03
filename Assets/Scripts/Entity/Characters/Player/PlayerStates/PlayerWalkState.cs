using UnityEngine;

public class PlayerWalkState: IPlayerState
{
    public void EnterState(Player player)
    {
        // Debug.Log("PlayerMovementWalkState entered");
        player.PlayerAnimator.SetBool(AnimatorString.PlayerParameters.IsWalking, true);
    }

    public void UpdateState(Player player)
    {
        if (player.CurrentMovementInput.magnitude <= 0)
        {
            player.TransitionToState(PlayerState.Idle);
        }
    }

    public void ExitState(Player player)
    {
        player.PlayerAnimator.SetBool(AnimatorString.PlayerParameters.IsWalking, false);
        // Debug.Log("PlayerMovementWalkState exited");
    }
}