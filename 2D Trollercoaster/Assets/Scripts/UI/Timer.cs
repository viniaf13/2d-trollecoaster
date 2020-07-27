using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float levelTimeInSeconds = 110f;
    [SerializeField] float timeAlert = 100f;
    [SerializeField] GameObject display = default;

    private TextMeshProUGUI timeText;
    private bool isRunningOutOfTime = false;

    void Start()
    {
        timeText = display.GetComponent<TextMeshProUGUI>();
        timeText.text = levelTimeInSeconds.ToString("000");
    }

    void Update()
    {
        RunTimer();
    }

    private void RunTimer()
    {
        levelTimeInSeconds -= Time.deltaTime;
        timeText.text = levelTimeInSeconds.ToString("000");
        if (isRunningOutOfTime) return;
        if (levelTimeInSeconds <= timeAlert)
        {
            GetComponent<Animator>().SetTrigger(Constants.Animations.Hurry);
            isRunningOutOfTime = true;
        }
    }
}
