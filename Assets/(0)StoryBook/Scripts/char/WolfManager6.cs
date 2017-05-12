using UnityEngine;
using System.Collections;

public class WolfManager6 : MonoBehaviour {

    //목적지
    public GameObject[] destination;
    //양
    public GameObject sheep;
    //동작 순서 체크
    private bool[] desFlag;

    //딜레이 횟수 체크
    private int delay = 0;


    //속도
    private float walkSpeed = 20.0f;
    private float runSpeed = 45.0f;
    private float minDistance = 0.1f;


    private MarkerStateManager mMarkerStateManager;
    private Animator anim;

    void Awake()
    {
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();
        anim = GetComponent<Animator>();
        desFlag = new bool[10];
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }


    void Update()
    {
        if (mMarkerStateManager.getOnePageMarker() == MarkerStateManager.StateType.On)
            Move();
    }


    void Move()
    {
        if (desFlag[0])
        {
            desFlag[0] = !desFlag[0];
            StartCoroutine("move0");
        }
        //0번째 목적지로
        else if (desFlag[1])
        {
            if (Vector3.Distance(transform.position, destination[0].transform.position) > minDistance)
            {
                anim.SetBool("Walk", true);
                Vector3 vDirection = destination[0].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * walkSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[0].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);

            }
        }

        //양을 쫓아감.
        else if (desFlag[2])
        {
            if (Vector3.Distance(transform.position, sheep.transform.position) > minDistance)
            {
                anim.SetBool("Run", true);
                Vector3 vDirection = sheep.transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(sheep.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);

            }
        }



    }


    //기다렸다가 0목적지로.
    IEnumerator move0()
    {
        yield return new WaitForSeconds(6.0f);
        desFlag[1] = true;
    }

    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            gameObject.SetActive(false);
        }
        //중간 목적지 충돌.
        if (coll.tag == "destination")
        {
            //0목적지. 
            if (desFlag[1] == true)
            {
                anim.SetBool("Walk", false);
                desFlag[1] = false;
                desFlag[2] = true;
                destination[0].SetActive(false);
            }
        }


    }




}
