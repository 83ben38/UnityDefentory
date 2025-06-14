using UnityEngine;

public class UpgradeSelectionManager : MonoBehaviour
{
    public static UpgradeSelectionManager instance;
    public Choice[] choices;
    public bool isOverlayActive = false;
    public Sprite[] backgrounds;
    public TileCard turret;
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
        Card[] cards = new Card[3];
        for (int i = 0; i < 3; i++)
        {
            int num = Random.Range(0, 4);
            if (num < 2)
            {
                cards[i] = turret;
                //do shenanigans
            }

            if (num == 2)
            {
                cards[i] = UpgradeManager.instance.notUnlocked[(int)(UpgradeManager.instance.notUnlocked.Count * Random.value)];
            }

            if (num == 3)
            {
                cards[i] = ChipManager.instance.available[(int)(ChipManager.instance.available.Count * Random.value)];
            }
        }
        setOverlay(cards);
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
