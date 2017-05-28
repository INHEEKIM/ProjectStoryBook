using UnityEngine;
using System.Collections;

public class People3 : MonoBehaviour {

    //양치기
    public GameObject shepherd;
    //동작 순서 체크
    private bool[] desFlag;

    //목적지
    public GameObject[] destination;

    //토크 딜레이
    public float delayTime = 1.0f;
    //토크 끝
    private bool talkFlag = false;
    //돌아갈 때 시간
    public float backTime = 1.0f;

    //속도
    private float runSpeed = 35.0f;
    private float minDistance = 0.1f;

    //애니메이션 이름
    public string anim_name;

    //양치기3
    private ShepherdManager3 shepherdManager;

    private MarkerStateManager mMarkerStateManager;
    private Animation anim;

    void Awake()
    {
        shepherdManager = shepherd.GetComponent<ShepherdManager3>();
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
        //VR에서 돌아옴.
        if (desFlag[0])
        {
            if (shepherdManager.getDesFlag(6))
            {
                desFlag[0] = !desFlag[0];
                desFlag[1] = !desFlag[1];
            }
        }
        // 0목적지로.
        else if (desFlag[1])
        {
            anim.CrossFade(anim_name + "_Walk");
            Vector3 vDirection = destination[0].transform.position - transform.position;
            Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
            transform.position += vMoveVector;

            Quaternion turretRotation = Quaternion.LookRotation(destination[0].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
        }

        // 토크
        else if (desFlag[2])
        {
            anim.CrossFade(anim_name + "_Idle");
            desFlag[2] = !desFlag[2];
                StartCoroutine("move2");
        }
        //화냄.
        else if (desFlag[3])
        {
            if (shepherdManager.getDesFlag(7))
            {
                desFlag[3] = !desFlag[3];
                StartCoroutine("move3");
            }
        }
        //마을로 돌아감.
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



    //토크
    IEnumerator move2()
    {
        yield return new WaitForSeconds(delayTime);
        anim.CrossFade(anim_name + "_Talk");
        yield return new WaitForSeconds(3.0f);
        anim.CrossFade(anim_name + "_Idle");
        //토크 끝.
        talkFlag = true;
        desFlag[3] = !desFlag[3];
    }

    //화냄
    IEnumerator move3()
    {
        yield return new WaitForSeconds(delayTime);
        anim.CrossFade(anim_name + "_Talk");
        yield return new WaitForSeconds(backTime + 1.0f);
        anim.CrossFade(anim_name + "_Idle");
        desFlag[4] = !desFlag[4];
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
        if (desFlag[1] == true)
        {
            desFlag[1] = false;
            desFlag[2] = true;
            destination[0].SetActive(false);
        }
    }

    public bool getDesFlag(int i)
    {
        return desFlag[i];
    }
    public bool getTalkFlag()
    {
        return talkFlag;
    }
}
