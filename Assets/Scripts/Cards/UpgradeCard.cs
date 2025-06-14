using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UpgradeCard", order = 2)]
public class UpgradeCard : Card
{
    [TextArea]
    public string cardText;

    public Upgrade upgrade;
    public enum Upgrade
    {
        
    }
}
