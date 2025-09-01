using UnityEngine;

public class PlayerMovementWalkState: IPlayerMovementState
{
    public void EnterState(Player player)
    {
        Debug.Log("PlayerMovementWalkState entered");
    }

    public void UpdateState(Player player)
    {
        Vector2 movementInput = player.PlayerInputActions.Player.Move.ReadValue<Vector2>();
        player.MovementController.SetMovementInput(movementInput);
        
        Debug.Log("PlayerMovementWalkState updated");
    }

    public void ExitState(Player player)
    {
        Debug.Log("PlayerMovementWalkState exited");
    }
}