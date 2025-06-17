using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DifficultySelector : MonoBehaviour
{
    public List<Difficulty> difficulties;
    public GameObject buttonPrefab;
    public Transform content;
    public void show()
    {
        gameObject.SetActive(true);
        foreach (Difficulty difficulty in difficulties)
        {
            GameObject newButton = Instantiate(buttonPrefab, content, false);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = difficulty.name;
            newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                EnemySpawnManager.difficulty = difficulty;
                SceneManager.LoadScene(1);
            });
        }
    }
}
