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
    public GameObject gameOverScreen;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lives = EnemySpawnManager.difficulty.lives;
        wave = 0;
        waveText.text = "Wave " + wave;
        livesText.text = lives + "";
        gameOverScreen.SetActive(false);
    }

    public void NextWave()
    {
        wave++;
        waveText.text = "Wave " + wave;
        UpgradeSelectionManager.instance.setOverlay();
    }
    public void TakeDamage(int damage)
    {
        lives -= damage;
        livesText.text = lives + "";
        if (lives <= 0)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
