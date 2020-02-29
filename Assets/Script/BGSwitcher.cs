using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGSwitcher : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Image          bgImage;

    private void Start()
    {
        if (GameManager.Manager == null) return;

        if(sr != null)
            sr.sprite = GameManager.Manager.GetRandomBG();

        if (bgImage != null)
            bgImage.sprite = GameManager.Manager.GetRandomBG();
    }
}
