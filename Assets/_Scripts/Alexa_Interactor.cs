using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inspect : MonoBehaviour
{
    [SerializeField] private GameObject inspectPanel;
    [SerializeField] private Image imageUI;
    [SerializeField] private TMP_Text nameUI;
    [SerializeField] private TMP_Text descriptionUI;
    [SerializeField] private int inspectIndex = 0;
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
            hit.transform.gameObject.CompareTag("Item"); //Checks if object hit is a memory
            GameObject hitObject = hit.transform.gameObject;
            Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            

            if (hitObject.CompareTag("Item"))
            {
                Debug.Log("Item Hit");
                Item hitItem = hitObject.GetComponent<Item>();
                ItemData hitItemData = hitItem.itemData;
                if (hitItemData != null)
                {
                    openInspectMenu(hitItemData);
                    Debug.Log("Opening Inspect Menu");
                }

                Debug.Log("No item data found");
            }
            
        }
        else
        {
            Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * inspectRange, Color.white);
            Debug.Log("Item not Hit");
        }

    }

    private void openInspectMenu(ItemData item)
    {
        //Uses canvas manager and locks the player screen.
        //Darkens the background
        //Creates a copy of the object detected in inspectInFront()

        isInspecting = true;
        imageUI.sprite = item.itemThumbnail;
        nameUI.text = item.itemName;
        descriptionUI.text = item.itemDescription;
        inspectPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void closeInspectMenu()
    {
        //Closes the menu and brings player back to gameplay
        isInspecting = false;
        inspectPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void InspectBucket(Bucket bucket)
    {
        if (bucket.itemsInBucket.Count < 1)
        {
            inspectIndex = 0;
        }
        
        openInspectMenu(bucket.itemsInBucket[inspectIndex]);
    }

    private void forwardIndex()
    {
        inspectIndex++;
    }

    private void backIndex()
    {
        inspectIndex--;
    }
}
