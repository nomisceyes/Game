using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropBehavior : MonoBehaviour
{
    private LayerMask _groundLayer;
    private float _stopDistance;
    private Rigidbody2D _rigidbody;

    public void Init(LayerMask groundLayer, float stopDistance)
    {
        this._groundLayer = groundLayer;
        this._stopDistance = stopDistance;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _stopDistance + 0.1f, _groundLayer);
          
        if(hit.collider != null && hit.distance <= _stopDistance)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.gravityScale = 0;
            _rigidbody.isKinematic = true;
        }
    }
}

