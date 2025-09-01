using UnityEngine;

public class PlayerActionIdleState : IPlayerActionState
{
    public void EnterState(Player player)
    {
        Debug.Log("PlayerActionIdleState entered");
    }

    public void UpdateState(Player player)
    {
    }

    public void ExitState(Player player)
    {
        Debug.Log("PlayerActionIdleState Exited");
    }
}
