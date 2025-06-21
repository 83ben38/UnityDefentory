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
        if (chip && chip.chip == ChipCard.Chip.SpeedUpChip)
        {
            return 2f;
        }

        if (chip && chip.chip == ChipCard.Chip.SpeedDownChip)
        {
            return 0.5f;
        }
        return 1f;
    }
    public float getDamageAddition()
    {
        if (chip &&chip.chip == ChipCard.Chip.DamageUpChip)
        {
            return 2f;
        }
        return 0f;
    }
    public float getPowerMultiplier()
    {
        if ( chip &&chip.chip == ChipCard.Chip.PowerMultiplyChip)
        {
            return 1.5f;
        }
        return 1f;
    }
    public float getPowerAddition()
    {
        if (chip &&chip.chip == ChipCard.Chip.PowerUpChip)
        {
            return 1f;
        }
        return 0f;
    }

    public int getPierceAddition()
    {
        if (chip &&chip.chip == ChipCard.Chip.PierceUpChip)
        {
            return 3;
        }
        return 0;
    }

    public float getRangeAddition()
    {
        if (chip &&chip.chip == ChipCard.Chip.RangeUpChip)
        {
            return 1f;
        }
        return 0f;
    }

    public int getPriceReduction(Shape.Type type)
    {
        if (chip &&chip.chip == ChipCard.Chip.CirclePriceReductionChip && type == Shape.Type.Circle)
        {
            return 1;
        }
        if (chip &&chip.chip == ChipCard.Chip.Tier1PriceReductionChip && type is > Shape.Type.Circle and < Shape.Type.Rectangle)
        {
            return 1;
        }
        if (chip &&chip.chip == ChipCard.Chip.Tier2PriceReductionChip && type >= Shape.Type.Rectangle)
        {
            return 1;
        }
        return 0;
    }
}
