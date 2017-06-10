using UnityEngine;
using System.Collections;

public class People4 : MonoBehaviour {

    //목적지
    public GameObject[] destination;
    //양치기
    public GameObject shepherd;
    //동작 순서 체크
    private bool[] desFlag;

    //딜레이 횟수 체크
    private int delay = 0;

    //끝나고 임시 위치
    public GameObject position;

    //속도
    private float runSpeed = 33.0f;
    private float minDistance = 0.1f;

    public float delayTime = 1.0f;

    //애니메이션 이름
    public string anim_name;

    //양치기4
    private ShepherdManager4 shepherdManager;

    private MarkerStateManager mMarkerStateManager;
    private Animation anim;

    void Awake()
    {
        shepherdManager = shepherd.GetComponent<ShepherdManager4>();
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

        anim = GetComponent<Animation>();
        desFlag = new bool[10];
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
        //양치기가 늑대가 나타났다고 외침.
        if (desFlag[0])
        {
            StopCoroutine("move2");
            StopCoroutine("move3");
            anim.Stop();


            if (shepherdManager.getDesFlag(2))
            {
                desFlag[0] = !desFlag[0];
                desFlag[1] = !desFlag[1];
            }
        }
        //0목적지 달려감.
        else if (desFlag[1])
        {
            if (Vector3.Distance(transform.position, destination[0].transform.position) > minDistance)
            {
                anim.CrossFade(anim_name + "_Walk");
                Vector3 vDirection = destination[0].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[0].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //양치기 쳐다봄.
        else if (desFlag[2])
        {
            anim.CrossFade(anim_name + "_Idle");
            if (delay < 30)
            {
                Quaternion turretRotation = Quaternion.LookRotation(shepherd.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if (delay == 30)
            {
                delay++;
                StartCoroutine("move2");
            }
        }
        //양치기가 뛰어감.
        else if (desFlag[3])
        {
            if (shepherdManager.getDesFlag(5))
            {
                desFlag[3] = !desFlag[3];
                StartCoroutine("move3");
            }
        }
        //양치기 따라감.
        else if (desFlag[4])
        {
            anim.CrossFade(anim_name + "_Walk");
            Vector3 vDirection = destination[1].transform.position - transform.position;
            Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
            transform.position += vMoveVector;

            Quaternion turretRotation = Quaternion.LookRotation(destination[1].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
        }

    }


    //딜레이
    IEnumerator move2()
    {
        yield return new WaitForSeconds(1.0f);
        desFlag[2] = false;
        delay = 0;
        desFlag[3] = true;
    }
    //딜레이
    IEnumerator move3()
    {
        yield return new WaitForSeconds(delayTime);
        desFlag[4] = !desFlag[4];
    }





    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            desFlag[4] = false;
            anim.CrossFade(anim_name + "_Idle");

            gameObject.transform.position = position.transform.position;
        }

        //중간 목적지 충돌.
        if (coll.tag == "destination")
        {
            //0목적지. 
            if (desFlag[1] == true)
            {
                desFlag[1] = false;
                desFlag[2] = true;
                destination[0].SetActive(false);
            }

        }

    }
    public void resetDesFlag()
    {
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }

}
