using UnityEngine;
using System.Collections;

public class LRRH_nav : MonoBehaviour {

    public GameObject LRRH_destination; //목적지

    private NavMeshAgent nav;

    private MarkerStateManager mMarkerStateManager;


    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

    }

    void Update()
    {
        if(mMarkerStateManager.getStoneMarker() == MarkerStateManager.StateType.On)
            nav.SetDestination(LRRH_destination.transform.position);
    }


    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //목적지 도달.
        if (coll.tag == "EndCube1")
        {
            Debug.Log("도착");
            gameObject.SetActive(false);
            
        }

    }


}
