using UnityEngine;
using System.Collections;

public class LRRH_nav : MonoBehaviour {

    public GameObject LRRH_destination; //목적지

    private NavMeshAgent nav;

    void Awaek()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.SetDestination(LRRH_destination.transform.position);
    }

}
