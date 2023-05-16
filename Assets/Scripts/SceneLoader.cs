using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    public void LoadSceneAsync(int buildIndex)
    {
        transition.SetTrigger("Start");

        SceneManager.LoadSceneAsync(buildIndex);
    }

}
