using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu = default;
    [SerializeField] GameObject levelSelection = default;
    [SerializeField] GameObject options = default;

    private GameObject[] sceneCanvases;

    private void Start()
    {
        sceneCanvases = new GameObject[3] { mainMenu, levelSelection, options};
    }

    public void SwitchCanvas(GameObject targetCanvas)
    {
        foreach (GameObject canvas in sceneCanvases)
        {
            if (canvas == targetCanvas)
            {
                canvas.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }
}
