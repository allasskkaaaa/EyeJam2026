using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    [SerializeField] private int SceneChangeTo;

    IEnumerator LoadLevel(int level)
    {
        if (transition != null)
            transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneChangeTo);
    }

    public void triggerDoor()
    {
        //StartCoroutine(LoadLevel(SceneChangeTo));
        Debug.Log("Changing scene to " +  SceneChangeTo);
        SceneManager.LoadScene(SceneChangeTo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; //Check if the colliding object is the Player. If not, return

        triggerDoor();
    }
}
