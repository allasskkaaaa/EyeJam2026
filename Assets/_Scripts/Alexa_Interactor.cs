using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspect : MonoBehaviour
{
    [SerializeField] private Transform holdPosition;
    [SerializeField] private GameObject inspectPanel;
    [SerializeField] private Camera cam;
    private float inspectRange = 5f;
    private bool isInspecting = false; //true if player is inspecting an object

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            checkInFront();
        }

        if (isInspecting)
        {
            if (Input.GetMouseButtonDown(1))
            {
                inspectMovement();
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                closeInspectMenu();
            }

        }
            

    }

    private void checkInFront()
    {
        //Uses raycast to detect object infront
        //If an object that can be inspected is found, start method openInspectMenu

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, inspectRange))

        {
            Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            openInspectMenu();
        }
        else
        {
            Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * inspectRange, Color.white);
            Debug.Log("Did not Hit");
        }

    }

    private void openInspectMenu()
    {
        //Uses canvas manager and locks the player screen.
        //Darkens the background
        //Creates a copy of the object detected in inspectInFront()

        isInspecting = true;
        inspectPanel.SetActive(true);
    }

    private void closeInspectMenu()
    {
        //Closes the menu and brings player back to gameplay
        isInspecting = false;
        inspectPanel.SetActive(false);
    }

    private void inspectMovement()
    {
        //Locks player cursor
        //Player presses right-click (1) to rotate object
    }

}
