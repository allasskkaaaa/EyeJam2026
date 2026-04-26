using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;


    IEnumerator LoadLevel(int level)
    {
        Debug.Log("Initializing scene change to " + level);
        Time.timeScale = 1f;
        if (transition != null)
            transition.Play("FadeToBlack");

        yield return new WaitForSeconds(transitionTime);

        Debug.Log("Changing scene to " + level);
        SceneManager.LoadScene(level);
    }

    public void changeScene(int newScene)
    {
        //StartCoroutine(LoadLevel(SceneChangeTo));
        SceneManager.LoadScene(newScene);
        //StartCoroutine(LoadLevel(newScene));
    }

    public void changeSceneTransition(int newScene)
    {
        //StartCoroutine(LoadLevel(SceneChangeTo));
        //SceneManager.LoadScene(newScene);
        StartCoroutine(LoadLevel(newScene));
    }
}
