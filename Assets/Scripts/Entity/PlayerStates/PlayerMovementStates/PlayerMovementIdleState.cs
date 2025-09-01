using UnityEngine;

public class PlayerMovementIdleState: IPlayerMovementState
{
    public void EnterState(Player player)
    {
        Debug.Log("PlayerMovementIdleState entered");
        player.MovementController.SetMovementInput(Vector2.zero);
    }

    public void UpdateState(Player player)
    {
    }

    public void ExitState(Player player)
    {
        Debug.Log("PlayerMovementIdleState exited");
    }
}
