using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component on the UI
   // public GameObject gameControlls;

    private void Start()
    {
        // Initialize the score and update the UI
       // UpdateScoreUI(0);
    }

    private void Update()
    {

    }

    public void UpdateScoreUI(int purple, int green, int red)
    {
        // Update the text on the UI with the current score
        if (scoreText != null)
        {
            Debug.Log("updateing score");
            scoreText.text = "P:" + purple+" G:"+green+" R:"+red;
        }
    }
}
