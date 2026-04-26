using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseUIObject;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            togglePause(pauseUIObject);
        }
    }
    public void toggleMenu(GameObject menu)
    {
        if (menu == null) return;

        if (menu.activeSelf)
        {
            menu.SetActive(false);
            
        }
        else
        {
            menu.SetActive(true);
            
        }
    }

    public void togglePause(GameObject menu)
    {
        if (menu == null) return;

        if (menu.activeSelf)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            toggleMenu(menu);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            toggleMenu(menu);
        }
    }

}
