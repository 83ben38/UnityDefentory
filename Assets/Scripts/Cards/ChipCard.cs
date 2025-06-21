using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ChipCard", order = 3)]
public class ChipCard : Card
{
    [TextArea]
    public string cardText;
    public Chip chip;
    public enum Chip
    {
        SpeedUpChip,
        SpeedDownChip,
        PowerUpChip,
        PowerMultiplyChip,
        RangeUpChip,
        PierceUpChip,
        CirclePriceReductionChip,
        Tier1PriceReductionChip,
        Tier2PriceReductionChip,
        DamageUpChip,
    }
}
