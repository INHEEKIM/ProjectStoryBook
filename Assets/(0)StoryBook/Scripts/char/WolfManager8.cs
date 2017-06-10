using UnityEngine;
using System.Collections;

public class WolfManager8 : MonoBehaviour {

    //목적지
    public GameObject destination;
    //양
    public GameObject sheep;
    //동작 순서 체크
    private bool[] desFlag;

    //딜레이 횟수 체크
    private int delay = 0;

    //끝나고 임시 위치
    public GameObject position;

    //속도
    private float runSpeed = 45.0f;
    private float minDistance = 0.1f;

    private Sheep8 sheepManager;

    private MarkerStateManager mMarkerStateManager;
    private Animator anim;

    void Awake()
    {
        sheepManager = sheep.GetComponent<Sheep8>();
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
        //양 쫓아감.
        if (desFlag[0])
        {
            StopCoroutine("move1");
            StopCoroutine("move00");

            anim.SetBool("Run", false);
            delay = 0;

            if (Vector3.Distance(transform.position, sheep.transform.position) > minDistance)
            {
                anim.SetBool("Run", true);
                Vector3 vDirection = sheep.transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;
                 
                Quaternion turretRotation = Quaternion.LookRotation(sheep.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
            
        }
        //양 쫓고 나서 냠냠
        else if (desFlag[1])
        {
            desFlag[1] = !desFlag[1];
            StartCoroutine("move1");         
        }

        //숲으로 돌아감.
        else if (desFlag[2])
        {
            if (Vector3.Distance(transform.position, destination.transform.position) > minDistance)
            {
                anim.SetBool("Run", true);
                Vector3 vDirection = destination.transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);

            }
        }



    }



    IEnumerator move1()
    {
        anim.SetTrigger("Bite Attack");
        yield return new WaitForSeconds(3.0f);
        desFlag[2] = true;
    }

    IEnumerator move00()
    {
        desFlag[0] = false;
        anim.SetBool("Run", false);
        anim.SetTrigger("Claw Attack");
        yield return new WaitForSeconds(3.0f);
        desFlag[1] = true;
    }


    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            desFlag[2] = false;
            anim.SetBool("Run", false);
            gameObject.transform.position = position.transform.position;
        }

        //양
        if (coll.tag == "sheep")
        {
            if(desFlag[0])
                StartCoroutine("move00");
        }

    }
    public void resetDesFlag()
    {
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }
}
