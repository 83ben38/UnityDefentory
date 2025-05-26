using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerClickHandler
{
    public GameObject childObject;
    public GameObject prefab;
    public void Start()
    {
        childObject.GetComponent<Image>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MouseFollower.instance.setPrefab(prefab);
    }
}
