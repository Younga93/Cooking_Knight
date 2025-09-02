using UnityEngine;

public class PlayerActionAttackState : IPlayerActionState
{
    public void EnterState(Player player)
    {
        // Debug.Log("PlayerActionAttackState entered");
        player.AttackController.StartAttack();
    }

    public void UpdateState(Player player)
    {
        //todo. 애니메이션 이벤트로 판정하기
    }

    public void ExitState(Player player)
    {
        // Debug.Log("PlayerActionAttackState Exited");
    }
}

