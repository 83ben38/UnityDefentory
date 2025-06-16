using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    public static Difficulty difficulty;
    private float credits = 0;
    private float time = 0f;
    private float difficultyScaling = 0f;
    public static EnemySpawnManager instance;
    public float lastWaveUpdate;
    public GameObject progressBar;
    private float initialScale;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lastWaveUpdate = difficulty.gracePeriod;
        initialScale = progressBar.transform.localScale.x;
        StartCoroutine(giveAdditionalUpgrades(5, true));
    }

    private void FixedUpdate()
    {
        if (time < difficulty.gracePeriod && time + Time.fixedDeltaTime >= difficulty.gracePeriod)
        {
            LivesManager.instance.NextWave();
            StartCoroutine(giveAdditionalUpgrades(2, false));
        }
        time += Time.fixedDeltaTime;
        
        if (time >= difficulty.gracePeriod)
        {
            if (time >= lastWaveUpdate + difficulty.waveTime)
            {
                credits += difficultyScaling * difficulty.waveBonus;
                lastWaveUpdate += difficulty.waveTime;
                LivesManager.instance.NextWave();
            }
            Vector3 scale = progressBar.transform.localScale;
            scale.x = initialScale * (time-lastWaveUpdate) / difficulty.waveTime;
            progressBar.transform.localScale = scale;
            progressBar.transform.localPosition = new Vector3(-93+(scale.x*250),progressBar.transform.localPosition.y);
            difficultyScaling += difficulty.difficultyScaling * Time.fixedDeltaTime;
            credits += difficultyScaling * Time.fixedDeltaTime;
            float randomValue = Random.value;
            if (randomValue < credits / (difficultyScaling * 1000))
            {
                TrySpawnEnemy();
            }
        }
        else
        {
            Vector3 scale = progressBar.transform.localScale;
            scale.x = initialScale * time / difficulty.gracePeriod;
            progressBar.transform.localScale = scale;
            progressBar.transform.localPosition = new Vector3(-93+(scale.x*250),progressBar.transform.localPosition.y);
        }
    }

    public IEnumerator giveAdditionalUpgrades(int num, bool tileOnly)
    {
        for (int i = 0; i < num; i++)
        {
            while (UpgradeSelectionManager.instance.isOverlayActive)
            {
                yield return null;
            }
            UpgradeSelectionManager.instance.setOverlay(tileOnly);
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
