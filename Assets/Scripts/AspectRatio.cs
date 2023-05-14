using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // TODO: Implement this by my own, so i can learn the logic behind it
        float targetAspect = 9f / 16f;
        float currentAspect = (float)Screen.width / Screen.height;
        float scaleHeight = currentAspect / targetAspect;

        // If the current aspect ratio is wider than the target aspect ratio, 
        // scale the camera's viewport horizontally to keep the aspect ratio.
        if (scaleHeight < 1f)
        {
            Camera.main.rect = new Rect(0f, (1f - scaleHeight) / 2f, 1f, scaleHeight);
        }
        // If the current aspect ratio is taller than the target aspect ratio, 
        // scale the camera's viewport vertically to keep the aspect ratio.
        else
        {
            float scaleWidth = 1f / scaleHeight;
            Camera.main.rect = new Rect((1f - scaleWidth) / 2f, 0f, scaleWidth, 1f);
        }
    }

}
