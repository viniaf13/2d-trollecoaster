using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;

    private LevelLoader levelLoader;
    //Singleton
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        levelLoader = GetComponent<LevelLoader>();
    }

    public void ProcessDeath()
    {
        //TODO: GAME OVER LOGIC (WILL IT HAVE?)
    }

    public void TakeLife()
    {
        playerLives--;
        levelLoader.RestartLevel();
    }

    public void ResetGameSession()
    {
        //Load Main Menu
        Destroy(gameObject);
    }
    
}
