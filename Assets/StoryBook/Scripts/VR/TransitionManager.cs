/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Vuforia;

public class TransitionManager : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES
    private BlackMaskBehaviour mBlackMask;

    private float mTransitionCursor = 0;
    private bool mPlaying = false;
    private bool mBackward = false;
    private MixedRealityController.Mode mCurrentMode = MixedRealityController.Mode.HANDHELD_AR;
    private float mCurrentTime = 0; //HANDHELD_AR
    private ViewTrigger[] viewTrigger;

    private TransitionManager mTransitionManager; //
    #endregion // PRIVATE_MEMBER_VARIABLES


    #region PUBLIC_MEMBER_VARIABLES
    public GameObject[] VROnlyObjects;
    public GameObject[] AROnlyObjects;

    public bool mTriggered = false;

    [Range(0.1f, 5.0f)]
    public float transitionDuration = 1.5f; // seconds

    public bool InAR { get { return mTransitionCursor <= 0.66f; } }
    #endregion PUBLIC_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        viewTrigger = GameObject.Find("GazeRay").GetComponent<GazeRay>().viewTriggers;

        // At start we assume we are in AR
        mTransitionCursor = 0;

        mTransitionManager = FindObjectOfType<TransitionManager>(); //

        mBlackMask = FindObjectOfType<BlackMaskBehaviour>();
        SetBlackMaskVisible(false, 0);

        VideoBackgroundManager.Instance.SetVideoBackgroundEnabled(true);

        mCurrentMode = GetMixedRealityMode();
        MixedRealityController.Instance.SetMode(mCurrentMode);

        UpdateVisibleObjects();

        mCurrentTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        float time = Time.realtimeSinceStartup;
        float deltaTime = Mathf.Clamp01(time - mCurrentTime);
        mCurrentTime = time;

        // We need to check if the video background is curently enabled
        // because Vuforia may restart the video background when the App is resumed
        // even if the app was paused in VR mode
        bool isVideoCurrentlyEnabled = IsVideoBackgroundRenderingEnabled();

        MixedRealityController.Mode mixedRealityMode = GetMixedRealityMode();


        //현재모드가 혼합현실모드가 아니거나 현재비디오상태가 AR로 진입한 상태가 아닐 때
        //VR 모드에 대한 처리부분
        if ((mCurrentMode != mixedRealityMode) || (InAR != isVideoCurrentlyEnabled))
        {
            // mixed reality mode to switch to
            mCurrentMode = mixedRealityMode;

            // When we transition to VR, we deactivate the Datasets 
            // before setting the mixed reality mode.
            // so to reduce CPU usage, as tracking is not needed in this phase
            // (with AutoStopCameraIfNotRequired ON by default, camera/tracker
            //  will be turned off for performance optimization).

            //AR -> VR로 변환될 때 (카메라 및 데이터셋 비활성화)

            if (mCurrentMode == MixedRealityController.Mode.HANDHELD_VR
                || mCurrentMode == MixedRealityController.Mode.VIEWER_VR)
            {
                Debug.Log("Switching to VR: deactivating datasets");
                ActivateDatasets(false);
            }

            // As we are moving back to AR, we re-activate the Datasets,
            // before setting the mixed reality mode.
            // this will ensure that the Tracker and Camera are restarted, 
            // in case they were previously stopped when moving to VR
            // before activating the AR mode

            //VR ->  AR로 변환될 때
            if (mCurrentMode == MixedRealityController.Mode.HANDHELD_AR
                || mCurrentMode == MixedRealityController.Mode.VIEWER_AR)
            {
                Debug.Log("Switching to AR: activating datasets");
                ActivateDatasets(true);
            }

            MixedRealityController.Instance.SetMode(mCurrentMode);
            UpdateVisibleObjects();
        }

        if (mPlaying)
        {
            float fadeFactor = 0;
            if (mTransitionCursor < 0.33f)
            {
                // fade to full black in first part of transition
                fadeFactor = Mathf.SmoothStep(0, 1, mTransitionCursor / 0.33f);
            }
            else if (mTransitionCursor < 0.66f)
            {
                // between 33% and 66% we stay in full black
                fadeFactor = 1;
            }
            else // > 0.66
            {
                // between 66% and 100% we fade out
                fadeFactor = Mathf.SmoothStep(1, 0, (mTransitionCursor - 0.66f) / 0.33f);
            }
            SetBlackMaskVisible(true, fadeFactor);

            float delta = (mBackward ? -1 : 1) * deltaTime / transitionDuration;
            mTransitionCursor += delta;

            if (mTransitionCursor <= 0 || mTransitionCursor >= 1)
            {
                // Done: stop animated transition
                mTransitionCursor = Mathf.Clamp01(mTransitionCursor);
                mPlaying = false;
                SetBlackMaskVisible(false, 0);
            }
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    public void Play(bool reverse)
    {
        // dont' restart playing during a transition
        if (!mPlaying)
        {
            mPlaying = true;
            mBackward = reverse;
            mTransitionCursor = mBackward ? 1 : 0;
        }
    }
    #endregion // PUBLIC_METHODS

    public void SwitchingTrigger(bool newTrigger)
    {
        mTriggered = newTrigger;
    }


    #region PRIVATE_METHODS
    private void ActivateDatasets(bool enableDataset)
    {
        //Disable/Enable dataset
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        IEnumerable<DataSet> datasets = objectTracker.GetDataSets();

        foreach (DataSet dataset in datasets)
        {
            if (enableDataset)
                objectTracker.ActivateDataSet(dataset);
            else
                objectTracker.DeactivateDataSet(dataset);
        }
    }

    private MixedRealityController.Mode GetMixedRealityMode()
    {
        if (InAR)
        {
            //조건문 만약 isFullScreenMode가 참이면 Mode.Handheld_AR을 반환
            //거짓이면 Mode.Viewer_AR를 반환
            return ModeConfig.isFullScreenMode ?
                MixedRealityController.Mode.HANDHELD_AR : MixedRealityController.Mode.VIEWER_AR;
        }
        else // in VR
        {
            return ModeConfig.isFullScreenMode ?
                MixedRealityController.Mode.HANDHELD_VR : MixedRealityController.Mode.VIEWER_VR;
        }
    }


    private void UpdateVisibleObjects()
    {
        //등록해둔 VR 오브젝트는 여기서 visible이 결정됨
        //아직 스윗칭 기능밖에 없다.

        //


        //
        foreach (var go in VROnlyObjects)
        {
            //AR이 아닐 경우에 대해 true값을 가진다 그러나 계속 업데이트 중인상태이기에 
            //go.SetActive(!InAR);
            if (!InAR)
            {
                if (viewTrigger[0].mFocuseState == true && viewTrigger[0].worldType == ViewTrigger.WorldType.House)
                {
                    mTransitionManager.VROnlyObjects[0].SetActive(true); //Home
                    mTransitionManager.VROnlyObjects[1].SetActive(false); //Sky
                }
                if (viewTrigger[1].mFocuseState == true && viewTrigger[1].worldType == ViewTrigger.WorldType.Sky)
                {
                    mTransitionManager.VROnlyObjects[0].SetActive(false); //Home
                    mTransitionManager.VROnlyObjects[1].SetActive(true); //Sky
                }
            }
            else
            {
                viewTrigger[0].mFocuseState = false;
                viewTrigger[1].mFocuseState = false;
                go.SetActive(false);
            }           
        }

        foreach (var go in AROnlyObjects)
        {
            go.SetActive(InAR);

        }
    }

    private void SetBlackMaskVisible(bool visible, float fadeFactor)
    {
        if (mBlackMask)
        {
            if (mBlackMask.GetComponent<Renderer>().enabled != visible)
                mBlackMask.GetComponent<Renderer>().enabled = visible;

            mBlackMask.SetFadeFactor(fadeFactor);
        }
    }

    private bool IsVideoBackgroundRenderingEnabled()
    {
        var bgPlaneBehaviour = GetCameraRigRoot().GetComponentInChildren<BackgroundPlaneAbstractBehaviour>();
        return (bgPlaneBehaviour ? bgPlaneBehaviour.GetComponent<MeshRenderer>().enabled : false);
    }

    private Transform GetCameraRigRoot()
    {
        var eyewear = DigitalEyewearARController.Instance;
        var vuforia = VuforiaBehaviour.Instance;
        return (eyewear.CentralAnchorPoint ? eyewear.CentralAnchorPoint.transform.root : vuforia.transform);
    }
    #endregion PRIVATE_METHODS
}
