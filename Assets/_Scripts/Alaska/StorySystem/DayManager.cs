using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public UnityEvent OnChangeDay;
    public int dayIndex;
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

    [SerializeField] public List<DayData> dayData;

    [SerializeField] Camera cam;

    [SerializeField] List<GameObject> dailyTerrainData;
    [SerializeField] List<GameObject> dailyBoat;
    [SerializeField] List<AudioClip> dailyMusic;

    [SerializeField] GameObject sleepTrigger;
    [SerializeField] private TMP_Text fishTracker;
    public int fishCaught;
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
        changeMusic(dayIndex);
        OnChangeDay?.Invoke();
        day++;
        hasFished = false;
        RenderSettings.fogEndDistance = dayData[dayIndex].fogDistanceEnd;
        cam.backgroundColor = dayData[dayIndex].skyColour;
        anim.Play("FadeFromBlack");
    }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        instance = this;
        setTracker(dayData[1]);
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

    private void changeMusic(int currentIndex)
    {
        if (currentIndex + 1 > dailyMusic.Count)
        {
            //If there is no more data for the day, return
            return;
        }
        else
        {
            AudioManager.instance.changeSong(dailyMusic[currentIndex]);
        }

    }

    public void setTracker(DayData day)
    {
        fishTracker.text = "Fish Caught: 0/" + day.fishingMinimum;
        fishCaught = 0;
    }

    public void increaseTracker(DayData day)
    {
        fishCaught++;
        fishTracker.text = "Fish Caught: " + fishCaught.ToString() + "/" + day.fishingMinimum;
        
    }

    public void checkTrackerProgress(DayData day)
    {
        if (fishCaught >= day.fishingMinimum)
        {
            hasFished = true;
            DialogueManager.instance.setDialogue("I should head back");
        }
    }

}
