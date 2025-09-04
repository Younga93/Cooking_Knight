public class PlayerDeadState : IPlayerState
{
    public void EnterState(Player player)
    {
        AudioManager.Instance.PlayDeathSoundEffect();
        player.PlayerAnimator.SetTrigger(AnimatorString.PlayerParameters.Dead);
        player.PlayerInputActions.Disable();
    }

    public void UpdateState(Player player)
    {
    }

    public void ExitState(Player player)
    {
        player.PlayerInputActions.Enable();
    }
}
