using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _player;
    
    [SerializeField] private float _jumpForce;
    
    [SerializeField] private float _distanceToGround = 1.1f;

    [SerializeField] private LayerMask _groundMask;
    
    [SerializeField] private float _moveSpeed;
    
    private bool _isJumping = false;
    private bool _isOnGround = false;
    
    private float _moveDirection;
    
    private Vector3 _lookRight;
    private Vector3 _lookLeft;
    

    void Start()
    {
        _lookRight = _player.transform.localScale;
        _lookLeft = new Vector3(-_player.transform.localScale.x, _player.transform.localScale.y, _player.transform.localScale.z);
    }

    void Update()
    {
        CalculateSpeed();
        CalculateJump();
        Move();
    }

    private void CalculateSpeed()
    {
        _moveDirection = Input.GetAxisRaw("Horizontal");
        if (_moveDirection < 0)
        {
            _player.transform.localScale = _lookLeft;
        }
        else _player.transform.localScale = _lookRight;
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _player.linearVelocity = new Vector2(-2f, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _player.linearVelocity = new Vector2(2f, 0);
        }
    }

    private void CalculateJump()
    {
        _isOnGround = Physics2D.Raycast(_player.position, Vector2.down, _distanceToGround, _groundMask);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isOnGround)
        {
            if (_isJumping)
            {
                _player.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _isJumping = false;
            }
        }
        _player.linearVelocity = new Vector2(_moveDirection*_moveSpeed, _player.linearVelocity.y);
    }
}
