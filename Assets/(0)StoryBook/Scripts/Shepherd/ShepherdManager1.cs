using UnityEngine;
using System.Collections;

public class ShepherdManager1 : MonoBehaviour {

    //목적지
    public GameObject[] destination;
    //동작 순서 체크
    private bool[] desFlag;
    //애니메이션 1번 체크
    private bool animFlag = false;

    //시간 체크
    private float time = 0.0f;

    //속도
    private float walkSpeed = 15.0f;
    private float runSpeed = 20.0f;
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
    }


    void Update()
    {
        if (mMarkerStateManager.getStoneMarker() == MarkerStateManager.StateType.On)
            Move();
    }



    void Move()
    {
        ////인식 후 토크 애니메이션 1번
        if (!desFlag[0])
        {
            desFlag[0] = !desFlag[0];
            StartCoroutine("move0");
        }

        //위로 이동.
        if (desFlag[1])
        {
            if (Vector3.Distance(transform.position, destination[0].transform.position) > minDistance)
            {
                    anim.SetBool("walk", true);

                    Vector3 vDirection = destination[0].transform.position - transform.position;
                    Vector3 vMoveVector = vDirection.normalized * walkSpeed * Time.deltaTime;
                    transform.position += vMoveVector;

                    Quaternion turretRotation = Quaternion.LookRotation(destination[0].transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //멈추고 기다림.
        else if (desFlag[2])
        {
            anim.SetBool("walk", false);
            desFlag[2] = !desFlag[2];
            StartCoroutine("move2");
        }
        // 1목적지로.
        else if (desFlag[3])
        {
            anim.SetBool("walk", true);

            Vector3 vDirection = destination[1].transform.position - transform.position;
            Vector3 vMoveVector = vDirection.normalized * walkSpeed * Time.deltaTime;
            transform.position += vMoveVector;

            Quaternion turretRotation = Quaternion.LookRotation(destination[1].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
        }
        //멈추고 기다림.
        else if (desFlag[4])
        {
            anim.SetBool("walk", false);
            desFlag[4] = !desFlag[4];

        }
    }

    //토크 애니메이션
    IEnumerator move0()
    {
        yield return new WaitForSeconds(2.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(2.0f);
        desFlag[1] = true;
    }

    //기다렸다가 1목적지로.
    IEnumerator move2()
    {
        yield return new WaitForSeconds(2.0f);
        desFlag[3] = true;
    }

    


    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            Debug.Log("도착");
            gameObject.SetActive(false);

        }

        //중간 목적지 충돌.
        if (coll.tag == "destination")
        {
            //1목적지. 밑으로 이동했을 때. 
            if (desFlag[3] == true)
            {
                desFlag[4] = true;
                desFlag[3] = false;
                destination[1].SetActive(false);
            }
            //0목적지. 위로 이동했을 때. 
            else if (desFlag[1] == true)
            {
                desFlag[2] = true;
                desFlag[1] = false;
                destination[0].SetActive(false);
            }

        }

    }

}
