using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    [SerializeField] private GameObject toolboxHUD;

    private bool inInteractRange;

    private void OnEnable()
    {
        PlayerItemController.OnPickupToolbox += PickedUp;
    }

    private void OnDisable()
    {
        PlayerItemController.OnPickupToolbox -= PickedUp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && toolboxHUD != null)
        {
            inInteractRange = true;
            toolboxHUD.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && toolboxHUD != null)
        {
            inInteractRange = false;
            toolboxHUD.SetActive(false);
        }
    }

    private void Start()
    {
        if (toolboxHUD != null)
            toolboxHUD.SetActive(false);
    }

    private void PickedUp()
    {
        if (!inInteractRange) return;

        Destroy(this.gameObject);
    }
}
