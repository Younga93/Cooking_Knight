using System.Collections;

public class PlayerDeadState : IPlayerState
{
    public void EnterState(Player player)
    {
        player.PlayerAnimator.SetTrigger(AnimatorString.PlayerParameters.Dead);
        player.PlayerInputActions.Disable();
        
        player.OnResurrection();
    }

    public void UpdateState(Player player)
    {
    }

    public void ExitState(Player player)
    {
        player.PlayerInputActions.Enable();
    }
}
