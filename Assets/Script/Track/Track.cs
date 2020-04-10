using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Track : MonoBehaviour
{
    [Header("Scene Components")]
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Sprite         unbrokenSprite;
    [SerializeField] private Sprite         brokenSprite;

    [Header("Track Life")]
    [SerializeField] private Canvas         lifeCanvas;
    [SerializeField] private GameObject     trackLifeBar;
    [SerializeField] private Image          lifeImage;
    [SerializeField] private Image          toolIcon;

    [Header("Stats")]
    [SerializeField] private int            maxHP           = 5;
    [SerializeField] private float          damageInterval  = 1.0f;
    [SerializeField] private int            currHP;
    [SerializeField] private bool           canBreak        = true;

    private bool            isBroken;
    private bool            hasRegisteredDamage     = false;
    private Coroutine       DamageDOTTimer;
    private TrackManager    trackManager;
    private ItemData        toolRequired;

    public bool IsDamaged   { get { return currHP < maxHP; } }
    public bool IsRendered  { get { return render.isVisible; } }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Train" && isBroken)
        {
            Train train = collision.gameObject.GetComponent<Train>();

            if (train == null) return;

            train.LaunchTrain();
        }

        if (collision.gameObject.tag == "Player")
        {
            PlayerController.OnRepairAction += RepairTrack;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.OnRepairAction -= RepairTrack;
        }
    }

    private void Start()
    {
        if (lifeCanvas == null) return;

        lifeCanvas.worldCamera = Camera.main;
    }

    private void OnDisable()
    {
        PlayerController.OnRepairAction -= RepairTrack;
    }

    public void Initialize(TrackManager manager)
    {
        trackManager = manager;

        currHP = maxHP;
        ToggleTrack(false);
        UpdateLifeBar(false);
    }

    public void ToggleTrack(bool isDamaged)
    {
        isBroken = isDamaged;

        if (render != null)
            render.sprite = (isBroken) 
                ? brokenSprite : unbrokenSprite;
    }

    private void UpdateLifeBar(bool active)
    {
        if (lifeImage != null && trackLifeBar != null)
        {
            lifeImage.fillAmount = (float)currHP / (float)maxHP;
            trackLifeBar.SetActive(active);
        }

        if (toolIcon != null && toolRequired != null)
            toolIcon.sprite = toolRequired.ItemSprite;
    }

    #region Track Action
    public void RepairTrack(ItemData currTool)
    {
        if (!IsDamaged) return;

        if (currTool == null)
        {
            Debug.LogError("No Tool");
            return;
        }

        if (toolRequired == null)
        {
            Debug.LogError("No required");
            return;
        }

        if(currTool.ID != toolRequired.ID)
        {
            trackManager.InvalidToolUsed();
            Debug.LogError(currTool.ID + " " + toolRequired.ID);
            return;
        }

        currHP++;
        UpdateLifeBar(true);

        if (currHP >= maxHP)
        {
            currHP = maxHP;
            ToggleTrack(false);
            StopDamageOverTime();
            UpdateLifeBar(false);

            hasRegisteredDamage = false;
            trackManager.RemoveDamaged(this);
        }
    }

    public void ToggleCanBreak()
    {
        canBreak = !canBreak;
    }

    public void DamageTrack(int delta = 1)
    {
        if (!canBreak) return;

        currHP -= delta;

        if (currHP < 0)
            currHP = 0;
        else if (currHP >= maxHP)
            currHP = maxHP;

        ToggleTrack(currHP <= 0);

        if (currHP == maxHP - 1)
            DetermineRequiredTool();

        UpdateLifeBar(true);

        DamageDOTTimer = StartCoroutine(DamageOverTimeCR());

        if (hasRegisteredDamage) return;

        hasRegisteredDamage = true;
        trackManager.AddDamaged(this);
    }

    private void DetermineRequiredTool()
    {
        toolRequired = trackManager.ToolDB.GetRandomFile();
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

