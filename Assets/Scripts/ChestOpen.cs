using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour, IUsable
{   
    private Animator _animator;   
    private bool _isOpened = false;  

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Use()
    {
        if (!_isOpened)
        {
             gameObject.SendMessage("DropCoins", SendMessageOptions.DontRequireReceiver);
            _isOpened = !_isOpened;
            _animator.SetBool("IsOpened", _isOpened);            
        }
        else
        {
            _isOpened = !_isOpened;
            _animator.SetBool("IsOpened", _isOpened);
        }
    }

    public string GetDescription()
    {
        if (_isOpened == false)
            return "Press [E] to <color=green>open</color> the chest";
        else 
            return "Press [E] to <color=red>close</color> the chest";
    }

    private void RRRR()
}
