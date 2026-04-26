using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera cam;
    [SerializeField] private float playerSpeed = 5;

    float horInput;
    float verInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handleMovement();
    }

    void HandleInput()
    {
        horInput = Input.GetAxis("Horizontal");
        verInput = Input.GetAxis("Vertical");
    }

    void handleMovement()
    {

        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        // Remove vertical influence
        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * verInput + camRight * horInput;
        Vector3 velocity = moveDir * playerSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }
}
