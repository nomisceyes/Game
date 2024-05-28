using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoins : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private LayerMask _groundLayer;

    private int _minCoinDrops = 3;
    private int _maxCoinDrops = 6;
    private float _dropForce = 10;
    private float _dropForceVariation = 0.5f;
    private float _angleVariation = 30f;
    private float _stopDistance = 0.1f;

    public void DropCoin(Vector2 dropPosition)
    {
        int numberOfDrops = Random.Range(_minCoinDrops, _maxCoinDrops + 1);

        for (int i = 0; i < numberOfDrops; i++)
        {
            GameObject coin = Instantiate(_coinPrefab, dropPosition, Quaternion.identity);
            coin.AddComponent<CoinDropBehavior>().Init(_groundLayer, _stopDistance);
            Rigidbody2D coinRigidbody = coin.GetComponent<Rigidbody2D>();

            float force = _dropForce + Random.Range(-_dropForceVariation, _dropForceVariation);
            float randomAngle = Random.Range(-_angleVariation, _angleVariation) * Mathf.Deg2Rad;
            Vector2 randomDirection = Random.insideUnitCircle;
            Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * randomDirection;

            coinRigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }
}
