using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreDisplay : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString("D4");
    }
    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString("D4");
    }
    public int GetScore()
    {
        return score;
    }
}
