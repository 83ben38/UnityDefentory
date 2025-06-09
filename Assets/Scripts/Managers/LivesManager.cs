using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    private int lives;
    public static LivesManager instance;
    public TextMeshProUGUI  livesText;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lives = EnemySpawnManager.instance.difficulty.lives;
        livesText.text = lives + "";
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        livesText.text = lives + "";
    }
}
