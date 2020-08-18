using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Vector3 lastCheckPointPos;

    //Singleton
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void TakeLife()
    {
        playerLives--;
        FindObjectOfType<LevelLoader>().RestartLevel();
    }

    public void ResetGameSession()
    {
        Destroy(gameObject);
    }

    public int GetPlayerLives()
    {
        return playerLives;
    }

    public void SetLastCP(Vector3 checkpointPos)
    {
        lastCheckPointPos = checkpointPos;
        Checkpoint[] sceneCheckPoints = FindObjectsOfType<Checkpoint>();
        if (sceneCheckPoints.Length >= 1)
        {
            foreach (Checkpoint cp in sceneCheckPoints)
            {
                if (cp.transform.position != checkpointPos)
                {
                    cp.DisableCP();
                }
            }
        }
    }

    public Vector3 GetLastCP()
    {
        return lastCheckPointPos;
    }
}
