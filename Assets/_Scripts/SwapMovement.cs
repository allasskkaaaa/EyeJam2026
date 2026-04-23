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
    public void becomeBoat()
    {
        walkingPlayer.enabled = false;
        boatPlayer.enabled = true;
        walkingPlayer.transform.gameObject.SetActive(false);
        cinemaCam.Follow = boatHead;

    }

    public void becomePlayer()
    {
        walkingPlayer.enabled = true;
        boatPlayer.enabled = false;
        walkingPlayer.transform.gameObject.SetActive(true);
        cinemaCam.Follow = playerHead;

    }

    private void OnTriggerEnter(Collider other)
    {
        DialogueManager.instance.setDialogue("[E]");
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isWalking)
            {
                becomeBoat();
            }
            else
            {
                becomePlayer();
            }
        }
        
    }
}
