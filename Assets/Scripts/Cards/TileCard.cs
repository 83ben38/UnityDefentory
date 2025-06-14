using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TileCard", order = 1)]
public class TileCard : Card
{
    public GameObject prefab;
    public Shape.Type costType;
    public int costAmount;
    public int defaultCount;
}
