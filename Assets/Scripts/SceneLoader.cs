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
        yield return new WaitForSeconds(0.6666f);
        SceneManager.LoadSceneAsync(buildIndex);

    }


}
