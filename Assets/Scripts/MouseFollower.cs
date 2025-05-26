using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    public Camera cam;
    public GameObject ghostObject;
    public int rotation;
    public static MouseFollower instance;
    public static Camera Camera;
    private void Awake()
    {
        instance = this;
        Camera = cam;
    }

    private void Start()
    {
        setPrefab(prefab);
    }

    public void setPrefab(GameObject prefab)
    {
        this.prefab = prefab;
        ghostObject.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        rotation = 0;
        ghostObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Button.onAny)
        {
            if (prefab)
            {
                if (ghostObject.activeSelf)
                {
                    GameObject go = Instantiate(prefab);
                    go.transform.position = ghostObject.transform.position;
                    Tile tile = prefab.GetComponent<Tile>();
                    tile.location = go.transform.position;
                    tile.rotation = rotation;
                    go.transform.eulerAngles = new Vector3(0, 0, rotation * 90);
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        prefab = null;
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
