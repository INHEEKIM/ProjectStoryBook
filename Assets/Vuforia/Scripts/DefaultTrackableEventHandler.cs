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
        private GameObject cuboidMarker;





        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

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
                //스톤마커와 큐보이드 마커를 발견하면 큐보이드는 CuboidTrackable 스크립트에서 On값 받음
               

                OnTrackingFound();

                //stoneMarker를 발견하게 되면
                if (mTrackableBehaviour.TrackableName == "stones") //Page1
                {
                    
                    mMarkerStateManager.setOnePageMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page1);
                    //Debug.Log(mMarkerStateManager.getOnePageMarker() + "" + mMarkerStateManager.getCuboidMarker());
                }

                //chipMarker
                if (mTrackableBehaviour.TrackableName == "chips") //Page2
                {
                    mMarkerStateManager.setTwoPageMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page2);
                }

                //3
                if (mTrackableBehaviour.TrackableName == "tarmac") 
                {
                    mMarkerStateManager.setThreePageMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page3);
                }
                //4
                if (mTrackableBehaviour.TrackableName == "grass")
                {
                    mMarkerStateManager.setFourPageMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page4);
                }
                //5
                if (mTrackableBehaviour.TrackableName == "painted")
                {
                    mMarkerStateManager.setFivePageMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page5);
                }
                //6
                if (mTrackableBehaviour.TrackableName == "wolf")
                {
                    mMarkerStateManager.setSixPageMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page6);
                }
                //7
                if (mTrackableBehaviour.TrackableName == "flower")
                {
                    mMarkerStateManager.setSevenPageMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page7);
                }
                //8
                if (mTrackableBehaviour.TrackableName == "fruits")
                {
                    mMarkerStateManager.setEightPageMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page8);
                }



            }
            else
            {
                OnTrackingLost();

                //stoneMarker를 읽으면
                if (mTrackableBehaviour.TrackableName == "stones")
                {
                    mMarkerStateManager.setOnePageMarker(MarkerStateManager.StateType.Off);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Nothing);
                    Debug.Log(mMarkerStateManager.getOnePageMarker());
                }

                //chipMarker를 잃으면
                if (mTrackableBehaviour.TrackableName == "chips")
                {
                    mMarkerStateManager.setTwoPageMarker(MarkerStateManager.StateType.Off);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Nothing);
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
