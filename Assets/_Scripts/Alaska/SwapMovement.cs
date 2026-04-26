using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class SwapMovement : MonoBehaviour
{
    [SerializeField] PlayerMovement walkingPlayer;
    [SerializeField] Transform playerHead;
    [SerializeField] Jack_BoatMovement boatPlayer;
    [SerializeField] Transform boatHead;
    [SerializeField] CinemachineVirtualCamera cinemaCam;
    [SerializeField] bool isWalking = true;
    [SerializeField] bool triggered;

    public void Update()
    {
     if (triggered)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (isWalking)
                {
                    becomeBoat();
                }
                else if (Input.GetKeyUp(KeyCode.E) && DayManager.instance.hasFished)
                {
                    becomePlayer();
                } else if (Input.GetKeyUp(KeyCode.E) && !DayManager.instance.hasFished)
                {
                    DialogueManager.instance.setDialogue("I haven't finished fishing yet");
                }
            }
        }   
    }
    public void becomeBoat()
    {
        Debug.Log("Swapping to boat");
        isWalking = false;
        walkingPlayer.enabled = false;
        boatPlayer.enabled = true;
        walkingPlayer.transform.gameObject.SetActive(false);
        cinemaCam.Follow = boatHead;
        Debug.Log("Swapped to boat");
    }

    public void becomePlayer()
    {

        Debug.Log("Swapping to walking");
        isWalking = true;
        walkingPlayer.enabled = true;
        boatPlayer.enabled = false;
        walkingPlayer.transform.gameObject.SetActive(true);
        cinemaCam.Follow = playerHead;
        Debug.Log("Swapped to walking");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.instance.setDialogue("[E] Enter Boat");
            triggered = true;
        }  
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
  
            triggered = false;
        }
    }
}
