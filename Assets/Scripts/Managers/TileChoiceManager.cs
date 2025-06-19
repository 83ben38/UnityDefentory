using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileChoiceManager : MonoBehaviour
{
    public static TileChoiceManager instance;
    public TileCard[] enemyBelts;
    public TileCard[] belts;
    public TileCard[] turrets;
    public TileCard circleSpawner;
    public GameObject[] tier1Prefabs;
    public GameObject[] tier2Prefabs;
    [SerializeField]
    private TileCard[] tier1Spawners;
    [SerializeField]
    private TileCard[] tier1Combiners;
    [SerializeField]
    private TileCard[] tier1AdvancedCombiners;
    [SerializeField]
    private TileCard[] tier2Combiners;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tier1Spawners = new TileCard[tier1Prefabs.Length];
        for (int i = 0; i < tier1Prefabs.Length; i++)
        {
            tier1Spawners[i] = ScriptableObject.CreateInstance<TileCard>();
            tier1Spawners[i].costAmount = 20;
            tier1Spawners[i].costType = Shape.Type.Circle;
            tier1Spawners[i].defaultCount = 1;
            GameObject go = Instantiate(tier1Prefabs[i]);
            go.SetActive(false);
            Combiner c = go.GetComponent<Combiner>();
            ShapeSpawn ss = go.transform.GetChild(1).gameObject.AddComponent<ShapeSpawn>();
            ss.prefab = c.prefab;
            ss.cooldown = 4;
            Destroy(c);
            tier1Spawners[i].prefab = go;
            tier1Spawners[i].description = "Creates a " + (i + Shape.Type.Square) + " every 4 seconds.";
            tier1Spawners[i].name = (i + Shape.Type.Square) + " Maker";
        }
        tier1Combiners = new TileCard[tier1Prefabs.Length];
        for (int i = 0; i < tier1Prefabs.Length; i++)
        {
            tier1Combiners[i] = ScriptableObject.CreateInstance<TileCard>();
            tier1Combiners[i].costAmount = 10;
            tier1Combiners[i].costType = Shape.Type.Circle;
            tier1Combiners[i].defaultCount = 1;
            tier1Combiners[i].prefab = tier1Prefabs[i];
            tier1Combiners[i].description = "Creates a " + (i + Shape.Type.Square) + " every 2 seconds using 3 Circles.";
            tier1Combiners[i].name = (i + Shape.Type.Square) + " Crafter";
        }

        tier1AdvancedCombiners = new TileCard[tier1Prefabs.Length * (tier1Prefabs.Length - 1)];
        for (int i = 0; i < tier1Prefabs.Length; i++)
        {
            for (int j = 0; j < tier1Prefabs.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }

                int z = i * (tier1Prefabs.Length - 1) + j;
                if (j > i)
                {
                    z--;
                }
                tier1AdvancedCombiners[z] = ScriptableObject.CreateInstance<TileCard>();
                tier1AdvancedCombiners[z].costAmount = 10;
                tier1AdvancedCombiners[z].costType = j + Shape.Type.Square;
                tier1AdvancedCombiners[z].defaultCount = 1;
                GameObject go = Instantiate(tier1Prefabs[i]);
                go.SetActive(false);
                Combiner c = go.GetComponent<Combiner>();
                c.spawnAmount = 2;
                c.requirements.RemoveAt(0);
                c.requirements.Add(j + Shape.Type.Square);
                tier1AdvancedCombiners[z].prefab = go;
                tier1AdvancedCombiners[z].description = "Creates 2 " + (i + Shape.Type.Square) + " every 2 seconds using 2 Circles and 1 " + (j + Shape.Type.Square);
                tier1AdvancedCombiners[z].name = (i + Shape.Type.Square) + " Crafter (Using " +(j + Shape.Type.Square) + ")";
            }
        }
        
        tier2Combiners = new TileCard[tier2Prefabs.Length];
        for (int i = 0; i < tier2Prefabs.Length; i++)
        {
            tier2Combiners[i] = ScriptableObject.CreateInstance<TileCard>();
            tier2Combiners[i].costAmount = 10;
            tier2Combiners[i].costType = i + Shape.Type.Square;
            tier2Combiners[i].defaultCount = 1;
            tier2Combiners[i].prefab = tier2Prefabs[i];
            tier2Combiners[i].description = "Creates a " + (i + Shape.Type.Rectangle) + " every 4 seconds using 3 " + (i + Shape.Type.Square);
            tier2Combiners[i].name = (i + Shape.Type.Rectangle) + " Crafter";
        }
    }

    public TileCard generateTileCard()
    {
        int value = Random.Range(0, 9);
        if (value < 2)
        {
            if (Random.Range(0, 3) == 0)
            {
                return tier1Spawners[(int)(Random.value * tier1Spawners.Length)];
            }

            return circleSpawner;
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
            if (Random.Range(0, 3) == 0)
            {
                return tier2Combiners[(int)(Random.value * tier2Combiners.Length)];
            }
            if (Random.Range(0, 2) == 0)
            {
                return tier1Combiners[(int)(Random.value * tier1Combiners.Length)];
            }
            return tier1AdvancedCombiners[(int)(Random.value * tier1AdvancedCombiners.Length)];
        }
        return turrets[(int)(Random.value * turrets.Length)];
    }
}
