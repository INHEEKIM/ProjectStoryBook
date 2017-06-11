using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class FullScreenButton : MonoBehaviour {

    GameObject StereoCanvasImage;

    public void Start()
    {
        StereoCanvasImage = GameObject.Find("StereoCanvasImage");
    }

    public void setFullScreenMode()
    {
        if (ModeConfig.isFullScreenMode == true)
        {
            ModeConfig.isFullScreenMode = false;
            StereoCanvasImage.SetActive(true);

        }
        else
        {
            ModeConfig.isFullScreenMode = true;
            StereoCanvasImage.SetActive(false);
        }
        
    }
}
