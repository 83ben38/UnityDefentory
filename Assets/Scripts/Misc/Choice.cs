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
            //setup display
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
            UpgradeSelectionManager.instance.stopOverlay();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OverlayController.instance.setOverlay(info);
        }
    }
}
