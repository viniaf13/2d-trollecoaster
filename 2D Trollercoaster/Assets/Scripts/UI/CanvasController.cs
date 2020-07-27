using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] List<GameObject> sceneCanvases = default;

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
