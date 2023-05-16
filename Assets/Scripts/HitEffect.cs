using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffect : MonoBehaviour
{
    Coroutine currentRunningRoutine = null;
    Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void StartEffect()
    {
        if (currentRunningRoutine != null)
        {
            StopCoroutine(currentRunningRoutine);
        }

        float alpha = 0.75f;
        float duration = 0.4f;
        currentRunningRoutine = StartCoroutine(FlashEffect(duration, alpha));
    }


    IEnumerator FlashEffect(float time, float alpha)
    {
        float halfTime = time / 2;


        Color currentColor;

        // Fade in
        for (float i = 0; i <= halfTime; i += Time.deltaTime)
        {
            currentColor = _image.color;
            currentColor.a = Mathf.Lerp(0, alpha, i / halfTime);
            _image.color = currentColor;
            yield return null;
        }

        // Fade out
        for (float i = 0; i <= halfTime; i += Time.deltaTime)
        {
            currentColor = _image.color;
            currentColor.a = Mathf.Lerp(alpha, 0, i / halfTime);
            _image.color = currentColor;
            yield return null;
        }


        // Ensure that alpha is 0 after animation ended
        currentColor = _image.color;
        currentColor.a = 0;
        _image.color = currentColor;

    }
}
