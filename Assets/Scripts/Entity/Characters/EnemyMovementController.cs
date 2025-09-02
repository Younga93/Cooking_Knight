using UnityEngine;

public class EnemyMovementController : MonoBehaviour    //refactoring PlayerMovementController랑 겹치는 부분이 많음!
{
    public Rigidbody2D Rigidbody2D { get; private set; }
    private float _currentSpeed;
    private Vector2 _moveDirection;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        FlipCharacter();
        Rigidbody2D.velocity = new Vector2(_moveDirection.x * _currentSpeed, Rigidbody2D.velocity.y);
        
    }

    public void SetCurrentSpeed(float speed)
    {
        _currentSpeed = speed;
    }
    public void SetMoveDirection(Vector2 direction)
    {
        _moveDirection = direction;
    }
    
    private void FlipCharacter()
    {
        if (_moveDirection.x < 0)    //좌측이동중
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_moveDirection.x > 0) //우측 이동중
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
