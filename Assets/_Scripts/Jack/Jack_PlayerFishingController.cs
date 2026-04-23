using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_PlayerFishingController : MonoBehaviour
{
    public Jack_Bobber bobber;
    public Jack_FishingMinigame fishingMinigame;
    public Jack_FishingManager fishingManager;
    public Transform originPoint;
    public Transform endPoint;
    public Transform camTransform;
    public int guaranteeMemory = 5;
    public int catchesSinceMemory = 0;

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
    private bool bobberOut = false;
    private bool fishHooked = false;
    private bool fishHookable;

    float elapsed;
    float timeUntilFish;
    [SerializeField] float minRandomTime;
    [SerializeField] float maxRandomTime;
    [SerializeField] float maxTimeToHookFish;


    private void Update()
    {
        if (bobberOut && !fishHooked)
        {
            elapsed += Time.deltaTime;
            if (elapsed > timeUntilFish)
            {
                bobber.doBobbing = true;
                fishHookable = true;
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
                
                charge = Mathf.PingPong(bobberTimer, 1f);
                Vector3 newPos = Vector3.Lerp(originPoint.position, camTransform.forward * maxDist, charge);
                endPoint.position = new Vector3(newPos.x, 0f, newPos.z);
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            bobberTimer = 0f;
            if (bobberOut && !fishHooked)
            {
                RetrieveBobber();
            }
            else if (!bobberOut)
            {
                ThrowBobber();
            }
        }
    }

    void ThrowBobber()
    {
        // Initialize and throw bobber
        bobber.InitializeBobber(originPoint.position, endPoint.position, bobberThrowSpeed, maxHeight);
        bobber.InitializeAnimation(trajectoryAnimCurve);
        bobber.gameObject.SetActive(true);
        bobberOut = true;
        RandomiseFishTime();
    }

    void RetrieveBobber()
    {
        // Disable and "retrieve" bobber, but only without hooked fish
        bobber.gameObject.SetActive(false);
        bobberOut = false;
    }

    void HookFish()
    {
        Fishable fishable;
        fishHookable = false;
        fishHooked = true;
        if (catchesSinceMemory >= guaranteeMemory) fishable = fishingManager.GetRandomFishOfType(Fishable.FishableType.Memory);
        else fishable = fishingManager.GetRandomFish();

        if (fishable.fishableType == Fishable.FishableType.Memory) catchesSinceMemory = 0;
        fishingMinigame.InitializeFishingMinigame(this, fishable, reelSpeed, hookStrength);
    }

    public void CatchFish()
    {
        fishHooked = false;
        catchesSinceMemory++;
        elapsed = 0f;
        bobber.doBobbing = false;
        RetrieveBobber();
        // Do catch
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
