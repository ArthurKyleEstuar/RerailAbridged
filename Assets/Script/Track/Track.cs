using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [Header("Scene Components")]
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Sprite         unbrokenSprite;
    [SerializeField] private Sprite         brokenSprite;

    [Header("Stats")]
    [SerializeField] private int    maxHP           = 5;
    [SerializeField] private float  damageInterval  = 1.0f;
    [SerializeField] private int    currHP;

    private bool isBroken;
    private Coroutine DamageDOTTimer;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Train" && isBroken)
        {
            Train train = collision.gameObject.GetComponent<Train>();

            if (train == null) return;

            train.LaunchTrain();
        }
    }

    public void Initialize()
    {
        currHP = maxHP;
        ToggleTrack(false);
    }

    public void ToggleTrack(bool isDamaged)
    {
        isBroken = isDamaged;

        if (render != null)
            render.sprite = (isBroken) ? brokenSprite : unbrokenSprite;
    }

    #region Track Action
    public void RepairTrack()
    {
        currHP++;

        if (currHP >= maxHP)
        {
            currHP = maxHP;
            ToggleTrack(false);
            StopDamageOverTime();
        }
    }

    public void DamageTrack()
    {
        currHP--;

        if (currHP < 0)
            currHP = 0;

        ToggleTrack(currHP <= 0);

        DamageDOTTimer = StartCoroutine(DamageOverTimeCR());
    }
    #endregion

    #region Track Timer
    private void StopDamageOverTime()
    {
        if (DamageDOTTimer == null) return;

        StopCoroutine(DamageDOTTimer);
    }

    private IEnumerator DamageOverTimeCR()
    {
        yield return new WaitForSeconds(damageInterval);

        DamageTrack();
    }
    #endregion
}

