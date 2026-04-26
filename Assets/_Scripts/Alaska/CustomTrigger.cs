using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    [SerializeField] private string promptText;
    public bool isPrompt = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Boat") && isPrompt)
            DialogueManager.instance.setDialogue(promptText);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Boat") && isPrompt)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                onTriggerEnter.Invoke();
            }
        }
        else if (other.CompareTag("Player") || other.CompareTag("Boat") && !isPrompt)
        {
            onTriggerEnter.Invoke();
        }
    }
}
