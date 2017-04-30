using UnityEngine;
using System.Collections;

public class LRRH_nav : MonoBehaviour {

    public GameObject[] LRRH_destination; //목적지
    private bool[] des_flag;

    public float speed = 10.0f;
    public float minDistance = 0.01f;


    private MarkerStateManager mMarkerStateManager;

    void Awake()
    {
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();
        des_flag = new bool[LRRH_destination.Length];
        for (int i = 0; i < des_flag.Length; i++)
            des_flag[i] = false;

    }

    void Update()
    {
        if (mMarkerStateManager.getStoneMarker() == MarkerStateManager.StateType.On)
            Move();
    }


    private void Move()
    {
        if(des_flag[0] == false)
        {
            if (Vector3.Distance(transform.position, LRRH_destination[0].transform.position) > minDistance)
            {
                Vector3 vDirection = LRRH_destination[0].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * speed * Time.deltaTime;
                transform.position += vMoveVector;

                transform.LookAt(LRRH_destination[0].transform);
            }
            else
            {
                des_flag[0] = true;
            }
        }
        if (des_flag[0] == true)
        {
            if (Vector3.Distance(transform.position, LRRH_destination[1].transform.position) > minDistance)
            {
                Vector3 vDirection = LRRH_destination[1].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * speed * Time.deltaTime;
                transform.position += vMoveVector;

                transform.LookAt(LRRH_destination[1].transform);
            }
            else
                des_flag[1] = true;
        }
        if (des_flag[1] == true)
        {
            if (Vector3.Distance(transform.position, LRRH_destination[2].transform.position) > minDistance)
            {
                Vector3 vDirection = LRRH_destination[2].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * speed * Time.deltaTime;
                transform.position += vMoveVector;

                transform.LookAt(LRRH_destination[2].transform);
            }
            else
                des_flag[2] = true;
        }
        if (des_flag[2] == true)
        {
            if (Vector3.Distance(transform.position, LRRH_destination[3].transform.position) > minDistance)
            {
                Vector3 vDirection = LRRH_destination[3].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * speed * Time.deltaTime;
                transform.position += vMoveVector;

                transform.LookAt(LRRH_destination[3].transform);
            }
            else
                des_flag[3] = true;
        }
        if (des_flag[3] == true)
        {
            if (Vector3.Distance(transform.position, LRRH_destination[4].transform.position) > minDistance)
            {
                Vector3 vDirection = LRRH_destination[4].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * speed * Time.deltaTime;
                transform.position += vMoveVector;

                transform.LookAt(LRRH_destination[4].transform);
            }
            else
                des_flag[4] = true;
        }
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
