using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public UnityEvent OnChangeDay;
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

    [SerializeField] List<GameObject> dailyTerrainData;
    [SerializeField] List<GameObject> dailyBoat;

    [SerializeField] GameObject sleepTrigger;
    [SerializeField] public bool hasFished = true;

    public void nextDay()
    {
        if (hasFished)
        {
            if (day < dayData.Count)
            {            
                StartCoroutine(sleep());
            }
            else
            {
                Debug.Log("No more days");
            }
        }
        else
        {
            DialogueManager.instance.setDialogue("I haven't fished yet");
            return;
        }

    }

    IEnumerator sleep()
    {
        anim.Play("FadeToBlack");
        yield return new WaitForSeconds(2);
        changeTerrain(dayIndex);
        changeBoat(dayIndex);
        OnChangeDay?.Invoke();
        day++;
        anim.Play("FadeFromBlack");
    }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        instance = this;
    }

    private void changeTerrain(int currentIndex)
    {
        if (currentIndex + 1 > dailyTerrainData.Count)
        {
            //If there is no more data for the day, return
            return;
        }
        else
        {
            dailyTerrainData[currentIndex].SetActive(false);
            dailyTerrainData[currentIndex + 1].SetActive(true);
        }

    }

    private void changeBoat(int currentIndex)
    {
        if (currentIndex + 1 > dailyBoat.Count)
        {
            //If there is no more data for the day, return
            return;
        }
        else
        {
            dailyBoat[currentIndex].SetActive(false);
            dailyBoat[currentIndex + 1].SetActive(true);
        }

    }
}
