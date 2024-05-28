using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private TextMeshProUGUI _interactionText;

    RaycastHit2D hit;

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 2f);

        OpenChest();
    }

    private void OpenChest()
    {
        bool hitSomething = false;

        if (Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 2f))
        {
            IUsable usable = hit.collider.GetComponent<IUsable>();

            if (usable != null)
            {
                hitSomething = true;
                _interactionText.text = usable.GetDescription();

                if (hit.collider.CompareTag("Chest") && Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.SendMessage("Use", SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        interactionUI.SetActive(hitSomething);
    }
}