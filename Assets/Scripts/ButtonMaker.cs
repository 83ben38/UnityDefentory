using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMaker : MonoBehaviour
{
    public List<Card> startingItems = new List<Card>();
    public List<GameObject> items = new List<GameObject>();
    public GameObject childPrefab;
    public static ButtonMaker instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < startingItems.Count; i++)
        {
            CreateChild(startingItems[i]);
        }
    }

    public void CreateChild(Card c)
    {
        Button b = childPrefab.GetComponent<Button>();
        b.info = c;
        b.setNumLeft(c.numLeft);
        items.Add(Instantiate(childPrefab));
        items[^1].transform.SetParent(transform);
        RespaceChildren();
    }

    public void RespaceChildren()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.localPosition = new Vector3(((i+1) * 900f / (items.Count+1)),-20f);
        }
    }
    
}
