using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _player;
    
    [SerializeField] private float _jumpForce;
    
    [SerializeField] private float _distanceToGround = 1.1f;

    [SerializeField] private LayerMask _groundMask;
    
    [SerializeField] private float _moveSpeed;
    
    private bool _isOnGround = false;
    
    private float _inputHorizontal;
    private float _moveDirection;
    
    private Vector3 _lookRight;
    private Vector3 _lookLeft;
    

    void Start()
    {
        _lookRight = _player.transform.localScale;
        _lookLeft = new Vector3(-_player.transform.localScale.x, _player.transform.localScale.y, _player.transform.localScale.z);
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
        _inputHorizontal = Input.GetAxis("Horizontal");
        _player.linearVelocity = new Vector2(_inputHorizontal * _moveSpeed, _player.linearVelocity.y);
    }

    private void CalculateJump()
    {
        _isOnGround = Physics2D.Raycast(_player.position, Vector2.down, _distanceToGround, _groundMask);
        if(_isOnGround && Input.GetKeyDown(KeyCode.Space))
            _player.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CalculateLinearVelocity()
    {
        _player.linearVelocity = new Vector2(_moveDirection*_moveSpeed, _player.linearVelocity.y);
    }

    private void FixedUpdate()
    {
        CalculateSpeed();
        CalculateJump();
        CalculateLinearVelocity();
        Move();
    }
}
