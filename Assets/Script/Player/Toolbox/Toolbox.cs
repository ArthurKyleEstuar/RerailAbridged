using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Toolbox : MonoBehaviour
{
    [SerializeField] private GameObject toolboxHUD;

    private bool inInteractRange;

    public static System.Action<bool> OnOverlap;

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
        if (IsPlayerTag(collision.gameObject.tag) && toolboxHUD != null)
        {
            OnOverlap?.Invoke(true);

            inInteractRange = true;
            toolboxHUD.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPlayerTag(collision.gameObject.tag) && toolboxHUD != null)
        {
            OnOverlap?.Invoke(false);

            inInteractRange = false;
            toolboxHUD.SetActive(false);
        }
    }
	
	private bool IsPlayerTag(string otherTag)
	{
		return otherTag == "Player";
	}

    private void Start()
    {
        if (toolboxHUD != null)
            toolboxHUD.SetActive(false);
    }

    private void PickedUp(bool pickUp)
    {
        if (!inInteractRange || !pickUp) return;

        Destroy(this.gameObject);
    }
}
