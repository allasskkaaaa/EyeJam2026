using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_BoatRocking : MonoBehaviour
{
    [Header("Rocking")]
    public float idleRockAmount;
    public float idleRockSpeed;

    // Update is called once per frame
    void Update()
    {
        BoatRocking();
    }
    void BoatRocking()
    {
        float rockX = Mathf.Sin(Time.time * idleRockSpeed) * idleRockAmount;
        float rockZ = Mathf.Cos(Time.time * idleRockSpeed * 0.7f) * idleRockAmount;
        transform.localRotation = Quaternion.Euler(rockX, transform.localRotation.eulerAngles.y, rockZ);
    }
}
