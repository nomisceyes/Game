using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class Attack : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private Transform _attackPoint;  
    [SerializeField] private int _attackDamage = 20;
    [SerializeField] private int _currentAttack = 0;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _timeSinceAttack = 0.0f;
    [SerializeField] private float _attackForce = 10.0f;

    private Animator _animator;
    private PlayerControl _playerControl;

    private float _delayAttackTime = 0.3f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerControl = GetComponent<PlayerControl>();
    }

    private void Update()
    {
        if ((_timeSinceAttack += Time.deltaTime) > _delayAttackTime)
        {
            if (_timeSinceAttack > 1.0f)
                _currentAttack = 0;

            if (Input.GetMouseButtonDown(0) && _timeSinceAttack > 0.25 && _playerControl.onGround)
            {
                _timeSinceAttack = 0.0f;
                AttackAnimation();
            }
        }
    }

    private void PlayerAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);      

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit curwa!");
            enemy.GetComponent<Enemy>().TakeDamage(_attackDamage);
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x * _attackForce,enemy.transform.position.y));
        }
    }

    private void AttackAnimation()
    {
        _currentAttack++;

        if (_currentAttack > 2)
            _currentAttack = 1;

        if (_timeSinceAttack > 1.0f)
            _currentAttack = 1;

        _animator.SetTrigger("Attack" + _currentAttack);

        _timeSinceAttack = 0.0f;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
