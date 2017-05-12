using UnityEngine;
using System.Collections;

public class ShepherdManager4 : MonoBehaviour {

    //목적지
    public GameObject[] destination;
    //사람
    public GameObject person;
    //동작 순서 체크
    private bool[] desFlag;


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
        //1번째 목적지로
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
            Quaternion turretRotation = Quaternion.LookRotation(person.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            StartCoroutine("move3");
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
        yield return new WaitForSeconds(2.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(2.0f);
        anim.SetTrigger("talk2");
        yield return new WaitForSeconds(2.0f);
        desFlag[2] = true;
    }
    //딜레이
    IEnumerator move3()
    {
        yield return new WaitForSeconds(2.0f);
        desFlag[3] = false;
        desFlag[4] = true;
    }
    //딜레이
    IEnumerator move4()
    {
        yield return new WaitForSeconds(2.0f);
        desFlag[5] = true;
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





}
