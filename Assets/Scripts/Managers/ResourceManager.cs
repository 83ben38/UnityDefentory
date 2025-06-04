using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public Dictionary<Shape.Type,int> resources = new();
    public static ResourceManager instance;
    public List<Sprite> shapes;
    private Dictionary<Shape.Type,TextMeshProUGUI> texts = new();
    public GameObject childPrefab;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        foreach (var type in resources.Keys)
        {
            if (!texts.ContainsKey(type))
            {
                GameObject go = Instantiate(childPrefab, transform);
                texts[type] = go.GetComponentInChildren<TextMeshProUGUI>();
                go.GetComponent<Image>().sprite = shapes[(int)type];
            }
            else
            {
                Debug.Log(texts[type]);
                Debug.Log(resources[type]);
                texts[type].text = resources[type].ToString();
            }
        }
    }
}
