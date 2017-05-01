using UnityEngine;
using System.Collections;

public class Wolf_nav_2 : MonoBehaviour {

    public GameObject LRRH;
    public GameObject[] Wolf_destination; //목적지
    private bool[] des_flag;

    public float speed = 50.0f;
    public float minDistance = 0.001f;

    private Animator anim;
    private MarkerStateManager mMarkerStateManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

        des_flag = new bool[Wolf_destination.Length];
        for (int i = 0; i < des_flag.Length; i++)
            des_flag[i] = false;

    }

    void Update()
    {
        if (mMarkerStateManager.getChipMarker() == MarkerStateManager.StateType.On)
            Move();
    }

    private void Move()
    {
        //1번째 목적지
        if (des_flag[0] == true)
        {
            anim.SetBool("Run", false);
            Quaternion turretRotation = Quaternion.LookRotation(LRRH.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.time * 0.05f);            
        }

    }

    //늑대 등장.
    //LRRH_nav_2에 쓰임.
    public void Wolf_move1()
    {
        anim.SetBool("Run", true);
        Vector3 vDirection = Wolf_destination[0].transform.position - transform.position;
        Vector3 vMoveVector = vDirection.normalized * speed * Time.deltaTime;
        transform.position += vMoveVector;

        Quaternion turretRotation = Quaternion.LookRotation(Wolf_destination[0].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, turretRotation, Time.deltaTime * 0.05f);
    }
    

    //빨간모자 멈춘 후 2초 후 애니메이션.
    public void Wolf_move2()
    {
        StartCoroutine("Wolf_attack");
    }
    IEnumerator Wolf_attack()
    {
        yield return new WaitForSeconds(2.0f);
        anim.SetTrigger("Bite Attack");
    }




    public bool getDes_flag(int index)
    {
        return des_flag[index];
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
                Wolf_destination[0].SetActive(false);
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
                Wolf_destination[1].SetActive(false);
            }
        }

    }

}
