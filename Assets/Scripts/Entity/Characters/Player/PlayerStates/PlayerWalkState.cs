using UnityEngine;

public class PlayerWalkState: IPlayerState
{
    //움직임 사운드 체크용 필드
    private bool _isMoveSoundPlayed;
    private float _elaspedTime;
    
    public void EnterState(Player player)
    {
        // Debug.Log("PlayerMovementWalkState entered");
        player.PlayerAnimator.SetBool(AnimatorString.PlayerParameters.IsWalking, true);
    }

    public void UpdateState(Player player)
    {
        if (_isMoveSoundPlayed)
        {
            _elaspedTime += Time.deltaTime;
            if (_elaspedTime > 1.0f)
            {
                _isMoveSoundPlayed = false;
                _elaspedTime = 0;
            }
        }
        if (!_isMoveSoundPlayed)
        {
            _isMoveSoundPlayed = true;
            AudioManager.Instance.PlayMoveSoundEffect();
        }
        
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