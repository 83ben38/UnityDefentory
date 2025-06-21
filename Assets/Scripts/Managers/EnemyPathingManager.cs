using System.Collections.Generic;
using UnityEngine;

public class EnemyPathingManager : MonoBehaviour
{
    public GameObject outPrefab;
    public static EnemyPathingManager instance;
    [SerializeField]
    private List<Tile> outputVortexes = new List<Tile>();
    [SerializeField]
    private List<Vector2Int> possibleStarts = new List<Vector2Int>();
    private int on = 0;
    private void Awake()
    {
        instance = this;
    }

    public int getNumPaths()
    {
        return outputVortexes.Count;
    }
    public bool isEnd(Vector2Int position)
    {
        foreach (Tile tile in outputVortexes)
        {
            if (tile.location == position)
            {
                return true;
            }
        }

        return false;
    }

    public Vector2Int getNextTile()
    {
        on %= possibleStarts.Count;
        Vector2Int tile = possibleStarts[on];
        on++;
        return tile;
    }
    public void DoPathing()
    {

        List<Vector2Int> newLocations = new List<Vector2Int>();
        possibleStarts = new List<Vector2Int>();
        bool addToNewLocations = false;
        for (int i = -1; i < 2; i+=2)
        {
            if (GridController.instance.grid.ContainsKey(new Vector2Int(0, i)))
            {
                if (GridController.instance.grid[new Vector2Int(0, i)].tileType == Tile.Type.EnemyBelt)
                {
                    possibleStarts.Add(new Vector2Int(0, i));
                }
               
            }
            if (GridController.instance.grid.ContainsKey(new Vector2Int(i,0)))
            {
                if (GridController.instance.grid[new Vector2Int(i,0)].tileType == Tile.Type.EnemyBelt)
                {
                    possibleStarts.Add(new Vector2Int(i,0));
                }
            }
        }

        if (possibleStarts.Count == 0)
        {
            addToNewLocations = true;
            for (int i = -1; i < 2; i+=2)
            {
                newLocations.Add(new Vector2Int(i, 0));
                newLocations.Add(new Vector2Int(0,i));
            }
        }
        else
        {
            foreach (Vector2Int start in possibleStarts)
            {
                List<Vector2Int> vortexes = findEnds(start, new List<Vector2Int>());
               newLocations.AddRange(vortexes);
            }
        }

        for (int i = 0; i < outputVortexes.Count; i++)
        {
            if (!newLocations.Contains(outputVortexes[i].location))
            {
                Destroy(outputVortexes[i].gameObject);
                outputVortexes.RemoveAt(i);
                i--;
            }
            else
            {
                newLocations.Remove(outputVortexes[i].location);
            }
        }
        foreach (Vector2Int vortex in newLocations)
        {
            GameObject go = Instantiate(outPrefab);
            go.transform.position = new Vector3(vortex.x,vortex.y);
            outputVortexes.Add(go.GetComponent<Tile>());
            outputVortexes[^1].location = vortex;
            
        }
        if (addToNewLocations)
        {
            for (int i = 0; i < outputVortexes.Count; i++)
            {
                possibleStarts.Add(outputVortexes[i].location);
            }
        }
    }

    public List<Vector2Int> findEnds(Vector2Int start, List<Vector2Int> alreadyVisited)
    {
        if (alreadyVisited.Contains(start))
        {
            return new List<Vector2Int> { start };
        }
        alreadyVisited.Add(start);
        if (!GridController.instance.grid.ContainsKey(start))
        {
            return new List<Vector2Int>{ start };
        }
        Tile t = GridController.instance.grid[start];
        if (t.tileType != Tile.Type.EnemyBelt)
        {
            return new List<Vector2Int>{ start };
        }

        if (t.rotation % 2 == 0)
        {
            return findEnds(start + new Vector2Int(0,1-t.rotation),alreadyVisited);
        }

        return findEnds(start + new Vector2Int(t.rotation - 2, 0),alreadyVisited);
    }
}
