using UnityEngine;
using System.Collections;

public class People2 : MonoBehaviour {

    //양치기
    public GameObject shepherd;
    //동작 순서 체크
    private bool[] desFlag;

    //토크 딜레이
    public float delayTime = 2.0f;
    //토크 끝
    private bool talkFlag = false;

    //속도
    private float runSpeed = 38.0f;
    private float minDistance = 0.1f;

    //애니메이션 이름
    public string anim_name;

    //양치기2
    private ShepherdManager2 shepherdManager;

    private MarkerStateManager mMarkerStateManager;
    private Animation anim;

    void Awake()
    {
        shepherdManager = shepherd.GetComponent<ShepherdManager2>();
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

        anim = GetComponent<Animation>();
        desFlag = new bool[10];
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }

    void Update()
    {
        if (mMarkerStateManager.getTwoPageMarker() == MarkerStateManager.StateType.On)
            Move();
    }

    void Move()
    {
        //VR에서 돌아옴.
        if (desFlag[0])
        {
            if (shepherdManager.getTalkFlag())
            {
                desFlag[0] = !desFlag[0];
                desFlag[1] = !desFlag[1];
            }
        }
        //토크 모션
        else if (desFlag[1])
        {
            desFlag[1] = !desFlag[1];
            StartCoroutine("move1");
        }
        //양치기가 뛰어감.
        else if (desFlag[2])
        {
            if (shepherdManager.getDesFlag(6) && talkFlag)
            {
                desFlag[2] = !desFlag[2];
                StartCoroutine("move2");
            }
        }
        //양치기 따라감.
        else if (desFlag[3])
        {
            anim.CrossFade(anim_name + "_Walk");
            Vector3 vDirection = shepherd.transform.position - transform.position;
            Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
            transform.position += vMoveVector;

            Quaternion turretRotation = Quaternion.LookRotation(shepherd.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
        }


    }



    //토크
    IEnumerator move1()
    {
        yield return new WaitForSeconds(delayTime);
        anim.CrossFade(anim_name + "_Talk");
        yield return new WaitForSeconds(2.0f);
        //토크 끝.
        talkFlag = true;
        desFlag[2] = !desFlag[2];
    }
    //딜레이
    IEnumerator move2()
    {
        yield return new WaitForSeconds(2.0f);
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
    }


    public bool getTalkFlag()
    {
        return talkFlag;
    }
}
