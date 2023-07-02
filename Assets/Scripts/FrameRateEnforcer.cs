using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateEnforcer : MonoBehaviour
{
    void Start(){
        QualitySettings.vSyncCount = 1; // Enable VSync

        // Set the target frame rate to the maximum supported by the device
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
}
