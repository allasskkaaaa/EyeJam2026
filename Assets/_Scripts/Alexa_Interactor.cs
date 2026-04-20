using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspect : MonoBehaviour
{
    private float inspectRange = 5f;
    private bool isInspecting; //true if player is inspecting an object

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inspectInFront();
        }
    }

    private void inspectInFront()
    {
        //Uses raycast to detect object infront
        //If an object that can be inspected is found, start method openInspectMenu
    }

    private void openInspectMenu()
    {
        //Uses canvas manager and locks the player screen.
        //Darkens the background
        //Creates a copy of the object detected in inspectInFront()
    }

    private void closeInspectMenu()
    {
        //Closes the menu and brings player back to gameplay
    }

    private void inspectMovement()
    {
        //Locks player cursor
        //Player presses right-click (1) to rotate object
    }

}
