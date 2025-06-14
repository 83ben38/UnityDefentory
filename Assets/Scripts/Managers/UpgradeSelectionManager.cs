using UnityEngine;

public class UpgradeSelectionManager : MonoBehaviour
{
    public static UpgradeSelectionManager instance;
    public Choice[] choices;
    public bool isOverlayActive = false;
    public Card[] test;
    public Sprite[] backgrounds;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        stopOverlay();
    }

    public void setOverlay()
    {
        setOverlay(test);
    }
    public void setOverlay(Card[] cards)
    {
        Time.timeScale = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            choices[i].info = cards[i];
        }
        gameObject.SetActive(true);
        isOverlayActive = true;
        Button.onAny = true;
    }

    public void stopOverlay()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        isOverlayActive = false;
        Button.onAny = false;
    }
}
