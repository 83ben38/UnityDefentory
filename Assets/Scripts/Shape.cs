using System;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public Type shapeType;
    public enum Type{
        Circle
    }

    public Vector2 location;

    private void FixedUpdate()
    {
        if (!GridController.instance.grid.ContainsKey(location))
        {
            Destroy(gameObject);
        }
        Tile on = GridController.instance.grid[location];
        if (!on)
        {
            Destroy(gameObject);
        }

        switch (on.tileType)
        {
            case Tile.Type.Belt: break;
            case Tile.Type.Combiner: break;
            case Tile.Type.Turret: break;
            case Tile.Type.Vortex: break;
            case Tile.Type.Spawner: break;
            default: Destroy(gameObject); break;
        }
    }
}
