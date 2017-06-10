using UnityEngine;
using System.Collections;

public class Sheep8 : MonoBehaviour {

    //목적지
    public GameObject[] destination;
    //동작 순서 체크
    private bool[] desFlag;
    //양치기
    public GameObject shepherd;
    //늑대
    public GameObject wolf;


    //속도
    private float runSpeed = 28.0f;
    private float minDistance = 0.1f;

    //양치기8
    private ShepherdManager8 shepherdManager;
    //늑대8
    private WolfManager8 wolfManager;

    private MarkerStateManager mMarkerStateManager;
    private Animator anim;

    void Awake()
    {
        shepherdManager = shepherd.GetComponent<ShepherdManager8>();
        wolfManager = wolf.GetComponent<WolfManager8>();

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
            StopCoroutine("move3");

            anim.SetBool("run", false);

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
        if (desFlag[1])
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
        //2번째 목적지로
        if (desFlag[2])
        {
            if (Vector3.Distance(transform.position, destination[2].transform.position) > minDistance)
            {
                Vector3 vDirection = destination[2].transform.position - transform.position;
                Vector3 vMoveVector = vDirection.normalized * runSpeed * Time.deltaTime;
                transform.position += vMoveVector;

                Quaternion turretRotation = Quaternion.LookRotation(destination[2].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 5f);
            }
        }

            
    }


    IEnumerator move3()
    {

        yield return new WaitForSeconds(1.0f);
        desFlag[0] = false;
        desFlag[1] = false;
        desFlag[2] = false;
        anim.SetBool("run", false);
        transform.Rotate(0, 0, -90);
    }


    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "LastDestination")
        {
            gameObject.SetActive(false);
        }
        //늑대
        if (coll.tag == "wolf")
        {
            if(desFlag[0] || desFlag[1])
            StartCoroutine("move3");
        }

        //2목적지. 
        else if (desFlag[2] == true)
        {
            desFlag[2] = false;
            desFlag[3] = true;
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

    public bool getDesFlag(int i)
    {
        return desFlag[i];
    }
    public void resetDesFlag()
    {
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }
}
