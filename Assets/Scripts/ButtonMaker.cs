using System.Collections.Generic;
using UnityEngine;

public class ButtonMaker : MonoBehaviour
{
    public List<GameObject> startingItems = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    public GameObject childPrefab;
    private void Start()
    {
        for (int i = 0; i < startingItems.Count; i++)
        {
            CreateChild(startingItems[i]);
        }
    }

    public void CreateChild(GameObject go)
    {
        childPrefab.GetComponent<Button>().prefab = go;
        items.Add(Instantiate(childPrefab));
        items[items.Count - 1].transform.SetParent(transform);
        RespaceChildren();
    }

    public void RespaceChildren()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.localPosition = new Vector3(((i+1) * 900f / (items.Count+1)),70f);
        }
    }
}
