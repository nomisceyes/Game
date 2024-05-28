using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;

public class Chest : MonoBehaviour
{
    private Animator _animator;
   
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Use()
    {
        _animator.SetBool("IsOpened", true);
    } 
}

