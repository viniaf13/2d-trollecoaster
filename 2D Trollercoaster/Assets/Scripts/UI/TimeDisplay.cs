using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] float levelTimeInSeconds = 10;

    private TextMeshProUGUI timeText;
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        timeText.text = levelTimeInSeconds.ToString("000");
    }

    void Update()
    {
        levelTimeInSeconds -= Time.deltaTime;
        timeText.text = levelTimeInSeconds.ToString("000");
    }
}
