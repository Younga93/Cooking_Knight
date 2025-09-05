using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditionController : ConditionController
{
    private Player _player;
    public bool IsInvincible { get; private set; } // 외부에 노출
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Coroutine _hitEffectCoroutine;
    
    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        maxHealth += PlayerManager.Instance.additionalMaxHealth;
        SetMaxHealth(maxHealth);
    }

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        SetMaxHealth(maxHealth);
    }
    public override void TakeDamage(float amount)
    {      
        if (IsInvincible)
        {
            return;
        }
        base.TakeDamage(amount);
        if (amount > 0 && currentHealth > 0)
        {
            _player.OnHit();
        }
    }

    protected override void OnDeath()
    {
        _player.OnDead();
    }
    public void StartHitEffect()
    {
        if (_hitEffectCoroutine != null)
        {
            StopCoroutine(_hitEffectCoroutine);
        }
        
        _hitEffectCoroutine = StartCoroutine(HitEffectCoroutine());
    }
    private IEnumerator HitEffectCoroutine()
    {
        IsInvincible = true;
        float timer = 0f;
        Color originalColor = _spriteRenderer.color;
        
        while (timer < Timer.STUN_DURATION)
        {
            timer += Time.deltaTime;
            float colorFactor = Mathf.Abs(Mathf.Sin(timer * 20));
            _spriteRenderer.color = new Color(1, 0, 0, colorFactor);
            yield return null;
        }
        _spriteRenderer.color = originalColor;
        
        while (timer < Timer.INVINCIBLE_TIME)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Abs(Mathf.Sin(timer * 20));
            _spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        IsInvincible = false;
        _spriteRenderer.color = originalColor;
    }
    
    // public void StartRedBlink(float duration)
    // {
    //     StartCoroutine(RedBlink(duration));
    // }
    //
    // private IEnumerator RedBlink(float duration)
    // {
    //     float timer = 0f;
    //     Color originalColor = _spriteRenderer.color;
    //     
    //     while (timer < duration)
    //     {
    //         timer += Time.deltaTime;
    //     
    //         // Mathf.PingPong을 사용해 0과 1 사이를 반복하며 색상을 보간
    //         float colorFactor = Mathf.PingPong(timer * 5, 1);
    //         Color targetColor = Color.Lerp(originalColor, Color.red, colorFactor);
    //     
    //         _spriteRenderer.color = targetColor;
    //     
    //         yield return null;
    //     }
    //     
    //     _spriteRenderer.color = originalColor;
    // }
}
