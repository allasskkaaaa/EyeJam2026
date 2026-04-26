using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_BoatMovement : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;
    public float drag;
    public float brakeForce;
    [SerializeField] private Transform boatObj;

    public AudioClip[] splooshes;
    public AudioSource audioSource;

    private Vector2 moveDir;
    private float velocity;
    [SerializeField] private float velocityDeadzone;
    private bool doBrake;

    public float strokeSpeed;
    public float strokeStrength;
    private float strokeTimer;

    float threshold = 0.9f;
    float lastStrokeForce;
    bool direction;

    private void Update()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.y = Input.GetAxis("Vertical");
        doBrake = Input.GetKey(KeyCode.X);

        BoatRotation();
        BoatMovement();
    }

    void BoatRotation()
    {
        float rotateSpeed = moveDir.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotateSpeed);
    }

    void BoatMovement()
    {
        // Start the timer for strokes and increment by the stroke speed (Default is 6)
        strokeTimer += Time.deltaTime * strokeSpeed;

        // Use sine wave and only get the down-strokes
        float stroke = Mathf.Sin(strokeTimer);
        float strokeForce = Mathf.Max(0f, stroke);
        if(strokeForce > threshold && lastStrokeForce <= threshold)
        {
            if (Mathf.Abs(moveDir.y) > 0f) PlaySploosh();
        }

        lastStrokeForce = strokeForce;

        // Check if movement is happening period
        velocity += acceleration * strokeForce * moveDir.y * strokeStrength * Time.deltaTime;
        if (doBrake)
        {
            velocity -= velocity * brakeForce * Time.deltaTime;
        }
        // Reduce by constant velocity, including an ever-present reduction that helps with making it slow faster as velocity reaches < ~0.1f
        velocity -= velocity * drag * Time.deltaTime;
        velocity -= Mathf.Sign(velocity) * 0.2f * Time.deltaTime;
        // If ship is nearly completely still, stop all movement (also checks if you're trying to move forward, so as to prevent accidental stopping constantly)
        if (Mathf.Abs(velocity) < velocityDeadzone && moveDir.y == 0f)
        {
            velocity = 0f;
        }
        velocity = Mathf.Clamp(velocity, -maxSpeed, maxSpeed);
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    void PlaySploosh()
    {
        AudioClip sploosh = splooshes[Random.Range(0, splooshes.Length)];
        audioSource.panStereo = direction ? -0.5f : 0.5f;
        direction = !direction;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(sploosh);
    }
    private void OnCollisionStay(Collision collision)
    {
        velocity -= Mathf.Sign(velocity) * 0.8f * Time.deltaTime;
    }
}