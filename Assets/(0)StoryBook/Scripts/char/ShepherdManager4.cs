using UnityEngine;
using System.Collections;

public class ShepherdManager4 : MonoBehaviour {

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
    private float walkSpeed = 15.0f;
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
        if (mMarkerStateManager.getFourPageMarker() == MarkerStateManager.StateType.On)
            Move();
    }

    void Move()
    {
        //0번째 목적지로
        if (desFlag[0])
        {
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
        //늑대가 나타났다고 외침
        else if (desFlag[1])
        {
            anim.SetBool("rrr", false);
            desFlag[1] = !desFlag[1];
            StartCoroutine("move1");
        }
        //마을사람들 나타난 후 목적지 1로.
        else if (desFlag[2])
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
        //마을사람들을 향해 돈다.
        else if (desFlag[3])
        {
            anim.SetBool("run", false);
            if (delay < 20)
            {
                Quaternion turretRotation = Quaternion.LookRotation(personDir.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if (delay == 20)
            {
                delay++;
                StartCoroutine("move3");
            }

        }
        //늑대가 나타났다고 한다.
        else if (desFlag[4])
        {
            anim.SetTrigger("talk2");
            desFlag[4] = !desFlag[4];
            StartCoroutine("move4");
        }
        //서북쪽길로 감.
        else if (desFlag[5])
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
    }

    //늑대가 나타났다고 외침
    IEnumerator move1()
    {
        yield return new WaitForSeconds(6.0f);
        desFlag[9] = true;

        yield return new WaitForSeconds(7.0f);
        anim.SetTrigger("talk2");
        desFlag[8] = true;
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(2.0f);
        //마을사람들 모임.
        for(int i=0; i < person.Length; i++)
        {
            person[i].SetActive(true);
        }
        desFlag[2] = true;
    }
    //딜레이
    IEnumerator move3()
    {
        yield return new WaitForSeconds(9.0f);
        desFlag[3] = false;
        delay = 0;
        desFlag[4] = true;
    }
    //딜레이
    IEnumerator move4()
    {
        //yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");
        desFlag[7] = true;
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("talk2");

        yield return new WaitForSeconds(3.0f);
        desFlag[4] = false;
        desFlag[5] = true;
    }

    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            gameObject.SetActive(false);
            desFlag[10] = true;
        }

        //중간 목적지 충돌.
        if (coll.tag == "destination")
        {
            //1목적지. 
            if (desFlag[2] == true)
            {
                desFlag[2] = false;
                desFlag[3] = true;
                destination[1].SetActive(false);
            }
            //0목적지. 
            if (desFlag[0] == true)
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


}
