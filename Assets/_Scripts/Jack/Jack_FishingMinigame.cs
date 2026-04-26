using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Jack_FishingMinigame : MonoBehaviour
{
    public UnityEvent<Fishable> OnCatchFish;

    Jack_PlayerFishingController fishingController;
    [SerializeField] float minSpeed = -2f, maxSpeed = 2f;
    private float bobberAcceleration;
    float bobberSpeed;
    [SerializeField] float drag = 0.5f;
    public Slider hookSlider, fishSlider, catchSlider;
    public GameObject fishingMinigameCanvas;
    [SerializeField] float hookWidth;
    float hookStrength;

    Fishable fish;

    private float elapsed;
    float nextFishMove;
    [SerializeField] private float minMoveTime, maxMoveTime;
    [SerializeField] private float minMoveSpeed, maxMoveSpeed;

    float catchAmt;
    float fishMove;

    public bool doMinigame;

    private void Awake()
    {
        fishingMinigameCanvas.gameObject.SetActive(false);
        doMinigame = false;
    }

    private void Update()
    {
        if (doMinigame)
        {
            HookMinigame();
            FishMovement();
        }
    }

    void FishMovement()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= nextFishMove)
        {
            fishMove = Random.Range(0f, 1f);
            nextFishMove = Random.Range(minMoveTime, maxMoveTime);
            elapsed = 0f;
        }

        fishSlider.value = Mathf.Lerp(fishSlider.value, fishMove, 2f * Time.deltaTime);

    }

    void HookMinigame()
    {
        catchSlider.value = catchAmt;
        if (catchAmt >= 100f)
        {
            Debug.Log("Wow you done caughted the fish");
            OnCatchFish?.Invoke(fish);
            doMinigame = false;
            // Caught fish
        }

        if (Mathf.Abs(hookSlider.value - fishSlider.value) < hookWidth)
        {
            // Catching fish
            catchAmt += hookStrength * Time.deltaTime;
        }
        else
        {
            // Losing fish
            catchAmt -= hookStrength * Time.deltaTime / 5f;
        }
                
        bobberSpeed -= bobberSpeed * drag * Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            bobberSpeed -= bobberAcceleration * Time.deltaTime;
        }
        if (Input.GetMouseButton(1))
        {
            bobberSpeed += bobberAcceleration * Time.deltaTime;
        }
        bobberSpeed = Mathf.Clamp(bobberSpeed, minSpeed, maxSpeed);
        hookSlider.value = Mathf.Clamp(hookSlider.value, 0f, 1f);
        hookSlider.value += bobberSpeed * Time.deltaTime;
    }

    public void FinishFishingGame()
    {
        fishingMinigameCanvas.gameObject.SetActive(false);
    }

    public void InitializeFishingMinigame(Jack_PlayerFishingController _fishingController, Fishable _fish, float _acceleration, float _hookStrength)
    {
        catchAmt = 0f;
        nextFishMove = 0f;
        fishingController = _fishingController;
        hookStrength = _hookStrength;
        fish = _fish;
        hookSlider.value = 0f;
        fishSlider.value = 0f;
        fishingMinigameCanvas.gameObject.SetActive(true);
        bobberAcceleration = _acceleration;
        doMinigame = true;
    }
}
