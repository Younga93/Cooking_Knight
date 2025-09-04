using UnityEngine;

public class PlayerHitState : IPlayerState
{
    // private float _stunTimer;
    public void EnterState(Player player)
    {
        Debug.Log($"Player: Hit 상태 진입 (피격 경직)");
        AudioManager.Instance.PlayHurtSoundEffect();
        //_stunTimer = Timer.STUN_DURATION;
        player.PlayerAnimator.SetTrigger(AnimatorString.PlayerParameters.Hit);
        
        // player.MovementController.ApplyKnockback();
        player.MovementController.SetStun(true, Timer.STUN_DURATION);
        player.ConditionController.StartHitEffect();
    }

    public void UpdateState(Player player)
    {
        // _stunTimer -= Time.deltaTime;
        //
        // if (_stunTimer <= 0)
        // {
        //     player.MovementController.SetStun(false);
        //     
        //     if (player.CurrentMovementInput.magnitude > 0)
        //     {
        //         player.TransitionToState(PlayerState.Walk);
        //     }
        //     else
        //     {
        //         player.TransitionToState(PlayerState.Idle);
        //     }
        // }
    }

    public void ExitState(Player player)
    {
        Debug.Log($"Player: Hit 상태 끝남");
    }
}
