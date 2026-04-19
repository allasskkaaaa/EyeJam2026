using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alexa_PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private Camera cam;
    [SerializeField] private float playerSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handleMovement();
    }

    void handleMovement()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        // Remove vertical influence
        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * verInput + camRight * horInput;
        moveDir.y = 0f;
        cc.Move(moveDir * playerSpeed * Time.deltaTime);
    }
}
