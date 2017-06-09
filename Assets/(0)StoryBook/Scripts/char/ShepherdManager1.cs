using UnityEngine;
using System.Collections;

public class ShepherdManager1 : MonoBehaviour {

    //목적지
    public GameObject[] destination;
    //동작 순서 체크
    private bool[] desFlag;

    //끝나고 임시 위치
    public GameObject position;

    //속도
    private float walkSpeed = 15.0f;
    private float runSpeed = 30.0f;
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
        ////인식 후 토크 애니메이션 1번
        if (desFlag[0])
        {
            StopCoroutine("move0");
            StopCoroutine("move2");
            StopCoroutine("move4");

            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("laugh", false);
            anim.SetTrigger("idle");


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
        //뭔가 생각난 듯 점프하고 킥킥 웃음.
        else if (desFlag[4])
        {
            anim.SetBool("walk", false);
            desFlag[4] = !desFlag[4];
            StartCoroutine("move4");
        }
        // 2목적지로.
        else if (desFlag[5])
        {
            anim.SetBool("run", true);

            Vector3 vDirection = destination[2].transform.position - transform.position;
            Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
            transform.position += vMoveVector;

            Quaternion turretRotation = Quaternion.LookRotation(destination[2].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
        }
        // 3목적지로.
        else if (desFlag[6])
        {
            anim.SetBool("run", true);

            Vector3 vDirection = destination[3].transform.position - transform.position;
            Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
            transform.position += vMoveVector;
        }
        // 4목적지로.
        else if (desFlag[7])
        {
            if (Vector3.Distance(transform.position, destination[4].transform.position) > minDistance)
            {

                anim.SetBool("run", true);

                Vector3 vDirection = destination[4].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;
            }
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

    //토크 애니메이션
    IEnumerator move4()
    {
        yield return new WaitForSeconds(2.0f);
        anim.SetTrigger("jump");
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("laugh", true);
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("laugh", false);
        yield return new WaitForSeconds(1.0f);
        desFlag[5] = true;
    }
    IEnumerator end()
    {
        anim.SetBool("run", false);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);

    }



    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            desFlag[7] = false;

            anim.SetBool("run", false);
            gameObject.transform.position = position.transform.position;

            desFlag[8] = true;
        }

        //중간 목적지 충돌.
        if (coll.tag == "destination")
        {
            //3목적지. 
            if(desFlag[6] == true)
            {
                desFlag[6] = false;
                desFlag[7] = true;
                destination[3].SetActive(false);
            }
            //2목적지. 
            else if(desFlag[5] == true)
            {
                desFlag[5] = false;
                desFlag[6] = true;             
                destination[2].SetActive(false);
            }
            //1목적지. 밑으로 이동했을 때. 
            else if(desFlag[3] == true)
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

    public bool getDesFlag(int i)
    {
        return desFlag[i];
    }
    public void resetDesFlag()
    {
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;

    }


}
