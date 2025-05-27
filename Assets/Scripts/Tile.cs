using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 location;
    public int rotation;
    public Type tileType;
    public enum Type{
        Belt,
        EnemyBelt,
        Spawner,
        EnemySpawner,
        Vortex,
        EnemyVortex,
        Combiner,
        Turret
    }
}
