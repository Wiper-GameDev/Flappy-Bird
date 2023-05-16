using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public struct BirdSettings
{
    public Vector2 initialPosition;
    public Vector2 finalPosition;
    public Transform transform;
    public float animationTime;
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] BirdSettings birdSettings;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAnimating());
    }


    IEnumerator StartAnimating()
    {
        while (true)
        {
            birdSettings.transform.localPosition = birdSettings.initialPosition;

            float halfTime = birdSettings.animationTime / 2;
            for (float i = 0; i <= halfTime; i += Time.deltaTime)
            {
                birdSettings.transform.localPosition = Vector2.Lerp(birdSettings.initialPosition, birdSettings.finalPosition, i / halfTime);
                yield return null;
            }

            for (float i = 0; i <= halfTime; i += Time.deltaTime)
            {
                birdSettings.transform.localPosition = Vector2.Lerp(birdSettings.finalPosition, birdSettings.initialPosition, i / halfTime);
                yield return null;
            }

        }
    }


    public void LoadGameScene(){
        SceneManager.LoadScene(1);
    }
}
