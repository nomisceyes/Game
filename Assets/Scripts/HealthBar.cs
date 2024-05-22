using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image[] _lives;
    [SerializeField] private Sprite _fullLive;
    [SerializeField] private Sprite _emptyLive;

    private int _health = 5;
    private int _numberOfLives = 5;


    private void Update()
    {
        if (_health > _numberOfLives)
            _health = _numberOfLives;

        for (int i = 0; i < _lives.Length; i++)
        {
            if (i < _health)
                _lives[i].sprite = _fullLive;
            else
                _lives[i].sprite = _emptyLive;


            if (i < _numberOfLives)
                _lives[i].enabled = true;
            else
                _lives[i].enabled = false;
        }
    }
}
