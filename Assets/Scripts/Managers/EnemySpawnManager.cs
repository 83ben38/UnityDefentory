using System;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float timeLeft;

    private void FixedUpdate()
    {
        if (timeLeft <= 0)
        {
            Instantiate(enemyPrefab).transform.position = Vector3.zero;
            timeLeft += 3;
        }
        timeLeft -= Time.fixedDeltaTime;
    }
}
