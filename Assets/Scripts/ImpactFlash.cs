using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ImpactFlash : MonoBehaviour
{
    public Color _originalColor;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    public void Flash(float duration, Color flashColor)
    {
        StartCoroutine(DoFlash(duration,flashColor));
    }

    private IEnumerator DoFlash(float duration, Color flashColor)
    {       
        _spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(duration);

        _spriteRenderer.color = _originalColor;
    }
}
