using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerClickHandler
{
    public GameObject childObject;
    public GameObject prefab;
    public static bool onAny = false;
    private bool onThis = false;
    public void Start()
    {
        childObject.GetComponent<Image>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MouseFollower.instance.setPrefab(prefab);
    }

    private void Update()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform,Input.mousePosition))
        {
            onAny = true;
            onThis = true;
            Vector3 position = transform.localPosition;
            if (position.y < 120f)
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
            if (position.y > 70f)
            {
                transform.localPosition = new Vector3(position.x, position.y - 2f);
            }
            onThis = false;
        }
    }
}
