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
    public Tunnel inputTunnel;
    public Button oldButton;
    private void Awake()
    {
        instance = this;
    }

   

    public void setPrefab(Button selected)
    {
        if (inputTunnel)
        {
            Destroy(inputTunnel.gameObject);
            inputTunnel = null;
        }
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
                    var position = go.transform.position;
                    position = ghostObject.transform.position;
                    go.transform.position = position;
                    Tile tile = go.GetComponent<Tile>();
                    tile.location = new Vector2Int((int)position.x, (int)position.y);
                    tile.rotation = rotation;
                    GridController.instance.addToGrid(tile);
                    go.transform.eulerAngles = new Vector3(0, 0, rotation * 90);
                    if (inputTunnel)
                    {
                        inputTunnel.endingLocation = tile.location;
                        inputTunnel = null;
                        selected = oldButton;
                        oldButton = null;
                        selected.setNumLeft(selected.numLeft - 1);
                    }
                    if (tile.tileType != Tile.Type.Tunnel)
                    {
                        selected.setNumLeft(selected.numLeft - 1);
                    }
                    if (tile.tileType == Tile.Type.Tunnel)
                    {
                        Tunnel t = tile.GetComponent<Tunnel>();
                        int prevRotation = rotation;
                        oldButton = selected;
                        setPrefab(t.outPrefab);
                        inputTunnel = t;
                        t.currentLocation = tile.location;
                        rotation = prevRotation;
                        ghostObject.transform.eulerAngles = new Vector3(0, 0, rotation * 90);
                    }
                    else if (!Input.GetKey(KeyCode.LeftShift) || selected.numLeft == 0)
                    {
                        selected = null;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!inputTunnel)
            {
                rotation++;
                rotation %= 4;
                ghostObject.transform.eulerAngles = new Vector3(0, 0, rotation * 90);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inputTunnel)
            {
                Destroy(inputTunnel.gameObject);
                inputTunnel = null;
            }
            selected = null;
        }
        if (selected)
        {
            ghostObject.SetActive(true);
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(mouseScreenPosition);
            mouseWorldPosition.z = 0f;
            mouseWorldPosition.y = Mathf.RoundToInt(mouseWorldPosition.y);
            mouseWorldPosition.x = Mathf.RoundToInt(mouseWorldPosition.x);
            if (inputTunnel)
            {
                if (rotation % 2 == 0)
                {
                    mouseWorldPosition.x = inputTunnel.currentLocation.x;
                    if (rotation == 0)
                    {
                        mouseWorldPosition.y = Math.Max(mouseWorldPosition.y, inputTunnel.currentLocation.y);
                    }
                    else
                    {
                        mouseWorldPosition.y = Math.Min(mouseWorldPosition.y, inputTunnel.currentLocation.y);
                    }
                }
                else
                {
                    mouseWorldPosition.y = inputTunnel.currentLocation.y;
                    if (rotation == 3)
                    {
                        mouseWorldPosition.x = Math.Max(mouseWorldPosition.x, inputTunnel.currentLocation.x);
                    }
                    else
                    {
                        mouseWorldPosition.x = Math.Min(mouseWorldPosition.x, inputTunnel.currentLocation.x);
                    }
                }
                
            }
            if (GridController.instance.grid.ContainsKey(new Vector2Int((int)mouseWorldPosition.x, (int)mouseWorldPosition.y)))
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
