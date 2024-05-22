using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform[] Points;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;

    private SpriteRenderer _spriteRenderer;
    private ImpactFlash _flash;

    private int _point = 0;
    private float _speed = 3f;
    private float _duration = 0.2f;

    private Color _color = Color.red;

    public Rigidbody2D _rigidbody;

    private void Start()
    {
        _currentHealth = _maxHealth;

        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _flash = GetComponent<ImpactFlash>();
    }

    private void Update()
    {
        MoveEnemyToFirstPoint();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _flash.Flash(_duration, _color);

        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    //public void AttackForce(float attackForce)
    //{
    //    _rigidbody.velocity = new Vector2(attackForce, _rigidbody.velocity.y);    
    //}

    private void MoveEnemyToFirstPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, Points[_point].position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, Points[_point].position) < 0.2f)
            if (_point > 0)
            {
                _point = 0;
                _spriteRenderer.flipX = false;
            }
            else
            {
                _point = 1;
                _spriteRenderer.flipX = true;
            }
    }
}
