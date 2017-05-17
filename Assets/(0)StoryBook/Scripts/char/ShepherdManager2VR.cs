using UnityEngine;
using System.Collections;

public class ShepherdManager2VR : MonoBehaviour {

    //버튼
    //public GameObject[] viewButton;
    public ViewButtonTrigger[] viewButtonTrigger;

    //동작 순서 체크
    private bool[] desFlag;

    private Animator anim;




    void Awake()
    {
        //for (int i = 0; i < viewButton.Length; i++)
        //    viewButtonTrigger[i] = viewButton[i].GetComponent<ViewButtonTrigger>();
        anim = GetComponent<Animator>();

        desFlag = new bool[10];
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }


    void Update()
    {
        Move();
    }


    void Move()
    {
        if (desFlag[0])
        {
            if (viewButtonTrigger[0].boolTrigger)
            {
                anim.SetTrigger("talk");
                desFlag[0] = !desFlag[0];
                
            }
        }


    }


    //기다렸다가 0목적지로.
    IEnumerator move0()
    {
        yield return new WaitForSeconds(1.5f);
        desFlag[1] = true;
    }

}
