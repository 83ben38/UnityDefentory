using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chip : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public ChipCard info;
    public bool mainChip;
    public void OnEnable()
    {
        if (info != null)
        {
            image.sprite = info.display;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (mainChip)
            {
                ChipOverlayController.instance.startOverlay();
            }
            else
            {
                ChipOverlayController.instance.stopOverlay(info);
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right && info)
        {
            ChipOverlayController.instance.setOverlay(info);
        }
    }
}
