using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    public Difficulty difficulty;
    public float credits = 0;
    public float time = 0f;
    public float difficultyScaling = 0f;
    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        difficultyScaling += difficulty.difficultyScaling * Time.fixedDeltaTime;
        credits += difficultyScaling *  Time.fixedDeltaTime;
        float randomValue = Random.value;
        if (randomValue < credits / (difficultyScaling * 100))
        {
            TrySpawnEnemy();
        }
    }

    public float[] sizes;
    public void TrySpawnEnemy()
    {
        SpawnCard sc = difficulty.getNextCard(time);
        if (sc.cost > credits)
        {
            return;
        }
        float randomValue = Random.value;
        if (randomValue < 1 / 3f)
        {
            int size = 1;
            while (credits >= sc.cost * Mathf.Pow(5, size))
            {
                size++;
            }
            credits -= sc.cost * Mathf.Pow(5, size-1);
            float scale = size >= sizes.Length ? sizes[^1] : sizes[size];
            Instantiate(sc.prefab).transform.localScale = new Vector3(scale,scale);
            //one big enemy
        }
        else if (randomValue < 2 / 3f)
        {
            int size = 1;
            while (credits >= sc.cost * Mathf.Pow(5, size) * 3)
            {
                size++;
            }

            int toSpawn = 0;
            while (credits >= sc.cost * Mathf.Pow(5, size-1))
            { 
                credits -= sc.cost * Mathf.Pow(5, size-1);
                toSpawn++;
            }
            float scale = size >= sizes.Length ? sizes[^1] : sizes[size];
            for (int i = 0; i < toSpawn; i++)
            {
                Instantiate(sc.prefab).transform.localScale = new Vector3(scale,scale);
            }
            //spaced enemies
        }
        else
        {
            int size = 1;
            while (credits >= sc.cost * Mathf.Pow(5, size) * 10)
            {
                size++;
            }

            int toSpawn = 0;
            while (credits >= sc.cost * Mathf.Pow(5, size-1))
            { 
                credits -= sc.cost * Mathf.Pow(5, size-1);
                toSpawn++;
            }
            float scale = size >= sizes.Length ? sizes[^1] : sizes[size];
            for (int i = 0; i < toSpawn; i++)
            {
                Instantiate(sc.prefab).transform.localScale = new Vector3(scale,scale);
            }
            // grouped enemies
        }
    }

    public IEnumerator WaitSpawn(GameObject prefab, Vector3 scale, float timeToWait)
    {
        float time = 0;
        while (time < timeToWait)
        {
            time += Time.deltaTime;
            yield return null;
        }
        Instantiate(prefab).transform.localScale = scale;
    }
}
