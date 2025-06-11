using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnCard", order = 1)]
public class SpawnCard : ScriptableObject
{
    public GameObject prefab;
    public float cost;
}
