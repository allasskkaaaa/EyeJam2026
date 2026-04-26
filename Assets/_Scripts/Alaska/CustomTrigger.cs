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
        if (other.CompareTag("Player") && isPrompt)
            DialogueManager.instance.setDialogue(promptText);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isPrompt)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                onTriggerEnter.Invoke();
            }
        }
        else
        {
            onTriggerEnter.Invoke();
        }


    }
}
