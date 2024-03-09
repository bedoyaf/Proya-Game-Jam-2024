using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum EnemyType
    {
        Purple=1,
        Green=2,
        Red=3
    }
    public Timer timer;
    public int purplePoints = 0;
    public int greenPoints = 5;
    public int redPoints = 0;

    private static GameController instance;
    public ScoreManager scoreManager;
    public void Start()
    {
        scoreManager.UpdateScoreUI(purplePoints, greenPoints, redPoints);
    }
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
                if (instance == null)
                {
                    instance = new GameObject("GameController").AddComponent<GameController>();
                }
            }
            return instance;
        }
    }
    public void AddPoints(int enemy, int points)
    {
        EnemyType type = (EnemyType)enemy;
        switch(type)
        {
            case EnemyType.Purple:
                purplePoints += points;
                break;
            case EnemyType.Green:
                greenPoints += points;
                break;
            case EnemyType.Red:
                redPoints += points;
                break;
            default:
                Debug.Log("Wrong enemy type");
                break;
        }
        scoreManager.UpdateScoreUI(purplePoints,greenPoints,redPoints);
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("pressed1");
            if(purplePoints>=5)
            {
                purplePoints = 0;
                timer.Add_to_remaining_time(40);
                scoreManager.UpdateScoreUI(purplePoints, greenPoints, redPoints);
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("pressed2");
            if (greenPoints >= 5)
            {
                greenPoints = 0;
                timer.Add_to_remaining_time(40);
                scoreManager.UpdateScoreUI(purplePoints, greenPoints, redPoints);
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("pressed3");
            if (redPoints >= 5)
            {
                redPoints = 0;
                timer.Add_to_remaining_time(40);
                scoreManager.UpdateScoreUI(purplePoints, greenPoints, redPoints);
            }

        }
    }
}
