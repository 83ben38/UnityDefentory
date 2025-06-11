using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    private int lives;
    public static LivesManager instance;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI waveText;
    private int wave;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lives = EnemySpawnManager.instance.difficulty.lives;
        wave = 0;
        waveText.text = "Wave " + wave;
        livesText.text = lives + "";
    }

    public void NextWave()
    {
        wave++;
        waveText.text = "Wave " + wave;
    }
    public void TakeDamage(int damage)
    {
        lives -= damage;
        livesText.text = lives + "";
    }
}
