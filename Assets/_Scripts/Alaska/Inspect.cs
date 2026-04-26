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
    [SerializeField] private Bucket bucket;
    [SerializeField] private Camera cam;
    [SerializeField] private float inspectRange = 5f;
    [SerializeField] bool isInspecting = false; //true if player is inspecting an object
    public bool IsInspecting => isInspecting;

    private void Update()
    {
        if (isInspecting)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Closing menu");
                closeInspectMenu();
            }
            else
            {
                return;
            }
        }
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    checkInFront();
        //}

        
    }

    private void checkInFront()
    {
        //Uses raycast to detect object infront
        //If an object that can be inspected is found, start method openInspectMenu

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, inspectRange))

        {
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
            } else if (hitObject.CompareTag("Bucket"))
            {
                InspectBucket();
            }
            
        }
        else
        {
            Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * inspectRange, Color.white);
            Debug.Log("Item not Hit");
        }

    }

    public void openInspectMenu(ItemData item)
    {
        //Uses canvas manager and locks the player screen.
        //Darkens the background
        //Creates a copy of the object detected in inspectInFront()

        isInspecting = true;
        imageUI.sprite = item.itemThumbnail;
        nameUI.text = item.itemName;
        descriptionUI.text = item.itemDescription;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        inspectPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void closeInspectMenu()
    {
        //Closes the menu and brings player back to gameplay
        isInspecting = false;
        inspectPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }

    public void InspectBucket()
    {
        if (bucket.totalCollected() <= 0)
        {
            DialogueManager.instance.setDialogue("You haven't caught anything.");
            return;
        }
        
        openInspectMenu(bucket.itemsInBucket[inspectIndex]);
    }

    public void increaseIndex()
    {
        inspectIndex = (inspectIndex + 1) % bucket.itemsInBucket.Count;
        openInspectMenu(bucket.itemsInBucket[inspectIndex]);


    }

    public void decreaseIndex()
    {
        if (bucket.itemsInBucket.Count == 0) return;

        inspectIndex = (inspectIndex - 1 + bucket.itemsInBucket.Count) % bucket.itemsInBucket.Count;
        openInspectMenu(bucket.itemsInBucket[inspectIndex]);
    }

}
