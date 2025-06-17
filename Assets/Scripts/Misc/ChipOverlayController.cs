using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChipOverlayController : MonoBehaviour
{
    public static ChipOverlayController instance;
    public Image display;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public GameObject scrollObject;
    public Transform scrollParent;
    public Chip chipPrefab;
    public bool isDisplayingChip;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        stopOverlay();
    }

    public void setOverlay(ChipCard card)
    {
        Time.timeScale = 0;
        Button.onAny = true;
        gameObject.SetActive(true);
        display.sprite = card.display;
        title.text = card.name;
        description.text = card.description;
        title.gameObject.SetActive(true);
        description.gameObject.SetActive(true);
        display.gameObject.SetActive(true);
        scrollObject.SetActive(false);
        isDisplayingChip = true;
    }

    public void startOverlay()
    {
        gameObject.SetActive(true);
        scrollObject.SetActive(true);
        title.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        display.gameObject.SetActive(false);
        foreach (ChipCard cc in ChipManager.instance.inventory)
        {
            chipPrefab.mainChip = false;
            chipPrefab.info = cc;
            Instantiate(chipPrefab, scrollParent, false);
        }

        isDisplayingChip = false;
    }

    public void stopOverlay()
    {
        if (isDisplayingChip)
        {
            startOverlay();
        }
        else
        {
            foreach(Transform child in scrollParent)
            {
                Destroy(child.gameObject);
            }
            gameObject.SetActive(false);
        }
    }

    public void stopOverlay(ChipCard card)
    {
        ChipManager.instance.inventory.Remove(card);
        if (OverlayController.instance.currentTile.chip != null)
        {
            ChipManager.instance.inventory.Add(OverlayController.instance.currentTile.chip);
        }
        OverlayController.instance.currentTile.chip = card;
        OverlayController.instance.chipImage.sprite = card.display;
        stopOverlay();
    }
}
