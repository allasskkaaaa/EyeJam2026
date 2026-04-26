using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public UnityEvent OnChangeDay;
    public UnityEvent OnMemoriesSpawned;
    public int dayIndex = 0;
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
    [SerializeField] List<GameObject> dailyMemories;
    [SerializeField] GameObject boatOBJ;
    [SerializeField] Transform boatResetPoint;
    [SerializeField] TMP_Text objectiveText;

    [SerializeField] GameObject sleepTrigger;
    [SerializeField] private TMP_Text fishTracker;
    public int memoriesCaught;
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
        hasFished = false;
        anim.Play("FadeToBlack");
        yield return new WaitForSeconds(2);
        changeTerrain(dayIndex);
        changeBoat(dayIndex);
        changeMusic(dayIndex);
        OnChangeDay?.Invoke();
        day++;
        
        resetTracker(dayData[dayIndex]);
        resetBoat();
        objectiveText.text = "Time to fish";
        RenderSettings.fogEndDistance = dayData[dayIndex].fogDistanceEnd;
        cam.backgroundColor = dayData[dayIndex].skyColour;
        anim.Play("FadeFromBlack");
        checkTrackerProgress(dayData[dayIndex]);
        if (dayIndex < dailyMemories.Count)
        {
            dailyMemories[dayIndex].gameObject.SetActive(false);
        }

        if (day == 5)
        {
            day5SetUp();
        }

    }

    private void Awake()
    {
        instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        resetTracker(dayData[1]);

        
    }

    private void Start()
    {
        AudioManager.instance.changeSong(dailyMusic[dayIndex]);
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

    public void resetTracker(DayData day)
    {
        fishTracker.text = "Fish Caught: 0/" + day.fishingMinimum;
        fishCaught = 0;
        memoriesCaught = 0;

        if (dayIndex < dailyMemories.Count)
        {
            dailyMemories[dayIndex].gameObject.SetActive(false);
        }
    }

    public void increaseTracker(DayData day)
    {
        fishCaught++;
        fishTracker.text = "Fish Caught: " + fishCaught.ToString() + "/" + day.fishingMinimum;
        checkTrackerProgress(day);


    }

    public void checkTrackerProgress(DayData day)
    {

        if (fishCaught >= day.fishingMinimum && memoriesCaught >= day.memoriesToFind)
        {
            hasFished = true;
            DialogueManager.instance.setDialogue("I should head back");
            objectiveText.text = "I should head back";
        } 
        else if ((fishCaught >= day.fishingMinimum && memoriesCaught < day.memoriesToFind))
        {
            AudioManager.instance.playSFX(day.memoryAlert);
            DialogueManager.instance.setDialogue("I sense something else here");
            objectiveText.text = "I sense something else here";
            if (dayIndex > dailyMemories.Count)
            {
                //If there is no more data for the day, return
                return;
            }
            else
            {
                dailyMemories[dayIndex].gameObject.SetActive(true);
            }
            
            OnMemoriesSpawned?.Invoke();
        }
    }

    private void resetBoat()
    {
        boatOBJ.transform.position = boatResetPoint.position;
        boatOBJ.transform.rotation = boatResetPoint.rotation;
        
    }

    private void day5SetUp()
    {
        objectiveText.text = "It's time.";
        fishTracker.gameObject.SetActive(false);
    }

}
