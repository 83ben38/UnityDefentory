using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FastForwardController : MonoBehaviour
{
    public static float currentTimeSpeed = 1f;
    public TextMeshProUGUI text;
    public Image image;

    public void toggleFastForward()
    {
        if (currentTimeSpeed < 2f)
        {
            currentTimeSpeed = 3f;
            text.text = "Fast Forward On";
            image.color = Color.green;
        }
        else
        {
            currentTimeSpeed = 1f;
            text.text = "Fast Forward Off";
            image.color = Color.red;
        }

        if (Time.timeScale > 0f)
        {
            Time.timeScale = currentTimeSpeed;
        }
    }
}
