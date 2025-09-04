using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Movement Configuration")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float knockbackForce;
    
    public bool IsStunned { get; private set; } = false;
    private Coroutine _stunCoroutine;
    private Player _player;
    
    public Rigidbody2D Rigidbody2D { get; private set; }
    // public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (!IsStunned)
        {
            FlipCharacter();
            Rigidbody2D.velocity = new Vector2(_player.CurrentMovementInput.x * moveSpeed, Rigidbody2D.velocity.y);
        }
        else
        {
            Rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FlipCharacter()
    {
        if (_player.CurrentMovementInput.x < 0)    //좌측이동중
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_player.CurrentMovementInput.x > 0) //우측 이동중
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    // public void ApplyKnockback()
    // {
    //     // Rigidbody2D.velocity = Vector2.zero;
    //     
    //     Vector2 knockbackDirection = transform.localScale.x < 0 ? Vector2.right : Vector2.left;
    //     Debug.Log($"{knockbackForce}만큼 {knockbackDirection.ToString()}으로 넉백중");
    //     Rigidbody2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    // }
    
    public void SetStun(bool isStunned, float duration)
    {
        IsStunned = isStunned;
        if (IsStunned)
        {
            if (_stunCoroutine != null)
            {
                StopCoroutine(_stunCoroutine);
            }
            _stunCoroutine = StartCoroutine(StunCoroutine(duration));
        }
    }
    private IEnumerator StunCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        IsStunned = false;
    }
}
