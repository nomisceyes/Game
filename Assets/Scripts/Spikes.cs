using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private string collisionTag;

    private int _spikesDamege = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == collisionTag)
        {
            HealthBar currentHealth = collision.gameObject.GetComponent<HealthBar>();
            currentHealth.TakeDamage(_spikesDamege);
        }
    }
}