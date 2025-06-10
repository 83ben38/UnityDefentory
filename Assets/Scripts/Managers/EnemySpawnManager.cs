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
    public static EnemySpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        difficultyScaling += difficulty.difficultyScaling * Time.fixedDeltaTime;
        credits += difficultyScaling *  Time.fixedDeltaTime;
        float randomValue = Random.value;
        if (randomValue < credits / (difficultyScaling * 1000))
        {
            TrySpawnEnemy();
        }
    }

    public float[] sizes;
    public void TrySpawnEnemy()
    {
        int numWavesToDo = 1;
        while (Random.value < 0.4f && numWavesToDo < 4)
        {
            numWavesToDo++;
        }

        float toAdd = credits / numWavesToDo;
        credits = 0;
        for (int j = 0; j < numWavesToDo; j++)
        {
            credits += toAdd;
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
                StartCoroutine(WaitSpawn(sc.prefab, 0, size));
                //one big enemy
            }
            else if (randomValue < 2 / 3f)
            {
                int size = 1;
                while (credits >= sc.cost * Mathf.Pow(5, size) * 2)
                {
                    size++;
                }
    
                int toSpawn = 0;
                while (credits >= sc.cost * Mathf.Pow(5, size-1))
                { 
                    credits -= sc.cost * Mathf.Pow(5, size-1);
                    toSpawn++;
                }
                for (int i = 0; i < toSpawn; i++)
                {
                    StartCoroutine(WaitSpawn(sc.prefab,i*0.5f,size));
                }
                //spaced enemies
            }
            else
            {
                int size = 1;
                while (credits >= sc.cost * Mathf.Pow(5, size) * 5)
                {
                    size++;
                }
    
                int toSpawn = 0;
                while (credits >= sc.cost * Mathf.Pow(5, size-1))
                { 
                    credits -= sc.cost * Mathf.Pow(5, size-1);
                    toSpawn++;
                }
                
                for (int i = 0; i < toSpawn; i++)
                {
                    StartCoroutine(WaitSpawn(sc.prefab,i*0.2f,size));
                }
                // grouped enemies
            }
        }
        
    }

    public IEnumerator WaitSpawn(GameObject prefab, float timeToWait, int size)
    {
        float time = 0;
        float scale = size >= sizes.Length ? sizes[^1] : sizes[size];
        while (time < timeToWait)
        {
            time += Time.deltaTime;
            yield return null;
        }
        GameObject go = Instantiate(prefab);
        go.transform.localScale = new Vector3(scale,scale);
        go.GetComponent<Enemy>().size = size;
    }
}
