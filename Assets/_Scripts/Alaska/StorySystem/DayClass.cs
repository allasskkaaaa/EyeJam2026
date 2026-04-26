using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDay", menuName = "Inventory/Day", order = 2)]
public class DayData : ScriptableObject
{
    public int day;
    public int fishingMinimum; //How many fish needs to be caught to progress the day;
    public float fogDistanceEnd = 400;
    public Color skyColour;

    //Put the daily fishing pool

}
