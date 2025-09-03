using UnityEngine;

public class PlayerIdleState: IPlayerState
{
    public void EnterState(Player player)
    {
        Debug.Log("PlayerMovementIdleState entered");
    }

    public void UpdateState(Player player)
    {
        if (player.CurrentMovementInput.magnitude > 0)
        {
            player.TransitionToState(PlayerState.Walk);
        }
    }

    public void ExitState(Player player)
    {
        // Debug.Log("PlayerMovementIdleState exited");
    }
}
