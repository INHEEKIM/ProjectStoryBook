using UnityEngine;
using System.Collections;

public class ShepherdManager8 : MonoBehaviour {

    //목적지
    public GameObject[] destination;
    //양
    public GameObject sheep;
    //늑대
    public GameObject wolf;
    //동작 순서 체크
    private bool[] desFlag;

    //딜레이 횟수 체크
    private int delay = 0;


    //속도
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
        if (mMarkerStateManager.getEightPageMarker() == MarkerStateManager.StateType.On)
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
        //멈추고 기다림. 양 나타남.
        else if (desFlag[2])
        {
            anim.SetBool("run", false);
            desFlag[2] = !desFlag[2];
            StartCoroutine("move2");
        }
        //늑대 나타나면 2번째 목적지로
        else if (desFlag[3])
        {
            if (Vector3.Distance(transform.position, destination[2].transform.position) > minDistance)
            {
                anim.SetBool("run", true);
                Vector3 vDirection = destination[2].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[2].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }              
        //돌아봄.
        else if (desFlag[4])
        {
            anim.SetBool("run", false);
            if (delay < 20)
            {
                Quaternion turretRotation = Quaternion.LookRotation(wolf.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if (delay == 20)
            {
                delay++;
                StartCoroutine("move4");
            }
        }
        //엉엉

        


    }


    //기다렸다가 2목적지로.
    IEnumerator move2()
    {
        sheep.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        wolf.SetActive(true);
        desFlag[3] = true;
    }

    //딜레이
    IEnumerator move4()
    {
        yield return new WaitForSeconds(0.1f);
        desFlag[4] = false;
        delay = 0;
        yield return new WaitForSeconds(2.0f);
        desFlag[5] = true;
        yield return new WaitForSeconds(6.0f);
        desFlag[6] = true;
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

            //2목적지. 
            if (desFlag[3] == true)
            {
                desFlag[3] = false;
                desFlag[4] = true;
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
