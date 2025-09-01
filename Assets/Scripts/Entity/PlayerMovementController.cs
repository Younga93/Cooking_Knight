using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Movement Configuration")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    public Rigidbody2D Rigidbody2D { get; private set; }
    
    
    public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(MovementInput.x * moveSpeed, Rigidbody2D.velocity.y);
    }

    public void SetMovementInput(Vector2 movementInput)
    {
        MovementInput = movementInput;
    }

    public void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
