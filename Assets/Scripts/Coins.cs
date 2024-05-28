using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    [SerializeField] Text _amountCoins;
    private int _coins = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            _coins++;
            _amountCoins.text = _coins.ToString();
            Destroy(collision.gameObject);
        }
    }
}
