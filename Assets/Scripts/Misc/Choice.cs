using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Choice : MonoBehaviour, IPointerClickHandler
{
    public GameObject childObject;
    public Card info;
    public Image background;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI costText;
    public Image costImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public void OnEnable()
    {
        if (info != null)
        {
            childObject.GetComponent<Image>().sprite = info.display;
            if (info is TileCard tc)
            {
                background.sprite = UpgradeSelectionManager.instance.backgrounds[0];
                costImage.sprite = ResourceManager.instance.shapes[(int)tc.costType];
                costText.enabled = true;
                if (tc.costAmount == 0)
                {
                    costImage.enabled = false;
                    costText.text = "Free!";
                }
                else
                {
                    costText.text = tc.costAmount + "";
                }

                if (tc.defaultCount > 1)
                {
                    countText.text = "x" + tc.defaultCount;
                }
                else
                {
                    countText.enabled = false;
                }
            }
            else
            {
                countText.enabled = false;
                costText.enabled = false;
                costImage.enabled = false;
            }
            if (info is UpgradeCard uc)
            {
                descriptionText.enabled = true;
                titleText.enabled = true;
                titleText.text = info.name;
                descriptionText.text = uc.cardText;
                background.sprite = UpgradeSelectionManager.instance.backgrounds[1];
            }
            else if (info is ChipCard cc){
                 descriptionText.enabled = true;
                 titleText.enabled = true;
                 titleText.text = info.name;
                 descriptionText.text = cc.cardText;
                 background.sprite = UpgradeSelectionManager.instance.backgrounds[2];
            }
            else{
                descriptionText.enabled = false;
                titleText.enabled = false;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !OverlayController.instance.gameObject.activeSelf)
        {
            if (info is TileCard tc)
            {
                ButtonMaker.instance.CreateChild(tc,tc.defaultCount);
            }

            if (info is UpgradeCard uc)
            {
                UpgradeManager.instance.unlock(uc.upgrade);
            }
            if (info is ChipCard cc)
            {
                ChipManager.instance.get(cc.chip);
            }
            UpgradeSelectionManager.instance.stopOverlay();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OverlayController.instance.setOverlay(info);
        }
    }
}
