using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float loadDelay = 3f;

    private int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private IEnumerator LoadLevelWithDelay(int sceneIndex)
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneIndex);
        currentSceneIndex = sceneIndex;
    }

    public void LoadLevelInstant(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        currentSceneIndex = sceneIndex;
    }

    public void LoadNextLevel()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession)
        {
            gameSession.ResetGameSession();
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
        currentSceneIndex++;
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevelWithDelay(currentSceneIndex));
    }

    public void LoadMainMenu()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession)
        {
            gameSession.ResetGameSession();
        }
        currentSceneIndex = 0;
        SceneManager.LoadScene(currentSceneIndex); //TODO: FIX MAGIC NUMBER
    }
}
