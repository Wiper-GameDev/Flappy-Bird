using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    public void LoadSceneAsync(int buildIndex)
    {
        StartCoroutine(LoadScene(buildIndex));
    }


    IEnumerator LoadScene(int buildIndex)
    {
        transition.SetTrigger("Start");

        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
