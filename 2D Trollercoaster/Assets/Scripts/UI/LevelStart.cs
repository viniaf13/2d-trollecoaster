using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifeText = default;
    [SerializeField] GameObject gameCanvas = default;

    private bool playerControl = true;
    
    void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession)
        {
            lifeText.text = "x  " + gameSession.GetPlayerLives().ToString("00");
        }
        else
        {
            Debug.LogError("GameSession not found!");
        }
    }

    public void TogglePlayerControl()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            playerControl = !playerControl;
            player.GrantPlayerControl(playerControl);
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    public void EnableGameCanvas()
    {
        FindObjectOfType<CanvasController>().SwitchCanvas(gameCanvas);
    }
}
