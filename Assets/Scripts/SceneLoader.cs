using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    public void ChangeSceneTo(int buildIndex)
    {
        StartCoroutine(LoadScene(buildIndex));

    }

    IEnumerator LoadScene(int buildIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(0.6666f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);


        while (!operation.isDone && Time.timeScale != 1f)
        {
            if (operation.progress >= 0.9f)
            {
                Time.timeScale = 1f; // Reset Time scale
                break;
            }


            yield return null;
        }
    }
}
