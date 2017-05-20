using UnityEngine;
using System.Collections;

public class ShepherdManager5 : MonoBehaviour {

    //목적지
    public GameObject[] destination;
    //사람
    public GameObject[] person;
    //사람 등장 플래그
    private bool personFlag = false;

    //동작 순서 체크
    private bool[] desFlag;

    //딜레이 횟수 체크
    private int delay = 0;

    //속도
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
        if (mMarkerStateManager.getFivePageMarker() == MarkerStateManager.StateType.On)
            Move();
    }


    void Move()
    {
        //0번째 목적지로
        if (desFlag[0])
        {
            if (Vector3.Distance(transform.position, destination[0].transform.position) > minDistance)
            {
                anim.SetBool("run", true);
                Vector3 vDirection = destination[0].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[0].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);

            }
        }
        //1번째 목적지로. 마을사람들 나타남.
        else if (desFlag[1])
        {
            if (!personFlag)
            {
                personFlag = !personFlag;
                StartCoroutine("move1");
            }
            if (Vector3.Distance(transform.position, destination[1].transform.position) > minDistance)
            {
                //anim.SetBool("run", true);
                Vector3 vDirection = destination[1].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[1].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);

            }
        }
        //2번째 목적지로.
        else if(desFlag[2])
        {
            if (Vector3.Distance(transform.position, destination[2].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[2].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;
            }
        }
        //3번째 목적지로. 언덕 위.
        else if(desFlag[3])
        {
            if (Vector3.Distance(transform.position, destination[3].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[3].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;
            }
        }
        //4 오른쪽으로 돌아봄. 
        else if (desFlag[4])
        {
            anim.SetBool("run", false);
            if(delay < 30)
            {
                Quaternion turretRotation = Quaternion.LookRotation(destination[4].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if(delay == 30)
            {
                delay++;
                StartCoroutine("move4");
            }
                
        }
        //5 왼쪽으로 돌아봄. 
        else if (desFlag[5])
        {
            if (delay < 30)
            {
                Quaternion turretRotation = Quaternion.LookRotation(destination[5].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if (delay == 30)
            {
                delay++;
                StartCoroutine("move5");
            }
        }
        //6 마을사람들 쪽으로 돌아섬
        else if (desFlag[6])
        {
            if (delay < 30)
            {
                Quaternion turretRotation = Quaternion.LookRotation(destination[6].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if (delay == 30)
            {
                delay++;
                StartCoroutine("move6");
            }
        }
        //거짓말이라고 말하고 웃는 모션.
        else if (desFlag[7])
        {
            desFlag[7] = !desFlag[7];
            StartCoroutine("move7");
        }
    }


    // 마을사람들 등장.
    IEnumerator move1()
    {
        //마을사람들 모임.
        person[0].SetActive(true);
        yield return new WaitForSeconds(0.8f);
        person[1].SetActive(true);
        yield return new WaitForSeconds(0.8f);
        person[2].SetActive(true);
    }
    //딜레이
    IEnumerator move4()
    {
        yield return new WaitForSeconds(1.0f);
        desFlag[4] = false;
        delay = 0;
        desFlag[5] = true;
        
    }
    //딜레이
    IEnumerator move5()
    {
        yield return new WaitForSeconds(1.0f);
        desFlag[5] = false;
        delay = 0;
        desFlag[6] = true;
    }
    //딜레이
    IEnumerator move6()
    {
        yield return new WaitForSeconds(1.0f);
        desFlag[6] = false;
        delay = 0;
        desFlag[7] = true;
    }
    //거짓말이라고 말하고 웃는 모션.
    IEnumerator move7()
    {
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("laugh", true);
        desFlag[8] = true;
        yield return new WaitForSeconds(3.0f);
        anim.SetBool("laugh", false);

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
            //3목적지. 
            if (desFlag[3] == true)
            {
                desFlag[3] = false;
                desFlag[4] = true;
                destination[3].SetActive(false);
                destination[9].SetActive(true);
                destination[10].SetActive(true);
            }
            //2목적지. 
            else if(desFlag[2] == true)
            {
                desFlag[2] = false;
                desFlag[3] = true;
                destination[2].SetActive(false);
            }
            //1목적지. 마을사람들 목적지
            else if (desFlag[1] == true)
            {
                desFlag[1] = false;
                desFlag[2] = true;
                destination[1].SetActive(false);
                destination[7].SetActive(true);
                destination[8].SetActive(true);
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

    public bool getDesFlag(int i)
    {
        return desFlag[i];
    }


}
