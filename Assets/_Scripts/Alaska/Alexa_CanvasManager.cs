using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseUIObject;
    public void pauseGame()
    {
        if (pauseUIObject == null) return;

        if (pauseUIObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseUIObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseUIObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
