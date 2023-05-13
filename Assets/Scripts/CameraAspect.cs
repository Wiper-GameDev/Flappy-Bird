using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    private float targetAspectRatio = 9.0f / 16.0f;

    void Awake() {
       QualitySettings.vSyncCount = 0;
       Application.targetFrameRate =  60; 
    }

    void Start()
    {
        float screenAspectRatio = (float)Screen.width / (float)Screen.height;
        float scaleHeight = screenAspectRatio / targetAspectRatio;
        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            camera.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1, scaleHeight);
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            camera.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1);
        }
    }
}
