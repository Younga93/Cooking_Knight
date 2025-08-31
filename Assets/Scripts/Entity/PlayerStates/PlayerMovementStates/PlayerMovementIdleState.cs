using UnityEngine;

public class PlayerMovementIdleState: IPlayerMovementState
{
    public void EnterState(Player player)
    {
        Debug.Log("PlayerMovementIdleState entered");
    }

    public void UpdateState(Player player)
    {
        Debug.Log("PlayerMovementIdleState updated");
    }

    public void ExitState(Player player)
    {
        Debug.Log("PlayerMovementIdleState exited");
    }
}
