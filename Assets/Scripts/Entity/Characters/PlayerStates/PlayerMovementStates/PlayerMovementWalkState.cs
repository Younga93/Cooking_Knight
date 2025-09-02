using UnityEngine;

public class PlayerMovementWalkState: IPlayerMovementState
{
    public void EnterState(Player player)
    {
        Debug.Log("PlayerMovementWalkState entered");
        player.PlayerAnimator.SetBool(AnimatorString.PlayerParameters.IsWalking, true);
    }

    public void UpdateState(Player player)
    {
        Vector2 movementInput = player.PlayerInputActions.Player.Move.ReadValue<Vector2>();
        player.MovementController.SetMovementInput(movementInput);
    }

    public void ExitState(Player player)
    {
        player.PlayerAnimator.SetBool(AnimatorString.PlayerParameters.IsWalking, false);
        Debug.Log("PlayerMovementWalkState exited");
    }
}