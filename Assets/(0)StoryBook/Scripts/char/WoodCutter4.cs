using UnityEngine;
using System.Collections;

public class WoodCutter4 : MonoBehaviour {

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
    private float runSpeed = 38.0f;
    private float minDistance = 0.1f;

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
            StopCoroutine("move3");
            StopCoroutine("move4");
            StopCoroutine("move5");
            StopCoroutine("move6");
            StopCoroutine("move7");
            anim.CrossFade("WC_Work");

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
                anim.CrossFade("WC_Walk");
                Vector3 vDirection = destination[0].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[0].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //1목적지 달려감.
        else if (desFlag[2])
        {
            if (Vector3.Distance(transform.position, destination[1].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[1].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[1].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //2목적지 달려감.
        else if (desFlag[3])
        {
            if (Vector3.Distance(transform.position, destination[2].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[2].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;
            }
        }
        //3목적지 달려감.
        else if (desFlag[4])
        {
            if (Vector3.Distance(transform.position, destination[3].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[3].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[3].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }
        //양치기 쳐다봄.
        else if (desFlag[5])
        {
            anim.CrossFade("WC_Idle");
            if (delay < 30)
            {
                Quaternion turretRotation = Quaternion.LookRotation(shepherd.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
                delay++;
            }
            else if (delay == 30)
            {
                delay++;
                StartCoroutine("move5");
            }
        }
        //토크
        else if (desFlag[6])
        {
                desFlag[6] = !desFlag[6];
                StartCoroutine("move6");
        }

        //양치기가 뛰어감.
        else if (desFlag[7])
        {
            if (shepherdManager.getDesFlag(5))
            {
                desFlag[7] = !desFlag[7];
                StartCoroutine("move7");
                
            }
        }
        //양치기 따라감.
        else if (desFlag[8])
        {
            anim.CrossFade("WC_Walk");
            Vector3 vDirection = destination[4].transform.position - transform.position;
            Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
            transform.position += vMoveVector;

            Quaternion turretRotation = Quaternion.LookRotation(destination[4].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
        }

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
    //딜레이
    IEnumerator move5()
    {
        yield return new WaitForSeconds(0.5f);
        desFlag[5] = false;
        delay = 0;
        desFlag[6] = true;
    }
    //토크
    IEnumerator move6()
    {
        yield return new WaitForSeconds(0.5f);
        desFlag[9] = true;
        anim.CrossFade("WC_Talk");
        yield return new WaitForSeconds(1.0f);
        desFlag[7] = !desFlag[7];
    }
    //딜레이
    IEnumerator move7()
    {
        yield return new WaitForSeconds(0.5f);
        desFlag[8] = !desFlag[8];
    }





    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            desFlag[8] = false;
            anim.CrossFade("WC_Idle");
            gameObject.transform.position = position.transform.position;
        }

        //중간 목적지 충돌.
        if (coll.tag == "destination")
        {
            //3목적지. 
            if (desFlag[4] == true)
            {
                desFlag[4] = false;
                desFlag[5] = true;
                destination[3].SetActive(false);
            }
            //2목적지. 
            else if(desFlag[3] == true)
            {
                desFlag[3] = false;
                desFlag[4] = true;
                destination[2].SetActive(false);
            }
            //1목적지. 
            else if (desFlag[2] == true)
            {
                desFlag[2] = false;
                desFlag[3] = true;
                destination[1].SetActive(false);
            }
            //0목적지. 
            else if (desFlag[1] == true)
            {
                desFlag[1] = false;
                desFlag[2] = true;
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
