using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fishable Item")]
public class Fishable : ScriptableObject
{
    public GameObject inspectionObject;
    public string objectName;
    public FishableType fishableType;

    public enum FishableType
    {
        Fish,
        Memory
    }
}
