using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelClearDIsplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fruitPoints = default;
    [SerializeField] TextMeshProUGUI timePoints = default;
    [SerializeField] TextMeshProUGUI totalPoints = default;

    void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        float[] sessionValues = gameSession.GetSessionValues();
        fruitPoints.text = sessionValues[0].ToString("0000");
        timePoints.text = sessionValues[1].ToString("0000");
        totalPoints.text = (sessionValues[0] + sessionValues[1]).ToString("0000");
    }
}
