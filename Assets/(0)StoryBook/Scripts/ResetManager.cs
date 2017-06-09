using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour {


    //버튼
    public ViewButtonTrigger[] viewButtonTrigger;

    //양치기
    public GameObject[] shepherd;
    public GameObject[] shepherdPosition;
    //마을사람
    public GameObject[] people2;
    public GameObject[] people2Position;
    public GameObject[] people3;
    public GameObject[] people3Position;
    public GameObject[] people4;        //0 혼자 다름
    public GameObject[] people4Position;
    public GameObject[] people5;
    public GameObject[] people5Position;
    //늑대
    public GameObject wolf6;
    public GameObject wolf6Position;
    public GameObject wolf8;
    public GameObject wolf8Position;
    //양
    public GameObject[] sheep6;
    public GameObject[] sheep6Position;
    public GameObject[] sheep8;
    public GameObject[] sheep8Position;
    //목적지
    public GameObject[] des1;   //다 켬
    public GameObject[] des2;   //다 켬
    public GameObject[] des3;   //마지막꺼 false해줘야
    public GameObject[] des4;
    public GameObject[] des5; //마지막꺼 false해줘야
    public GameObject[] des6;
    public GameObject[] des7; //마지막꺼 false해줘야
    public GameObject[] des8;




    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (viewButtonTrigger[0].boolTrigger)
        {
            resetPage1();
        }


    }


    //1페이지 초기화
    void resetPage1()
    {
        viewButtonTrigger[0].boolTrigger = false;
        shepherd[0].transform.position = shepherdPosition[0].transform.position;
        shepherd[0].transform.rotation = shepherdPosition[0].transform.rotation;
        shepherd[0].SetActive(true);

        shepherd[0].GetComponent<ShepherdManager1>().resetDesFlag();
        for (int i = 0; i < des1.Length; i++) des1[i].SetActive(true);



    }










}
