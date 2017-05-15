using UnityEngine;
using System.Collections;

public class Sheep6 : MonoBehaviour {

    //목적지
    public GameObject destination;
    //동작 순서 체크
    private bool[] desFlag;
    //늑대
    public GameObject wolf;


    //대기 시간
    public float delayTime = 1.0f;


    //속도
    private float runSpeed = 25.0f;
    private float minDistance = 0.1f;
    
    //늑대6
    private WolfManager6 wolfManager;

    private MarkerStateManager mMarkerStateManager;
    private Animator anim;

    void Awake()
    {
        wolfManager = wolf.GetComponent<WolfManager6>();
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();
        anim = GetComponent<Animator>();
        desFlag = new bool[10];
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }

    void Update()
    {
        if (mMarkerStateManager.getSixPageMarker() == MarkerStateManager.StateType.On)
            Move();
    }

    void Move()
    {
        if (desFlag[0])
        {
            if (wolfManager.getDesFlag(1))
            {
                desFlag[0] = !desFlag[0];
                StartCoroutine("move0");
            }
        }
        //0번째 목적지로
        else if (desFlag[1])
        {
            if (Vector3.Distance(transform.position, destination.transform.position) > minDistance)
            {
                anim.SetBool("run", true);
                Vector3 vDirection = destination.transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);

            }
        }

    }

    //기다렸다가 0목적지로.
    IEnumerator move0()
    {
        yield return new WaitForSeconds(delayTime);
        desFlag[1] = true;
    }



    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            gameObject.SetActive(false);
        }

    }


}
