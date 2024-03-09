using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    float maxHealth;
    public void SetMaxHealth(float maxHealth)
    {
        scoreText.text = "Health:"+maxHealth;
    }

    public void SetHealth(float health)
    {
        scoreText.text = "Health:" + health;
    }
}
