using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Animator _animator;
    private Player _player;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _player = GetComponent<Player>();
    }

    public void StartAttack()
    {
        //1번 레이어(Action Layer)에서 재생하기
        _animator.Play(AnimatorString.PlayerAnimation.Attack, 1);
        
        //todo. 추후 애니메이션 이벤트로 처리하기
        float animationLength = _animator.GetCurrentAnimatorStateInfo(1).length;
        Invoke(nameof(OnAttackAnimationEnd), animationLength);
    }

    public void OnAttackAnimationEnd()
    {
        _animator.Play("Empty", 1);
        _player.TransitionToActionState(PlayerState.Action.Idle);
    }
}
