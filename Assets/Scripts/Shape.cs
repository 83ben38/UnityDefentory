using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shape : MonoBehaviour
{
    public Type shapeType;
    public enum Type{
        Circle
    }

    public Vector2 location;

    private void Start()
    {
        CheckLocation(true);
    }

    private void CheckLocation(bool start)
    {
        if (!GridController.instance.grid.ContainsKey(location))
        {
            Destroy(gameObject);
            return;
        }
        Tile on = GridController.instance.grid[location];
        if (!on)
        {
            Destroy(gameObject);
        }

        switch (on.tileType)
        {
            case Tile.Type.Belt: StartCoroutine(Belt()); break;
            case Tile.Type.Combiner: break;
            case Tile.Type.Turret: break;
            case Tile.Type.Vortex: break;
            case Tile.Type.Spawner:
                if (start)
                {
                    StartCoroutine(Belt());
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
            default: Destroy(gameObject); break;
        }
    }

    public IEnumerator Vortex()
    {
        Vector3 start = transform.position;
        Vector3 end = location;
        Vector3 difference = end - start;
        float startingScale = transform.localScale.x;
        for (float i = 0; i < 1; i+=Time.deltaTime)
        {
            transform.position = start + difference * i;
            transform.localScale = new Vector3(startingScale * (1-i),startingScale * (1-i));
            yield return null;
        }

        if (!ResourceManager.instance.resources.ContainsKey(shapeType))
        {
            ResourceManager.instance.resources[shapeType] = 0;
        }
        ResourceManager.instance.resources[shapeType] += 1;
        Destroy(gameObject);
    }
    public IEnumerator Belt()
    {
        Tile on = GridController.instance.grid[location];
        Vector2 desinationTile;
        Vector3 start = transform.position;
        Vector3 end;
        if (on.rotation % 2 == 0)
        {
            desinationTile = on.location + new Vector2(0,1-on.rotation);
            end = new Vector3(start.x,desinationTile.y + Random.value * .7f - .35f);
        }
        else
        {
            desinationTile = on.location + new Vector2(on.rotation-2,0);
            end = new Vector3(desinationTile.x + Random.value * .7f - .35f,start.y);
        }

        Vector3 difference = end - start;
        for (float i = 0; i < difference.magnitude; i+=Time.deltaTime)
        {
            transform.position = start + difference * i / difference.magnitude;
            yield return null;
        }

        transform.position = end;
        location = desinationTile;
        CheckLocation(false);
    }
}
