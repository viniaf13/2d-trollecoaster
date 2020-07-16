using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float loadDelay = 3f;

    private int currentSceneIndex;
    private enum Levels
    {
        MainMenu = 0,
        LevelSelectionScreen = 1,
        Level1 = 2,
    }

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

    private void LoadLevelInstant(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        currentSceneIndex = sceneIndex;
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevelWithDelay(currentSceneIndex));
    }
}
