using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] float levelTimeInSeconds = 110f;
    [SerializeField] Vector3 lastCheckPointPos;

    private int finalFruitScore;
    private float finalTimeScore;

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

    private void Update()
    {
        levelTimeInSeconds -= Time.deltaTime;
    }
    public void SaveSessionValues()
    {
        finalTimeScore = levelTimeInSeconds;
        finalFruitScore = FindObjectOfType<ScoreDisplay>().GetScore();
    }

    public float[] GetSessionValues()
    {
        float[] sessionValues = new float[] {finalFruitScore, finalTimeScore};
        return sessionValues;
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

    public float GetCurrentLevelTime()
    {
        return levelTimeInSeconds;
    }

    public int GetPlayerLives()
    {
        return playerLives;
    }
}
