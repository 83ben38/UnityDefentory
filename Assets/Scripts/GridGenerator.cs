using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Tile vortex;
    public GameObject emptyPrefab;
    public static GridGenerator instance;
    public Dictionary<Vector2, Tile> grid;
    void Start()
    {
        instance = this;
        grid = new Dictionary<Vector2, Tile>();
        grid[new Vector2(0, 0)] = vortex;
        TilePlaced(vortex);
    }

    void TilePlaced(Tile placed)
    {
        Vector2 position = placed.position;
        for (int i = -3; i <= 3; i++)
        {
            for (int j = -3; j <= 3; j++)
            {
                if (!grid.ContainsKey(position + new Vector2(i, j)))
                {
                    GameObject thing = Instantiate(emptyPrefab);
                    Tile t = thing.GetComponent<Tile>();
                    t.position = position + new Vector2(i, j);
                    grid[t.position] = t;
                    thing.transform.position = t.position;
                }
            }
        }
    }
}
