using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider))]
public class Trap : MonoBehaviour
{
    [SerializeField] float trapLength = 1.0f;

    [Header("Trap parameters")]
    [Header("Audio Trap")]
    [SerializeField] AudioClip clip;

    [Header("Camera Colour Trap")]
    [SerializeField] Camera cam;
    [SerializeField] Color colour;

    [Header("Volume Trap")]
    [SerializeField] Volume volume;
    [SerializeField] VolumeProfile volumeProfile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (volumeProfile != null && volume != null) StartCoroutine(volumeTrap(volumeProfile, volume)); //Debug.Log("Changing Volume");

            if (clip != null) AudioManager.instance.playSFX(clip); //Debug.Log("Playing " + clip.name);

            if (cam != null && colour != null) StartCoroutine(cameraTrap(colour, cam)); //Debug.Log("Changing Camera");

            

            
        }
    }

    IEnumerator cameraTrap(Color colour, Camera cam)
    {
        Debug.Log("Changing camera background");
        Color OGColour = cam.backgroundColor;
        cam.backgroundColor = colour;
        yield return new WaitForSeconds(trapLength);
        cam.backgroundColor = OGColour;
        GameObject.Destroy(gameObject);
    }

    IEnumerator volumeTrap(VolumeProfile volumeProfile, Volume volume)
    {
        Debug.Log("Changing volume");
        VolumeProfile OGProfile = volume.profile;
        volume.profile = volumeProfile;
        yield return new WaitForSeconds(trapLength);
        Debug.Log("Returning volume");
        volume.profile = OGProfile;
        GameObject.Destroy(gameObject);
    }
}
