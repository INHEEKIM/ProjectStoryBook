using UnityEngine;
using System.Collections;

public class slime_anim : MonoBehaviour {


    void Start()
    {
        gameObject.GetComponent<Animation>().Play("Wait");
    }

    //충돌하는 동안.
    void OnTriggerStay(Collider coll)
    {
        //슬라임 만났을 때
        if (coll.tag == "knight")
        {
            //공격 모션
            gameObject.GetComponent<Animation>().Play("Attack");
        }

    }

    //충돌 끝나고 나갈 때
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "knight")
        {
            //보통 상태
            gameObject.GetComponent<Animation>().CrossFade("Wait", 1.0f);
        }
    }

}
