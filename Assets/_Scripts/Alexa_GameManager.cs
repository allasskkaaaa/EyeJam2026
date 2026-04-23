using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alexa_GameManager : MonoBehaviour
{
    int day;
    
    int memoriesFound;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
}
