using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeAlert = 100f;
    [SerializeField] GameObject display = default;

    private TextMeshProUGUI timeText;
    private bool isRunningOutOfTime = false;
    private GameSession gameSession;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        timeText = display.GetComponent<TextMeshProUGUI>();
        timeText.text = gameSession.GetCurrentLevelTime().ToString("000");
    }

    void Update()
    {
        RunTimer();
    }

    private void RunTimer()
    {
        float levelTimeInSeconds = gameSession.GetCurrentLevelTime();
        timeText.text = levelTimeInSeconds.ToString("000");
        if (isRunningOutOfTime) return;
        if (levelTimeInSeconds <= timeAlert)
        {
            GetComponent<Animator>().SetTrigger(Constants.Animations.Hurry);
            isRunningOutOfTime = true;
        }
    }
}
