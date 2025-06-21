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
    public float getSpeedMultiplier(Combiner c = null)
    {
        float speedMultiplier = 1f;
        if (chip && chip.chip == ChipCard.Chip.SpeedUpChip)
        {
            speedMultiplier *= 2;
        }

        if (chip && chip.chip == ChipCard.Chip.SpeedDownChip)
        {
            speedMultiplier *= 0.5f;
        }

        if (tileType == Type.EnemyBelt && UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.ReducedSpeedBelts))
        {
            speedMultiplier *= 0.8f;
        }

        if (c &&
            UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.IncreaseAdvancedCombiners) && c.requirements2.Count > 1)
        {
            speedMultiplier *= 1.5f;
        }
        return speedMultiplier;
    }
    public float getDamageAddition()
    {
        float damageAddition = 0f;
        if (chip &&chip.chip == ChipCard.Chip.DamageUpChip)
        {
            damageAddition += 2f;
        }

        if (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.DamageForPaths))
        {
            damageAddition += EnemyPathingManager.instance.getNumPaths() - 1f;
        }
        return damageAddition;
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
        float rangeAddition = 0f;
        if (chip &&chip.chip == ChipCard.Chip.RangeUpChip)
        {
            rangeAddition++;
        }

        if (UpgradeManager.instance.isUpgradeAvailable(UpgradeCard.Upgrade.RangeForProximity) &&
            location.sqrMagnitude <= 16)
        {
            rangeAddition++;
        }
        return rangeAddition;
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
