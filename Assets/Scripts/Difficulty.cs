using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Difficulty", order = 1)]
public class Difficulty : ScriptableObject
{
    public List<SpawnCard> spawnCards = new();
    public List<int> weights = new();
    public List<float> startingTimes = new();
    public float difficultyScaling;
    public int lives;
    private int totalWeight = 0;
    private int currentNext = 0;
    public SpawnCard getNextCard(float time)
    {
        while (currentNext < startingTimes.Count && startingTimes[currentNext] < time)
        {
            totalWeight+=weights[currentNext];
            currentNext++;
        }

        int value = Random.Range(0, totalWeight);
        for (int i = 0; i < weights.Count; i++)
        {
            value -= weights[i];
            if (value < 0)
            {
                return spawnCards[i];
            }
        }

        return spawnCards[^1];
    }
}
