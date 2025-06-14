using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ChipCard", order = 3)]
public class ChipCard : Card
{
    [TextArea]
    public string cardText;
    public Chip chip;
    public enum Chip
    {
        
    }
}
