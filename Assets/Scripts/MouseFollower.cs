using System;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    public Camera cam;
    public GameObject ghostObject;

    private void Start()
    {
        setPrefab(prefab);
    }

    public void setPrefab(GameObject prefab)
    {
        this.prefab = prefab;
        ghostObject.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }
    void Update()
    {
        if (prefab)
        {
            
            ghostObject.SetActive(true);
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenPosition);
            mouseWorldPosition.z = 0f;
            mouseWorldPosition.y = Mathf.RoundToInt(mouseWorldPosition.y);
            mouseWorldPosition.x = Mathf.RoundToInt(mouseWorldPosition.x);
            if (GridController.instance.grid.ContainsKey(new Vector2(mouseWorldPosition.x, mouseWorldPosition.y)))
            {
                ghostObject.SetActive(false);
            }
            ghostObject.transform.position = mouseWorldPosition;
        }
        else
        {
            ghostObject.SetActive(false);
        }

    }
}
