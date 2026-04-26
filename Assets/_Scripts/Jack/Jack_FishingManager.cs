using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_FishingManager : MonoBehaviour
{
    public static Jack_FishingManager Instance;
    public List<Fishable> fishables = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Fishable GetRandomFish()
    {
        return fishables[Random.Range(0, fishables.Count)];
    }

}
