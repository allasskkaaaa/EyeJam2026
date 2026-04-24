using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DayManager : MonoBehaviour
{
    private int dayIndex;
    [SerializeField] private int _day = 1;
    [SerializeField] private Animator anim;
    public int day
    {
        get { return _day; }
        set
        {
            _day = value;
            dayIndex = value - 1;
            
        }

    }

    [SerializeField] List<DayData> dayData;


    public void nextDay()
    {
        if (day <= dayData.Count)
        {
            day++;
            StartCoroutine(sleep());
        }
    }

    IEnumerator sleep()
    {
        anim.Play("FadeToBlack");
        yield return new WaitForSeconds(2);
        anim.Play("FadeFromBlack");
    }

    public void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
