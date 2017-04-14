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
        private GameObject chaKnight;
        private GameObject cuboidMarker;

        private bool stoneTrigger = false;
        private bool markerTrigger = false;

        private bool moveTrigger = false;
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();
            subUI = GameObject.Find("SubUI").GetComponent<Canvas>();
            subUI.enabled = false;

            chaKnight = GameObject.Find("Cha_Knight");
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

                    //로드.
                    GameManager.manager.Load();
                    //이미 페이지 진행을 모두 했을 때.
                    if (GameManager.manager.phase[0])
                    {
                        //다음페이지를 가라는 가이드와 다시하기 버튼 띄움.

                    }
                    //페이지 진행을 처음 할 때. or 다시할 때.
                    else
                    {
                        //knight가 3번 이상 공격했을 때
                        if (knight_Anim.anim.getAttack() > 2)
                        {
                            //다음페이지로 가라는 가이드 띄움.

                            //저장.
                            GameManager.manager.phase[0] = true;
                            GameManager.manager.Save();
                        }
                    }

                }
                //chipMarker
                if (mTrackableBehaviour.TrackableName == "chip") //Page2
                {
                    mMarkerStateManager.setBookMarkerPageNumber(MarkerStateManager.PageType.Page2);

                }
                if (mTrackableBehaviour.TrackableName == "Cuboid")
                {
                    mMarkerStateManager.setCuboidMarker(MarkerStateManager.StateType.On);
                }
                //스톤마커와 큐보이드 마커를 발견하면
                if(mMarkerStateManager.getCuboidMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getStoneMarker() == MarkerStateManager.StateType.On)
                {
                    moveTrigger = true;
                    Debug.Log("OK : " + moveTrigger);
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
                    subUI.enabled = false;
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

                //만약 큐보이드 마커의 발견 상태와 스톤마커의 발견 상태가 오프 상태이면
                if (mMarkerStateManager.getCuboidMarker() == MarkerStateManager.StateType.Off &&
            mMarkerStateManager.getStoneMarker() == MarkerStateManager.StateType.Off)
                {
                    moveTrigger = false;
                    Debug.Log("Off : " + moveTrigger);
                }

            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS

        void Update()
        {
            if (moveTrigger == true)
            {
                CharaterMove();
            }
            
        }

        void CharaterMove()
        {
            Vector3 direction = chaKnight.transform.position - cuboidMarker.transform.position; //로컬 포지션으로 캐릭터와 마커의 사이의 벡터를 구한후
            Quaternion rotation = Quaternion.LookRotation(-direction); //캐릭터의 foward가 반대로 되어있음
            chaKnight.transform.rotation = (Quaternion.Slerp(chaKnight.transform.rotation, rotation, Time.deltaTime * 1.5f));

            chaKnight.transform.position = Vector3.Lerp(chaKnight.transform.position, cuboidMarker.transform.position, Time.deltaTime * 0.8f);
        }

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
