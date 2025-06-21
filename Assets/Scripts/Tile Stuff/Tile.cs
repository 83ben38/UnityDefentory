using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int location;
    public int rotation;
    public Type tileType;
    public TileCard spawnCard;
    public ChipCard chip;
    public enum Type{
        Belt,
        EnemyBelt,
        Spawner,
        EnemySpawner,
        Vortex,
        EnemyVortex,
        Combiner,
        Turret,
        Splitter,
        Tunnel
    }
    public float getSpeedMultiplier()
    {
        return 1f;
    }
    public float getDamageAddition()
    {
        return 0f;
    }
    public float getPowerMultiplier()
    {
        return 1f;
    }
    public float getPowerAddition()
    {
        return 0f;
    }

    public int getPierceAddition()
    {
        return 0;
    }

    public float getRangeAddition()
    {
        return 0f;
    }

    public int getPriceReduction(Shape.Type type)
    {
        return 0;
    }
}
