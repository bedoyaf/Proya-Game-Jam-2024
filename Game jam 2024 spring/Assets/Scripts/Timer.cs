using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] GameObject gameOverCanvas;

    bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0f;
                UpdateTimerDisplay();
                timerText.color = Color.red;
                GameOver();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        // Set isGameOver flag to true to prevent further execution of Update() method
        isGameOver = true;
        gameOverCanvas.SetActive(true);
    }
}
