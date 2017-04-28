/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GazeRay : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES
    //시각 마우스에 반응하는 트리거 버튼 등록
    public ViewTrigger[] viewTriggers;
    private TransitionManager mTransitionManager;
    #endregion // PUBLIC_MEMBER_VARIABLES

    void Start()
    {
        mTransitionManager = FindObjectOfType<TransitionManager>();
    }

    #region MONOBEHAVIOUR_METHODS
    void Update()
    {
        // Check if the Head gaze direction is intersecting any of the ViewTriggers
        RaycastHit hit;
        Ray cameraGaze = new Ray(this.transform.position, this.transform.forward);
        Physics.Raycast(cameraGaze, out hit, Mathf.Infinity);

        foreach (var trigger in viewTriggers)
        {
            trigger.Focused = hit.collider && (hit.collider.gameObject == trigger.gameObject);
            if (trigger.Focused)
            {
                
                mTransitionManager.SetViewTriggerName(trigger.gameObject.name);
                //trigger.mFocuseState = true;
                //Debug.Log(viewTrigger);
                

            }
            
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS
}

