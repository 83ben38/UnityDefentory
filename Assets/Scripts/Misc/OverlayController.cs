using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
    public static OverlayController instance;
    public Image display;
    public Image resourceCounterImage;
    public TextMeshProUGUI resourceCounterText;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        stopOverlay();
    }

    public void setOverlay(Card card, Tile tile = null)
    {
        Time.timeScale = 0;
        Button.onAny = true;
        gameObject.SetActive(true);
        display.sprite = card.display;
        title.text = card.name;
        description.text = card.description;
        if (card is TileCard && ((TileCard)card).costAmount > 0)
        {
            resourceCounterImage.sprite = ResourceManager.instance.shapes[(int)((TileCard)card).costType];
            resourceCounterText.text = ((TileCard)card).costAmount + "";
        }
        else
        {
            resourceCounterImage.enabled = false;
            resourceCounterText.enabled = false;
        }
    }

    public void stopOverlay()
    {
        if (!UpgradeSelectionManager.instance.isOverlayActive)
        {
            Time.timeScale = 1;
            Button.onAny = false;
        }
        gameObject.SetActive(false);
        
    }
}
