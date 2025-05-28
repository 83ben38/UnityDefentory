using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseFollower : MonoBehaviour
{

    public Button selected;
    public Camera cam;
    public GameObject ghostObject;
    public int rotation;
    public static MouseFollower instance;
    private void Awake()
    {
        instance = this;
    }

   

    public void setPrefab(Button selected)
    {
        this.selected = selected;
        ghostObject.GetComponent<SpriteRenderer>().sprite = selected.prefab.GetComponent<SpriteRenderer>().sprite;
        rotation = 0;
        ghostObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Button.onAny)
        {
            if (selected)
            {
                if (ghostObject.activeSelf)
                {
                    GameObject go = Instantiate(selected.prefab);
                    go.transform.position = ghostObject.transform.position;
                    Tile tile = go.GetComponent<Tile>();
                    tile.location = go.transform.position;
                    tile.rotation = rotation;
                    GridController.instance.addToGrid(tile);
                    go.transform.eulerAngles = new Vector3(0, 0, rotation * 90);
                    selected.setNumLeft(selected.numLeft - 1);
                    if (!Input.GetKey(KeyCode.LeftShift) || selected.numLeft == 0)
                    {
                        selected = null;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotation++;
            rotation %= 4;
            ghostObject.transform.eulerAngles = new Vector3(0, 0, rotation * 90);
        }
        if (selected)
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
