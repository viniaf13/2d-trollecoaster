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
    private IEnumerator LoadLevel(int sceneIndex)
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneIndex);
        currentSceneIndex = sceneIndex;
    }
    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(currentSceneIndex));
    }
}
