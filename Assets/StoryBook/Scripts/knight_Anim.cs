using UnityEngine;
using System.Collections;

public class knight_Anim : MonoBehaviour {

    public static knight_Anim anim;

    private int attack;


    void Awake()
    {
        if (anim == null)
        {
            DontDestroyOnLoad(gameObject);  //don't destroy!!! other Scene
            anim = this;
        }
        else if (anim != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        attack = 0;
    }

    //충돌하는 동안.
    void OnTriggerStay(Collider coll)
    {
        //슬라임 만났을 때
        if (coll.tag == "slime")
        {
            //공격 모션
            gameObject.GetComponent<Animation>().Play("Attack");

            if (attack == 3)
                GameManager.manager.activeNext();
            else if (attack < 4)
                attack++;
            
        }

    }

    //충돌 끝나고 나갈 때
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "slime")
        {
            //보통 상태
            gameObject.GetComponent<Animation>().CrossFade("Wait", 1.0f);
        }
    }




    public int getAttack()
    {
        return attack;
    }
    public void resetAttack()
    {
        attack = 0;
    }

}
