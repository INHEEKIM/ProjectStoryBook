﻿/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class CardWolfTrackableEvantHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        private MarkerStateManager mMarkerStateManager;
        private GameObject WolfMarker;

        //
        public GameObject[] MaleCivilian;

        private bool moveTrigger = false;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

            WolfMarker = GameObject.Find("Card_Wolf");

            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();

                //양치기 소년 마커 찾음
                if (mTrackableBehaviour.TrackableName == "Card_Wolf")
                {
                    mMarkerStateManager.setWolfMarker(MarkerStateManager.StateType.On);
                    Debug.Log(mMarkerStateManager.getWolfMarker());
                }

                //1Paga와 마커를 찾으면
                if (mMarkerStateManager.getOnePageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On)
                {
                    //MaleCivilian[0].SetActive(true);
                }

                //2Paga와 마커를 찾으면
                if (mMarkerStateManager.getTwoPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On)
                {
                    //MaleCivilian[0].SetActive(true);
                }

                //3Paga와 마커를 찾으면
                if (mMarkerStateManager.getThreePageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On)
                {
                    //MaleCivilian[0].SetActive(true);
                }

                //4Paga와 마커를 찾으면
                if (mMarkerStateManager.getFourPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On)
                {
                    //MaleCivilian[0].SetActive(true);
                }

                //5Paga와 마커를 찾으면
                if (mMarkerStateManager.getFivePageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On)
                {
                    //MaleCivilian[0].SetActive(true);
                }

                //6Paga와 마커를 찾으면
                if (mMarkerStateManager.getSixPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On)
                {
                    //MaleCivilian[0].SetActive(true);
                }

                //7Paga와 마커를 찾으면
                if (mMarkerStateManager.getSevenPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On)
                {
                    //MaleCivilian[0].SetActive(true);
                }

                //8Page와 마커를 찾으면
                if (mMarkerStateManager.getEightPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On)
                {
                    //MaleCivilian[0].SetActive(true);
                }

            }
            else
            {
                OnTrackingLost();

                //잃음
                if (mTrackableBehaviour.TrackableName == "Card_Wolf")
                {
                    mMarkerStateManager.setWolfMarker(MarkerStateManager.StateType.Off);
                    Debug.Log("People Lost");
                }

            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS

        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
