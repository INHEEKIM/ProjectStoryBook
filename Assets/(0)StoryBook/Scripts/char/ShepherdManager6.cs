using UnityEngine;
using System.Collections;

public class ShepherdManager6 : MonoBehaviour
{

    //목적지
    public GameObject[] destination;
    //늑대
    public GameObject wolf;
    //동작 순서 체크
    private bool[] desFlag;

    //딜레이 횟수 체크
    private int delay = 0;


    //속도
    private float walkSpeed = 15.0f;
    private float runSpeed = 35.0f;
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
        //0번째 목적지로
        if (desFlag[0])
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
        else if (desFlag[1])
        {
            anim.SetBool("walk", false);
            desFlag[1] = !desFlag[1];
            StartCoroutine("move1");
        }
        //1번째 목적지로
        else if (desFlag[2])
        {
            if (Vector3.Distance(transform.position, destination[1].transform.position) > minDistance)
            {
                anim.SetBool("walk", true);
                Vector3 vDirection = destination[1].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * walkSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[1].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //멈추고 기다림.
        else if (desFlag[3])
        {
            anim.SetBool("walk", false);
            desFlag[3] = !desFlag[3];
            StartCoroutine("move3");
        }
        //2번째 목적지로
        else if (desFlag[4])
        {
            if (Vector3.Distance(transform.position, destination[2].transform.position) > minDistance)
            {
                anim.SetBool("walk", true);
                Vector3 vDirection = destination[2].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * walkSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[2].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //늑대 쪽으로 돌아봄.
        else if (desFlag[5])
        {
            anim.SetBool("walk", false);
            if (delay < 10)
            {
                Quaternion turretRotation = Quaternion.LookRotation(wolf.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if (delay == 10)
            {
                delay++;
                StartCoroutine("move5");
            }
        }
        //늑대를 보고 놀람.
        else if (desFlag[6])
        {
            desFlag[6] = !desFlag[6];
            StartCoroutine("move6");
        }
        //3 머리를 감싸고 마을로 달려감.
        else if (desFlag[7])
        {
            anim.SetBool("rrr", true);
            if (Vector3.Distance(transform.position, destination[3].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[3].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[3].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //4 목적지
        else if (desFlag[8])
        {
            if (Vector3.Distance(transform.position, destination[4].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[4].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[4].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 10f);
            }
        }
        //5 목적지
        else if (desFlag[9])
        {
            if (Vector3.Distance(transform.position, destination[5].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[5].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;
            }
        }
    }


    //기다렸다가 1목적지로.
    IEnumerator move1()
    {
        yield return new WaitForSeconds(1.0f);
        desFlag[2] = true;
    }
    //기다렸다가 2목적지로.
    IEnumerator move3()
    {
        yield return new WaitForSeconds(1.0f);
        desFlag[4] = true;
    }

    //딜레이
    IEnumerator move5()
    {
        yield return new WaitForSeconds(0.1f);
        desFlag[5] = false;
        delay = 0;
        desFlag[6] = true;
    }
    //늑대를 보고 놀람
    IEnumerator move6()
    {
        anim.SetTrigger("jump");
        yield return new WaitForSeconds(1.0f);
        desFlag[6] = false;
        delay = 0;
        desFlag[7] = true;
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
            //4목적지. 
            if (desFlag[8] == true)
            {
                desFlag[8] = false;
                desFlag[9] = true;
                destination[4].SetActive(false);
            }
            //3목적지. 
            else if(desFlag[7] == true)
            {
                desFlag[7] = false;
                desFlag[8] = true;
                destination[3].SetActive(false);
            }
            //2목적지. 
            else if (desFlag[4] == true)
            {
                desFlag[4] = false;
                desFlag[5] = true;
                destination[2].SetActive(false);
            }
            //1목적지. 
            else if (desFlag[2] == true)
            {
                desFlag[2] = false;
                desFlag[3] = true;
                destination[1].SetActive(false);
            }
            //0목적지. 
            else if (desFlag[0] == true)
            {
                desFlag[0] = false;
                desFlag[1] = true;
                destination[0].SetActive(false);
            }
        }

    }
}

