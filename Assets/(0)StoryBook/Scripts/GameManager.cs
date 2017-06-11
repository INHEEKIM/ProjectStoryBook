using UnityEngine;
using Vuforia;
using System.Collections;

public class GameManager : MonoBehaviour {


    public static GameManager manager;

    //ar->vr
    public GameObject[] ViewTriggerARObj;
    private ViewTrigger[] viewTriggerAR;
    //vr->ar
    public GameObject[] ViewTriggerVRObj;
    private ViewTrigger[] viewTriggerVR;

    private bool[] arTrigger;
    private bool[] vrTrigger;

    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);  //don't destroy!!! other Scene
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
        viewTriggerAR = new ViewTrigger[3];
        viewTriggerVR = new ViewTrigger[3];

        arTrigger = new bool[3];
        vrTrigger = new bool[3];

        for (int i = 0; i < ViewTriggerARObj.Length; i++)
            viewTriggerAR[i] = ViewTriggerARObj[i].GetComponent<ViewTrigger>();
        for (int i = 0; i < ViewTriggerVRObj.Length; i++)
            viewTriggerVR[i] = ViewTriggerVRObj[i].GetComponent<ViewTrigger>();
    }

    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (viewTriggerAR[i].getMTriggered())
            {
                arTrigger[i] = true;
                VuforiaARController.Instance.SetWorldCenterMode(Vuforia.VuforiaARController.WorldCenterMode.DEVICE_TRACKING);
                Debug.Log(VuforiaARController.Instance.WorldCenterModeSetting);
                viewTriggerAR[i].setMTriggered(false);
            }
            if (viewTriggerVR[i].getMTriggered())
            {
                vrTrigger[i] = true;
                VuforiaARController.Instance.SetWorldCenterMode(Vuforia.VuforiaARController.WorldCenterMode.FIRST_TARGET);
                //Debug.Log(VuforiaARController.Instance.WorldCenter);
                Debug.Log(VuforiaARController.Instance.WorldCenterModeSetting);
                viewTriggerVR[i].setMTriggered(false);

            }
        }
    }

    public bool getARTrigger(int i)
    {
        return arTrigger[i];
    }
    public bool getVRTrigger(int i)
    {
        return vrTrigger[i];
    }
    public void resetTrigger()
    {
        for (int i = 0; i < arTrigger.Length; i++)
        {
            arTrigger[i] = false;
            vrTrigger[i] = false;
        }
    }

}

