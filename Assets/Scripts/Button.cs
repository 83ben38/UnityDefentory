using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerClickHandler
{
    public GameObject childObject;
    public Card info;
    public static bool onAny = false;
    private bool onThis = false;
    public TextMeshProUGUI countText;
    public int numLeft;
    public TextMeshProUGUI priceText;
    public Image priceImage;
    public void Start()
    {
        childObject.GetComponent<Image>().sprite = info.prefab.GetComponent<SpriteRenderer>().sprite;
        
        if (info.costAmount != 0)
        {
            priceImage.sprite = ResourceManager.instance.shapes[(int)info.costType];
            priceText.text = info.costAmount.ToString();
        }
        else
        {
            priceImage.enabled = false;
            priceText.text = "Free!";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (info.costAmount == 0 || ResourceManager.instance.resources.ContainsKey(info.costType) &&
            ResourceManager.instance.resources[info.costType] >= info.costAmount)
        {
            MouseFollower.instance.setPrefab(this);
        }
    }

    public void setNumLeft(int num)
    {
        numLeft = num;
        if (numLeft < 1)
        {
            ButtonMaker.instance.items.Remove(gameObject);
            ButtonMaker.instance.RespaceChildren();
            Destroy(gameObject);
        }

        if (numLeft > 1)
        {
            countText.text = "x" + numLeft;
        }
        else if (numLeft == 1)
        {
            countText.text = "";
        }
    }

    

    private void Update()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform,Input.mousePosition))
        {
            onAny = true;
            onThis = true;
            Vector3 position = transform.localPosition;
            if (position.y < 50f)
            {
                transform.localPosition = new Vector3(position.x, position.y + 2f);
            }
        }
        else
        {
            if (onThis)
            {
                onAny = false;
            }
            Vector3 position = transform.localPosition;
            if (position.y > -20f)
            {
                transform.localPosition = new Vector3(position.x, position.y - 2f);
            }
            onThis = false;
        }
    }
}
