/*==============================================================================
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
    public class CardVillagerTrackableEvantHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        private MarkerStateManager mMarkerStateManager;
        private GameObject villagerMarker;

        //
        public GameObject[] MaleCivilian;
        //마을 사람
        public GameObject People;
        //늑대
        public GameObject Wolf;
        //양
        public GameObject[] sheep;

        //첫번째 인식
        public bool[] flag;

        private bool moveTrigger = false;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

            villagerMarker = GameObject.Find("Card_Villager");

            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

            flag = new bool[8];
            for (int i = 0; i < flag.Length; i++)
                flag[i] = false;
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
                if (mTrackableBehaviour.TrackableName == "Card_Villager")
                {
                    mMarkerStateManager.setCharMarker(MarkerStateManager.StateType.On);
                    Debug.Log(mMarkerStateManager.getCharMarker());
                }

                //1Paga와 마커를 찾으면
                if (mMarkerStateManager.getOnePageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On
                    && flag[0] == false)
                {
                    MaleCivilian[0].SetActive(true);
                    flag[0] = true;
                }

                //2Paga와 마커, 마을사람
                if (mMarkerStateManager.getTwoPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getPersonsMarker() == MarkerStateManager.StateType.On
                    && flag[1] == false)
                {
                    MaleCivilian[1].SetActive(true);
                    flag[1] = true;
                }

                //3Paga와 마커를 찾으면
                if (mMarkerStateManager.getThreePageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getPersonsMarker() == MarkerStateManager.StateType.On
                    && flag[2] == false)
                {
                    MaleCivilian[2].SetActive(true);
                    flag[2] = true;
                }

                //4Paga와 마커를 찾으면
                if (mMarkerStateManager.getFourPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getPersonsMarker() == MarkerStateManager.StateType.On
                    && flag[3] == false)
                {
                        MaleCivilian[3].SetActive(true);
                        People.SetActive(true);
                    flag[3] = true;

                }

                //5Paga와 마커를 찾으면
                if (mMarkerStateManager.getFivePageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getPersonsMarker() == MarkerStateManager.StateType.On
                    && flag[4] == false)
                {
                    MaleCivilian[4].SetActive(true);
                    flag[4] = true;
                }

                //6Paga와 마커를 찾으면
                if (mMarkerStateManager.getSixPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getSheepMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On
                    && flag[5] == false)
                {
                    MaleCivilian[5].SetActive(true);
                    Wolf.SetActive(true);
                    sheep[0].SetActive(true);
                    sheep[1].SetActive(true);
                    sheep[2].SetActive(true);
                    flag[5] = true;
                }

                //7Paga와 마커를 찾으면
                if (mMarkerStateManager.getSevenPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On
                    && flag[6] == false)
                {
                    MaleCivilian[6].SetActive(true);
                    flag[6] = true;
                }

                //8Page와 마커를 찾으면
                if (mMarkerStateManager.getEightPageMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getSheepMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On
                    && flag[7] == false)
                {
                    MaleCivilian[7].SetActive(true);
                    sheep[3].SetActive(true);
                    sheep[4].SetActive(true);
                    flag[7] = true;
                }

            }
            else
            {
                OnTrackingLost();

                //잃음
                if (mTrackableBehaviour.TrackableName == "Card_Villager")
                {
                    mMarkerStateManager.setCharMarker(MarkerStateManager.StateType.Off);
                    Debug.Log("Villager Lost");
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
