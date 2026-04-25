using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingController : MonoBehaviour
{
    public static PostProcessingController instance;
    [SerializeField] private Volume postProcessingVolume;
    [SerializeField] private List<VolumeProfile> subtleProfiles;

    private void Awake()
    {
        instance = this;
    }
    public void StartGradientEffect()
    {
        StartCoroutine(CreateGradientEffect());
    }

    public void StartPingPongEffect()
    {
        StartCoroutine(CreatePingPongGradientEffect());
    }

    IEnumerator CreateGradientEffect()
    {
        postProcessingVolume.weight = 0;

        float time = 2;
        float elapsed = 0;

        while (elapsed < time)
        {
            // Calculate t as a percentage of total time (0 to 1)
            float t = elapsed / time;
            postProcessingVolume.weight = Mathf.Lerp(0, 1, t);

            elapsed += Time.deltaTime;
            yield return null; // Wait for next frame
        }
        postProcessingVolume.weight = 1; // Ensures final value is exact

    }

    IEnumerator CreatePingPongGradientEffect()
    {
        postProcessingVolume.weight = 0;

        float time = 2;
        float elapsed = 0;

        while (elapsed < time)
        {
            // Calculate t as a percentage of total time (0 to 1)
            float t = elapsed / time;
            postProcessingVolume.weight = Mathf.PingPong(t, 1);

            elapsed += Time.deltaTime;
            yield return null; // Wait for next frame
        }
       

    }

    IEnumerator RemoveGradientEffect()
    {
        postProcessingVolume.weight = 0;

        float time = 2;
        float elapsed = 0;

        while (elapsed < time)
        {
            // Calculate t as a percentage of total time (0 to 1)
            float t = elapsed / time;
            postProcessingVolume.weight = Mathf.Lerp(0, 1, t);

            elapsed += Time.deltaTime;
            yield return null; // Wait for next frame
        }
        postProcessingVolume.weight = 1; // Ensures final value is exact

    }

    IEnumerator CreateFlashEffect()
    {
        postProcessingVolume.weight = 1;

        yield return new WaitForSeconds(1);

        postProcessingVolume.weight = 0;
    }
    public void clearEffect()
    {
        postProcessingVolume.weight = 0;
    }

    public void setProfile(VolumeProfile newProfile)
    {
        postProcessingVolume.profile = newProfile;
    }

}
