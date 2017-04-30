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
                //���渶Ŀ�� ť���̵� ��Ŀ�� �߰��ϸ� ť���̵�� CuboidTrackable ��ũ��Ʈ���� On�� ����
               

                OnTrackingFound();

                //stoneMarker�� �߰��ϰ� �Ǹ�
                if (mTrackableBehaviour.TrackableName == "stones") //Page1
                {
                    mMarkerStateManager.setStoneMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page1);
                    subUI.enabled = true;
                    //Debug.Log(mMarkerStateManager.getStoneMarker() + "" + mMarkerStateManager.getCuboidMarker());
                }




                //chipMarker
                if (mTrackableBehaviour.TrackableName == "chips") //Page2
                {
                    mMarkerStateManager.setChipMarker(MarkerStateManager.StateType.On);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page2);
                    Debug.Log(mMarkerStateManager.getChipMarker() + "" + mMarkerStateManager.getCuboidMarker());

                }



            }
            else
            {
                OnTrackingLost();

                //stoneMarker�� ������
                if (mTrackableBehaviour.TrackableName == "stones")
                {
                    mMarkerStateManager.setStoneMarker(MarkerStateManager.StateType.Off);
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Nothing);
                    Debug.Log(mMarkerStateManager.getStoneMarker());
                    //SubUI�� �����ش�.
                    //subUI.enabled = false;
                }

                //chipMarker�� ������
                if (mTrackableBehaviour.TrackableName == "chips")
                {
                    mMarkerStateManager.setChipMarker(MarkerStateManager.StateType.Off);
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
