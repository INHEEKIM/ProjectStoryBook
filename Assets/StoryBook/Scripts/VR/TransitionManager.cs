/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private string viewTrigger;

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
        //viewTriggers = GameObject.Find("GazeRay").GetComponent<GazeRay>().viewTriggers;
        //viewTrigger = GameObject.Find("GazeRay").GetComponent<GazeRay>().viewTrigger;

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


        //�����尡 ȥ�����Ǹ�尡 �ƴϰų� ����������°� AR�� ������ ���°� �ƴ� ��
        //VR ��忡 ���� ó���κ�
        if ((mCurrentMode != mixedRealityMode) || (InAR != isVideoCurrentlyEnabled))
        {
            // mixed reality mode to switch to
            mCurrentMode = mixedRealityMode;

            // When we transition to VR, we deactivate the Datasets 
            // before setting the mixed reality mode.
            // so to reduce CPU usage, as tracking is not needed in this phase
            // (with AutoStopCameraIfNotRequired ON by default, camera/tracker
            //  will be turned off for performance optimization).

            //AR -> VR�� ��ȯ�� �� (ī�޶� �� �����ͼ� ��Ȱ��ȭ)

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

            //VR ->  AR�� ��ȯ�� ��
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

    public void SetViewTriggerName(string newViewTriggerName)
    {
        viewTrigger = newViewTriggerName;
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
            //���ǹ� ���� isFullScreenMode�� ���̸� Mode.Handheld_AR�� ��ȯ
            //�����̸� Mode.Viewer_AR�� ��ȯ
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
        //����ص� VR ������Ʈ�� ���⼭ visible�� ������
        //���� ����Ī ��ɹۿ� ����.

        // ����Ī �Ǵ� ��ư�� �̸� �ĺ��� ��
        //Debug.Log(viewTrigger); 

        //

        //
        foreach (var go in VROnlyObjects)
        {
            //AR�� �ƴ� ��쿡 ���� true���� ������ �׷��� ��� ������Ʈ ���λ����̱⿡ 
            //go.SetActive(!InAR);
            if (!InAR)
            {
                //Ȩ��ư �浹���°� Ʈ���̸�
                if (viewTrigger == "VRHomePotal")
                {
                    mTransitionManager.VROnlyObjects[0].SetActive(true); //Home
                    mTransitionManager.VROnlyObjects[1].SetActive(false); //Sky
                }
                //��ī�̹�ư �浹���°� Ʈ���̸�
                if (viewTrigger == "VRSkyPotal")
                {
                    mTransitionManager.VROnlyObjects[0].SetActive(false); //Home
                    mTransitionManager.VROnlyObjects[1].SetActive(true); //Sky
                }
                if (viewTrigger == "QuitPotal")
                {
                    SceneManager.LoadScene("TitleScene");
                }
            }
            else
            {
               
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
