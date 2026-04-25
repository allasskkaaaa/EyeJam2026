using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TMP_Text dialogueUI;
    [SerializeField] private float dialoguePlayLength = 3f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void setDialogue(string dialogue)
    {
        StartCoroutine(playDialogue(dialogue));
    }

    IEnumerator playDialogue(string dialogue)
    {
        dialogueUI.text = dialogue;
        dialogueUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(dialoguePlayLength);

        dialogueUI.gameObject.SetActive(false);
    }
}
