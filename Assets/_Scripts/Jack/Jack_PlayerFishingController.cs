using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_PlayerFishingController : MonoBehaviour
{
    public Jack_Bobber bobber;
    public Jack_FishingMinigame fishingMinigame;
    public Jack_FishingManager fishingManager;
    public Inspect inspect;
    public GameObject bobHologram;
    public Transform originPoint;
    public Transform endPoint;
    public Transform camTransform;
    public Transform water;

    [Header("Fishing stats")]
    public float chargeSpeed;
    public float reelSpeed;
    public float maxHeight;
    public float hookStrength;
    [SerializeField] private float bobberThrowSpeed;

    public AnimationCurve trajectoryAnimCurve;

    [SerializeField] private float maxDist;

    [SerializeField] private float charge;
    [SerializeField] float bobberTimer;

    [SerializeField] private LineRenderer lr;
    [SerializeField] private List<Jack_FishingSpot> fishingSpots = new();
    private bool bobberOut = false;
    private bool fishHooked = false;
    private bool fishHookable;
    private bool onWater;

    float elapsed;
    float timeUntilFish;
    float trueZero;
    [SerializeField] float minRandomTime;
    [SerializeField] float maxRandomTime;
    [SerializeField] float maxTimeToHookFish;

    Jack_FishingSpot cachedFishingSpot;
    Fishable fishable;

    
    [SerializeField] Bucket bucket;

    [Header("Sound Effects")]
    private bool nibbleSoundPlayed = false;
    [SerializeField] AudioClip fishNibbling;
    [SerializeField] AudioClip fishCaught;
    [SerializeField] AudioClip goodMemoryCaught;
    [SerializeField] AudioClip badMemoryCaught;

    private void Awake()
    {
        trueZero = water.position.y;
    }

    private void Start()
    {
        PopulateFishingSpots();
    }

    private void Update()
    {
        if (inspect.IsInspecting) return;

        if (bobberOut && !fishHooked)
        {
            elapsed += Time.deltaTime;
            if (elapsed > timeUntilFish)
            {
                bobber.doBobbing = true;
                fishHookable = true;

                if (!nibbleSoundPlayed)
                {
                    AudioManager.instance.playSFX(fishNibbling);
                    nibbleSoundPlayed = true;
                }
                
            }
            if(elapsed > timeUntilFish + maxTimeToHookFish)
            {
                fishHookable = false;
                bobber.doBobbing = false;
                RandomiseFishTime();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (fishHookable)
            {
                HookFish();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (!bobberOut)
            {
                bobberTimer += Time.deltaTime * chargeSpeed;
                bobHologram.SetActive(true);
                
                charge = Mathf.PingPong(bobberTimer, 1f);
                Vector3 targetPos = camTransform.position + camTransform.forward * maxDist;
                Vector3 currentPos = camTransform.position + camTransform.forward * 1.1f;
                Vector3 newPos = Vector3.Lerp(currentPos, targetPos, charge);
                endPoint.position = new Vector3(newPos.x, trueZero, newPos.z);
                bobHologram.transform.position = endPoint.position;

                Vector3 checkPos = new Vector3(endPoint.position.x, trueZero+10f, endPoint.position.z);

                if(Physics.Raycast(checkPos, Vector3.down*50f, out RaycastHit hit))
                {
                    if (hit.transform.gameObject.tag == "Water") onWater = true;
                    else onWater = false;
                }
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            bobHologram.SetActive(false);
            bobberTimer = 0f;
            if (bobberOut && !fishHooked)
            {
                RetrieveBobber();
            }
            else if (!bobberOut && onWater)
            {
                ThrowBobber();
            }
        }
    }

    void ThrowBobber()
    {
        // Initialize and throw bobber
        bobber.InitializeBobber(originPoint.position, endPoint.position, trueZero, bobberThrowSpeed, maxHeight);
        bobber.InitializeAnimation(trajectoryAnimCurve);
        bobber.gameObject.SetActive(true);
        bobberOut = true;
        RandomiseFishTime();
    }

    public void RetrieveBobber()
    {
        // Disable and "retrieve" bobber, but only without hooked fish
        bobber.gameObject.SetActive(false);
        bobberOut = false;
    }

    void HookFish()
    {
        nibbleSoundPlayed = false;
        fishHookable = false;
        fishHooked = true;
        fishable = fishingManager.GetRandomFish();
        foreach (Jack_FishingSpot f in fishingSpots)
        {
            if (f == null)
            {
                break;
            }

            if (Vector3.Distance(endPoint.position, f.transform.position) < f.radius)
            {
                fishable = f.fishable;
                cachedFishingSpot = f;
                Destroy(f.gameObject);
                //fishingSpots.Remove(f);
                break;
            }
        }
        fishingMinigame.InitializeFishingMinigame(this, fishable, reelSpeed, hookStrength);
    }

    public void CatchFish()
    {
        fishHooked = false;
        elapsed = 0f;
        bobber.doBobbing = false;
        RetrieveBobber();
        
        if (cachedFishingSpot) Destroy(cachedFishingSpot);

        bucket.itemsInBucket.Add(fishable.inspectionObject);
        if (fishable.fishableType == Fishable.FishableType.Fish) AudioManager.instance.playSFX(fishCaught);
        else if (fishable.fishableType == Fishable.FishableType.GoodMemory) AudioManager.instance.playSFX(goodMemoryCaught);
        else if (fishable.fishableType == Fishable.FishableType.BadMemory) AudioManager.instance.playSFX(badMemoryCaught);

        // Inspect Item
        inspect.openInspectMenu(fishable.inspectionObject);
        DayManager.instance.increaseTracker(DayManager.instance.dayData[DayManager.instance.dayIndex]);
        DayManager.instance.checkTrackerProgress(DayManager.instance.dayData[DayManager.instance.dayIndex]);
        // Do catch
        

    }

    public void PopulateFishingSpots()
    {
        fishingSpots.Clear();
        fishingSpots.AddRange(FindObjectsOfType<Jack_FishingSpot>());
    }

    public void SetFishingRodActive(bool setActive)
    {
        gameObject.SetActive(setActive);
    }

    void RandomiseFishTime()
    {
        elapsed = 0f;
        timeUntilFish = Random.Range(minRandomTime, maxRandomTime);
    }

    private void LateUpdate()
    {
        if (bobberOut)
        {
            lr.enabled = true;
            lr.SetPosition(0, originPoint.position);
            lr.SetPosition(1, bobber.transform.position);
        }
        else
        {
            lr.enabled = false;
        }
    }

}
