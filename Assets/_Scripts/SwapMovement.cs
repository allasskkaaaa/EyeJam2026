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

    public void becomeBoat()
    {
        walkingPlayer.enabled = false;
        boatPlayer.enabled = true;
        cinemaCam.Follow = boatHead;

    }

    public void becomePlayer()
    {
        walkingPlayer.enabled = true;
        boatPlayer.enabled = false;
        cinemaCam.Follow = playerHead;

    }


}
