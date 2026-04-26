using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class SwapMovement : MonoBehaviour
{
    public UnityEvent<bool> OnSwappedMovement;

    [SerializeField] PlayerMovement walkingPlayer;
    [SerializeField] Transform playerHead;
    [SerializeField] Jack_BoatMovement boatPlayer;
    [SerializeField] Transform boatHead;
    [SerializeField] Collider boatCollider;
    [SerializeField] CinemachineVirtualCamera cinemaCam;
    [SerializeField] bool isWalking = true;
    [SerializeField] bool doneFishing;

    public void becomeBoat()
    {
        //isWalking = false;
        walkingPlayer.enabled = false;
        boatPlayer.enabled = true;
        boatCollider.enabled = true;
        walkingPlayer.transform.gameObject.SetActive(false);
        cinemaCam.Follow = boatHead;
    }

    public void becomePlayer()
    {
        //isWalking = true;
        walkingPlayer.enabled = true;
        boatPlayer.enabled = false;
        boatCollider.enabled = false;
        walkingPlayer.transform.gameObject.SetActive(true);
        cinemaCam.Follow = playerHead;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " has entered the movement swap trigger");
        if (other.CompareTag("Player") && isWalking)
        {
            DialogueManager.instance.setDialogue("[E] Enter Boat");
        } else if (other.CompareTag("Boat") && !isWalking && DayManager.instance.hasFished)
        {
            DialogueManager.instance.setDialogue("[E] Exit Boat");
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (isWalking && !DayManager.instance.hasFished)
                {
                    OnSwappedMovement?.Invoke(isWalking);
                    becomeBoat();
                }
            }
        }
        if (other.CompareTag("Boat"))
        {
            if (Input.GetKeyUp(KeyCode.E) && DayManager.instance.hasFished)
            {
                OnSwappedMovement?.Invoke(isWalking);
                becomePlayer();
            }
            else if (Input.GetKeyUp(KeyCode.E) && !DayManager.instance.hasFished)
            {
                DialogueManager.instance.setDialogue("I haven't finished fishing yet");
            }
        }
        
    }
}
