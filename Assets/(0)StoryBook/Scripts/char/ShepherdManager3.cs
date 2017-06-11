using UnityEngine;
using System.Collections;

public class ShepherdManager3 : MonoBehaviour {
    //VR 포탈
    public GameObject Potal;


    //목적지
    public GameObject[] destination;
    //사람
    public GameObject[] person;
    //사람 방향
    public GameObject personDir;

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

        desFlag = new bool[11];
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }

    void Update()
    {
        if (mMarkerStateManager.getThreePageMarker() == MarkerStateManager.StateType.On)
            Move();
    }

    void Move()
    {
        //0번째 목적지로
        if (desFlag[0])
        {
            StopCoroutine("move6");
            StopCoroutine("move3");
            StopCoroutine("move4");
            StopCoroutine("move5");

            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("laugh", false);

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
        //1번째 목적지로
        else if (desFlag[1])
        {
            if (Vector3.Distance(transform.position, destination[1].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[1].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

            }
        }
        //2번째 목적지로. 언덕 위
        else if (desFlag[2])
        {
            if (Vector3.Distance(transform.position, destination[2].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[2].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[2].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //3 대기. 포탈 생성
        else if (desFlag[3])
        {
            anim.SetBool("run", false);
            desFlag[3] = !desFlag[3];

            StartCoroutine("move3");
        }
        //4 포탈 작동 후 포탈 없앰.
        else if (desFlag[4])
        {
            if (GameManager.manager.getARTrigger(1))
            {
                desFlag[4] = !desFlag[4];
                StartCoroutine("move4");
            }
        }
        //VR에서 돌아옴. 마을사람들 등장.
        else if (desFlag[5])
        {
            if (GameManager.manager.getVRTrigger(1))
            {
                desFlag[5] = !desFlag[5];
                StartCoroutine("move5");
            }
        }
        //마을사람들 토크 끝나면 대사.
        else if (desFlag[6])
        {
            if (person[1].GetComponent<People3>().getTalkFlag())
            {
                desFlag[6] = !desFlag[6];
                StartCoroutine("move6");
            }
        }



    }
    //move

    //대기. 포탈
    IEnumerator move3()
    {
        yield return new WaitForSeconds(1.0f);
        Potal.SetActive(true);
        desFlag[4] = !desFlag[4];
    }

    //시간차 줌. 포탈 없애고
    IEnumerator move4()
    {
        yield return new WaitForSeconds(1.0f);
        Potal.SetActive(false);
        desFlag[5] = true;
    }

    // 마을사람들 등장.
    IEnumerator move5()
    {
        transform.Rotate(0, 180, 0);
        //마을사람들 목적지.
        for (int i = 3; i < 5; i++)
            destination[i].SetActive(true);
        //마을사람들 모임.
        person[0].SetActive(true);
        desFlag[6] = true;
        yield return new WaitForSeconds(2.0f);
        person[1].SetActive(true);
        
    }

    //웃고 거짓말이라고 말함.
    IEnumerator move6()
    {
        //마을사람들 마지막 목적지.
        destination[5].SetActive(true);

        anim.SetBool("laugh", true);
        desFlag[9] = true;
        yield return new WaitForSeconds(2.0f);
        anim.SetBool("laugh", false);

        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.5f);
        //마을사람들 마지막
        desFlag[7] = true;

        anim.SetBool("laugh", true);
        yield return new WaitForSeconds(8.0f);
        anim.SetBool("laugh", false);
        desFlag[8] = true;
        yield return new WaitForSeconds(5.0f);
        desFlag[10] = true;

    }



    //충돌
    void OnTriggerEnter(Collider coll)
    {

        //중간 목적지 충돌.
        if (coll.tag == "destination")
        {
            //2목적지.
            if (desFlag[2] == true)
            {
                desFlag[2] = false;
                desFlag[3] = true;
                destination[2].SetActive(false);
            }
            //1목적지. 
            else if (desFlag[1] == true)
            {
                desFlag[1] = false;
                desFlag[2] = true;
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



    public bool getDesFlag(int i)
    {
        return desFlag[i];
    }
    public void setDesFlag(int i, bool b)
    {
        desFlag[i] = b;
    }
    public void resetDesFlag()
    {
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;

    }
}
