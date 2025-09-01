using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Movement Configuration")]
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody2D _rigidbody2D;
    
    public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // if (MovementInput.magnitude == 0) //미끄러지지 않고 바로 멈추도록함.
        // {
        //     _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        // }
        // else
        // {
            _rigidbody2D.velocity = new Vector2(MovementInput.x * moveSpeed, _rigidbody2D.velocity.y);
        // }
    }

    public void SetMovementInput(Vector2 movementInput)
    {
        MovementInput = movementInput;
    }
}
