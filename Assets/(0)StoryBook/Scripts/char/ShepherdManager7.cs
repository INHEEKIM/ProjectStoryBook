using UnityEngine;
using System.Collections;

public class ShepherdManager7 : MonoBehaviour {

    //VR 포탈
    public GameObject Potal;


    //목적지
    public GameObject[] destination;
    //사람 방향
    public GameObject personDir;

    //동작 순서 체크
    private bool[] desFlag;

    //딜레이 횟수 체크
    private int delay = 0;

    //첫시작 flag
    private bool flag = false;

    //속도
    private float runSpeed = 35.0f;
    private float minDistance = 0.1f;


    private MarkerStateManager mMarkerStateManager;
    private Animator anim;

    void Awake()
    {
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

        anim = GetComponent<Animator>();

        desFlag = new bool[11];
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }


    void Update()
    {
        if (mMarkerStateManager.getSevenPageMarker() == MarkerStateManager.StateType.On)
            Move();
    }

    void Move()
    {
        //0번째 목적지로
        if (desFlag[0])
        {
            if (!flag)
            {
                flag = !flag;
                desFlag[9] = true;
            }
            if (Vector3.Distance(transform.position, destination[0].transform.position) > minDistance)
            {
                anim.SetBool("rrr", true);
                Vector3 vDirection = destination[0].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[0].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //마을을 향해 돈다.
        else if (desFlag[1])
        {
            anim.SetBool("rrr", false);
            if (delay < 20)
            {
                Quaternion turretRotation = Quaternion.LookRotation(personDir.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if (delay == 20)
            {
                delay++;
                StartCoroutine("move1");
            }
        }
        //VR 포탈
        else if (desFlag[2])
        {
            Potal.SetActive(true);
            desFlag[2] = !desFlag[2];
            desFlag[3] = !desFlag[3];
        }
        //포탈 작동 후
        else if (desFlag[3])
        {
            if (GameManager.manager.getARTrigger(2))
            {
                desFlag[3] = !desFlag[3];
                StartCoroutine("move3");
            }
        }

        //VR에서 돌아옴. 시간차 줘야 함. 
        else if (desFlag[4])
        {
            if (GameManager.manager.getVRTrigger(2))
            {
                desFlag[4] = !desFlag[4];
                StartCoroutine("move4");
            }
        }
        //마을사람들 토크
        else if (desFlag[5])
        {
            desFlag[5] = !desFlag[5];
            StartCoroutine("move5");
        }

        //다시 목장으로 달려감
        else if (desFlag[6])
        {
                if (Vector3.Distance(transform.position, destination[1].transform.position) > minDistance)
                {
                    anim.SetBool("run", true);
                    Vector3 vDirection = destination[1].transform.position - transform.position;
                    Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                    transform.position += vMoveVector;

                    Quaternion turretRotation = Quaternion.LookRotation(destination[1].transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);

                }

        }



    }


    //딜레이
    IEnumerator move1()
    {
        yield return new WaitForSeconds(1.0f);
        desFlag[1] = false;
        delay = 0;
        desFlag[2] = true;
    }

    //시간차 줌. 포탈 없애고
    IEnumerator move3()
    {
        yield return new WaitForSeconds(1.0f);
        Potal.SetActive(false);
        desFlag[4] = true;
    }
    //딜레이
    IEnumerator move4()
    {
        yield return new WaitForSeconds(5.0f);
        desFlag[4] = false;
        desFlag[5] = true;
    }
    // 토크 외침
    IEnumerator move5()
    {
        desFlag[8] = true;
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(7.0f);
        desFlag[7] = true;
        yield return new WaitForSeconds(7.0f);
        desFlag[6] = true;
    }

    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            desFlag[10] = true;
            gameObject.SetActive(false);
        }

        //중간 목적지 충돌.
        if (coll.tag == "destination")
        {
            //0목적지. 
            if (desFlag[0] == true)
            {
                desFlag[0] = false;
                desFlag[1] = true;
                destination[0].SetActive(false);
                destination[1].SetActive(true);
            }
        }

    }
    public void setDesFlag(int i, bool b)
    {
        desFlag[i] = b;
    }
    public bool getDesFlag(int i)
    {
        return desFlag[i];
    }


}
