using UnityEngine;
using System.Collections;

public class LRRH_nav_2 : MonoBehaviour {

    public GameObject[] LRRH_destination; //목적지
    private bool[] des_flag;

    private bool wolf_move2_flag = false;

    public float speed = 20.0f;
    public float minDistance = 0.001f;
    private Vector3 nomaliedVector;

    private Animation anim;
    private MarkerStateManager mMarkerStateManager;
    public GameObject wolf;
    private Wolf_nav_2 wolf_nav;

    void Awake()
    {
        anim = GetComponent<Animation>();
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();
        wolf_nav = wolf.GetComponent<Wolf_nav_2>();

        des_flag = new bool[LRRH_destination.Length];
        for (int i = 0; i < des_flag.Length; i++)
            des_flag[i] = false;
        nomaliedVector = Vector3.zero - transform.position;
    }

    void Update()
    {
        if (mMarkerStateManager.getChipMarker() == MarkerStateManager.StateType.On)
            Move();
    }


    private void Move()
    {
        //1번째 목적지 
        //늑대 등장.
        if (!wolf_nav.getDes_flag(0))
        { 
            if (des_flag[0])
            {          
                wolf_nav.Wolf_move1();            
            }
        }
        ////마지막 목적지
        //if (des_flag[3] == true)
        //{
        //    if (Vector3.Distance(transform.position, LRRH_destination[4].transform.position) > minDistance)
        //    {
        //        Vector3 vDirection = LRRH_destination[4].transform.position - transform.position;
        //        Vector3 vMoveVector = vDirection.normalized * speed * Time.deltaTime;
        //        transform.position += vMoveVector;

        //        Quaternion turretRotation = Quaternion.LookRotation(LRRH_destination[4].transform.position - transform.position);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.time * 0.05f);
        //    }
        //}
        ////4번째 목적지
        //else if (des_flag[2] == true)
        //{
        //    if (Vector3.Distance(transform.position, LRRH_destination[3].transform.position) > minDistance)
        //    {
        //        Vector3 vDirection = LRRH_destination[3].transform.position - transform.position;
        //        Vector3 vMoveVector = vDirection.normalized * speed * Time.deltaTime;
        //        transform.position += vMoveVector;

        //        Quaternion turretRotation = Quaternion.LookRotation(LRRH_destination[3].transform.position - transform.position);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.time * 0.05f);
        //    }
        //}
        //2번째 목적지
        //빨간모자 멈춤.
        //2초 후 늑대 애니메이션
        if (des_flag[1] == true)
        {
            anim.CrossFade("wait");
            if (!wolf_move2_flag)
            {
                wolf_nav.Wolf_move2();
                wolf_move2_flag = !wolf_move2_flag;
            }
        }
        //2번째 목적지로
        //빨간모자 이동.
        else if (!des_flag[1])
        {
            Vector3 vDirection = LRRH_destination[1].transform.position - transform.position;
            Vector3 vMoveVector = vDirection.normalized * speed * Time.deltaTime;
            transform.position += vMoveVector;

            Quaternion turretRotation = Quaternion.LookRotation(LRRH_destination[1].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 0.05f);

            anim.Play("run");
        }



    }


    //충돌
    void OnTriggerEnter(Collider coll)
    {
        //최종 목적지 도달.
        if (coll.tag == "EndCube1")
        {
            Debug.Log("도착");
            gameObject.SetActive(false);
        }

        //중간 목적지 충돌.
        if (coll.tag == "destination")
        {
            if (des_flag[0] == false)
            {
                des_flag[0] = true;
                LRRH_destination[0].SetActive(false);
            }
            //else if (des_flag[3] == true)
            //{
            //    des_flag[4] = true;
            //    LRRH_destination[4].SetActive(false);
            //}
            //else if (des_flag[2] == true)
            //{
            //    des_flag[3] = true;
            //    LRRH_destination[3].SetActive(false);
            //}
            //else if (des_flag[1] == true)
            //{
            //    des_flag[2] = true;
            //    LRRH_destination[2].SetActive(false);
            //}
            else if (des_flag[0] == true)
            {
                des_flag[1] = true;
                LRRH_destination[1].SetActive(false);
            }
        }

    }
}
