using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Choice : MonoBehaviour, IPointerClickHandler
{
    public GameObject childObject;
    public Card info;
    public void Start()
    {
        childObject.GetComponent<Image>().sprite = info.display;
        //setup display
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
             //apply card
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OverlayController.instance.setOverlay(info);
        }
    }
}
