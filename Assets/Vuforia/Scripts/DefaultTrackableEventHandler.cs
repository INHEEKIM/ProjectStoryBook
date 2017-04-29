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
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
        private MarkerStateManager mMarkerStateManager;
        private Canvas subUI;
        private GameObject cuboidMarker;

        private bool stoneTrigger = false;
        private bool markerTrigger = false;

        private bool moveTrigger = false;

        public GameObject LRRH_stoneMaker;
        public GameObject LRRH_cuboid;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();
            subUI = GameObject.Find("SubUI").GetComponent<Canvas>();
            subUI.enabled = false;

            cuboidMarker = GameObject.Find("CuboidMarker");

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

                //stoneMarker를 발견하게 되면
                if (mTrackableBehaviour.TrackableName == "stones") //Page1
                {
                    mMarkerStateManager.setStoneMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page1);
                    subUI.enabled = true;


                    //스톤마커와 큐보이드 마커를 발견하면
                    if (mMarkerStateManager.getStoneMarker() == MarkerStateManager.StateType.On &&
                        mMarkerStateManager.getCuboidMarker() == MarkerStateManager.StateType.On)
                    {
                        LRRH_stoneMaker.SetActive(true);
                        LRRH_cuboid.SetActive(false);
                        GameManager.manager.setPhase(1, true);
                    }

                }
                //chipMarker
                if (mTrackableBehaviour.TrackableName == "chip") //Page2
                {
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page2);

                }

                //큐브
                if (mTrackableBehaviour.TrackableName == "Cuboid")
                {
                    mMarkerStateManager.setCuboidMarker(MarkerStateManager.StateType.On);

                    //스톤마커와 큐보이드 마커를 발견하면
                    if (mMarkerStateManager.getStoneMarker() == MarkerStateManager.StateType.On &&
                        mMarkerStateManager.getCuboidMarker() == MarkerStateManager.StateType.On)
                    {
                        LRRH_stoneMaker.SetActive(true);
                        LRRH_cuboid.SetActive(false);
                        GameManager.manager.setPhase(1, true);
                    }
                }


                
            }
            else
            {
                OnTrackingLost();

                //stoneMarker를 읽으면
                if (mTrackableBehaviour.TrackableName == "stones")
                {
                    mMarkerStateManager.setStoneMarker(MarkerStateManager.StateType.Off);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Nothing);

                    //SubUI를 보여준다.
                    //subUI.enabled = false;
                }

                //chipMarker를 잃으면
                if (mTrackableBehaviour.TrackableName == "chip")
                {
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Nothing); 
                }

                if (mTrackableBehaviour.TrackableName == "Cuboid")
                {
                    mMarkerStateManager.setCuboidMarker(MarkerStateManager.StateType.Off);
                }

            //    //만약 큐보이드 마커의 발견 상태와 스톤마커의 발견 상태가 오프 상태이면
            //    if (mMarkerStateManager.getCuboidMarker() == MarkerStateManager.StateType.Off &&
            //mMarkerStateManager.getStoneMarker() == MarkerStateManager.StateType.Off)
            //    {
            //        moveTrigger = false;
            //        Debug.Log("Off : " + moveTrigger);
            //    }

            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS

        //void Update()
        //{
        //    if (moveTrigger == true)
        //    {
        //        CharaterMove();
        //    }
            
        //}

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
