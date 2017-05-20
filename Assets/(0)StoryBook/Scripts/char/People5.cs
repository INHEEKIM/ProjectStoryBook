using UnityEngine;
using System.Collections;

public class People5 : MonoBehaviour {

    //양치기
    public GameObject shepherd;
    //양치기방향
    public GameObject shepherdDir;
    //동작 순서 체크
    private bool[] desFlag;

    //목적지
    public GameObject[] destination;

    private int delay = 0;

    //토크 딜레이
    public float delayTime = 1.0f;
    //돌아갈 때 시간
    public float backTime = 1.0f;

    //속도
    private float runSpeed = 35.0f;
    private float minDistance = 0.1f;

    //애니메이션 이름
    public string anim_name;

    //양치기5
    private ShepherdManager5 shepherdManager;

    private MarkerStateManager mMarkerStateManager;
    private Animation anim;

    void Awake()
    {
        shepherdManager = shepherd.GetComponent<ShepherdManager5>();
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

        anim = GetComponent<Animation>();
        desFlag = new bool[10];
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
        //각자 위치로.
        if (desFlag[0])
        {
            anim.CrossFade(anim_name + "_Walk");
            if (Vector3.Distance(transform.position, destination[0].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[0].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[0].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }


        // 돌아서서 대기.
        else if (desFlag[1])
        {
            anim.CrossFade(anim_name + "_Idle");
            if (delay < 30)
            {
                Quaternion turretRotation = Quaternion.LookRotation(shepherdDir.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if (delay == 30)
            {
                delay++;
                StartCoroutine("move1");
            }
        }
        //말하고 화냄.
        else if (desFlag[2])
        {
            if (shepherdManager.getDesFlag(8))
            {
                desFlag[2] = !desFlag[2];
                StartCoroutine("move2");
            }
        }
        //마을로 돌아감.
        else if (desFlag[3])
        {
            anim.CrossFade(anim_name + "_Walk");
            Vector3 vDirection = destination[1].transform.position - transform.position;
            Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
            transform.position += vMoveVector;

            Quaternion turretRotation = Quaternion.LookRotation(destination[1].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
        }


    }



    //대기
    IEnumerator move1()
    {
        yield return new WaitForSeconds(0.5f);
        desFlag[1] = false;
        delay = 0;
        desFlag[2] = !desFlag[2];
    }

    //화냄
    IEnumerator move2()
    {
        yield return new WaitForSeconds(delayTime);
        anim.CrossFade(anim_name + "_Talk");
        yield return new WaitForSeconds(backTime + 1.0f);
        anim.CrossFade(anim_name + "_Idle");
        desFlag[3] = !desFlag[3];
    }



    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            gameObject.SetActive(false);
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
