using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Vector2 _moveVector;
    private Vector2 _jumpAngle = new Vector2(3.5f, 10);

    private int _facingDirection = 1;
    private float _groundCheckRadius = 0.03f;
    private float _wallCheckRadius = 0.03f;
    private float _speed = 350f;
    private float _jumpForce = 6.5f;
    private float _jumpWallTime = 0.1f;
    private float _timerJumpWall;
    private float _slideSpeed = 1f;
    private float _dashForce = 3f;
    private float _dashTime = 0.0f;
    private float _dashCurrentTime;
    private float _dashDelayTime = 1.2f;
    private float _dashDuration = 8.0f / 14.0f;
    private float _gravityDef;
    private bool _flipRight = true;
    private bool _isDashing = false;
    private bool _blockMoveXforJump;
    private bool _onWallDown;

    public bool onGround;
    public bool onWall;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _groundCheckRadius = _groundCheck.GetComponent<CircleCollider2D>().radius;
        _wallCheckRadius = _wallCheck.GetComponent<CircleCollider2D>().radius;
        _gravityDef = _rigidBody.gravityScale;
    }

    private void Update()
    {
        if (_isDashing)
            _dashCurrentTime += Time.deltaTime;

        if (_dashCurrentTime > _dashDuration)
            _isDashing = false;

        Flip();
        Jump();
        WallJump();
        Fall();
        Dash();
    }

    private void FixedUpdate()
    {
        CheckingGround();
        CheckingWall();
        Move();
    }

    private void Move()
    {
        if (_blockMoveXforJump)
        {
            _moveVector.x = 0;
        }
        else
        {
            _moveVector.x = Input.GetAxisRaw("Horizontal");
            _rigidBody.velocity = new Vector2(_moveVector.x * _speed * Time.deltaTime, _rigidBody.velocity.y);
        }
        _animator.SetFloat("speed", Mathf.Abs(_moveVector.x));
    }

    private void Flip()
    {
        if ((_moveVector.x > 0 && !_flipRight) || (_moveVector.x < 0 && _flipRight))
        {
            transform.localScale *= new Vector2(-1f, 1f);
            _flipRight = !_flipRight;             
        }
    }

    private void Jump()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
        }
    }

    private void WallJump()
    {     
        if (onWall && !onGround && Input.GetKeyDown(KeyCode.Space))
        {
            _blockMoveXforJump = true;

            _animator.StopPlayback();
            _animator.SetTrigger("Jump");

            _moveVector.x = 0;

            transform.localScale *= new Vector2(-1, 1);
            _flipRight = !_flipRight;

            _rigidBody.gravityScale = _gravityDef;
            _rigidBody.velocity = new Vector2(0, 0);

            _rigidBody.velocity = new Vector2((transform.localScale.x * _jumpAngle.x) / 3, _jumpAngle.y / 2);
        }

        if ((_blockMoveXforJump && (_timerJumpWall += Time.deltaTime) >= _jumpWallTime))
        {
            if (onWall || onGround || Input.GetAxisRaw("Horizontal") != 0)
            {
                _blockMoveXforJump = false;
                _timerJumpWall = 0;
            }
        }
    }

    private void Fall()
    {
        _animator.SetFloat("airSpeedY", _rigidBody.velocity.y);
    }

    private void Dash()
    {
        if (((_dashTime += Time.deltaTime) > _dashDelayTime) && onGround && !_isDashing && Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dashTime = 0.0f;
            _isDashing = true;
            _animator.SetTrigger("dash");
            _rigidBody.velocity = new Vector2(_facingDirection * _dashForce, _rigidBody.velocity.y);
        }
    }

    private void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        _animator.SetBool("onGround", onGround);
    }

    private void CheckingWall()
    {
        onWall = Physics2D.OverlapCircle(_wallCheck.position, _wallCheckRadius, _wallLayer);
     
        _animator.SetBool("onWall", onWall);

        if (onWall)
        {
            _animator.SetFloat("airSpeedY", _rigidBody.velocity.y);

            if (!_blockMoveXforJump && _moveVector.y == 0)
            {
                _rigidBody.gravityScale = 0;
                _rigidBody.velocity = new Vector2(0, -_slideSpeed);
            }
        }
        else if (!onGround && !onWall)
            _rigidBody.gravityScale = _gravityDef;
    }
}

