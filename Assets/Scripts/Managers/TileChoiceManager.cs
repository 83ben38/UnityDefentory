using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileChoiceManager : MonoBehaviour
{
    public static TileChoiceManager instance;
    public TileCard[] shapeMakers;
    public TileCard[] enemyBelts;
    public TileCard[] belts;
    public TileCard[] combiners;
    public TileCard[] turrets;
    private void Awake()
    {
        instance = this;
    }

    public TileCard generateTileCard()
    {
        int value = Random.Range(0, 9);
        if (value < 2)
        {
            return shapeMakers[(int)(Random.value * shapeMakers.Length)];
        }
        if (value < 4)
        {
            return enemyBelts[(int)(Random.value * enemyBelts.Length)];
        }
        if (value < 5)
        {
            return belts[(int)(Random.value * belts.Length)];
        }
        if (value < 7)
        {
            return combiners[(int)(Random.value * combiners.Length)];
        }
        return turrets[(int)(Random.value * turrets.Length)];
    }
}
