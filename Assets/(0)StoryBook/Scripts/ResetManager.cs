using UnityEngine;
using System.Collections;
using Vuforia;

public class ResetManager : MonoBehaviour {


    //리셋 버튼
    public GameObject[] button;
    public ViewButtonTrigger[] viewButtonTrigger;
    //vr 버튼
    public GameObject[] buttonVR;
    public ViewButtonTrigger[] viewButtonTriggerVR;
    //전환 버튼
    public GameObject[] buttonARVR;
    public ViewTrigger[] viewTriggerARVR;

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



    //마커인식
    public GameObject card;
    CardVillagerTrackableEvantHandler cardHandler;

    //텍스트 인식
    public TextManager textManager;

    void Start ()
    {
        cardHandler = card.GetComponent<CardVillagerTrackableEvantHandler>();
	}
	
	void Update ()
    {
        //1페
        if (viewButtonTrigger[0].boolTrigger)
            resetPage1();
        //2페
        if (viewButtonTrigger[1].boolTrigger)
            resetPage2();

    }


    //1페이지 초기화
    void resetPage1()
    {
        button[0].SetActive(true);
        viewButtonTrigger[0].boolTrigger = false;
        viewButtonTrigger[0].mTriggered = false;

        textManager.setMakerFlag(0, true);

        shepherd[0].transform.position = shepherdPosition[0].transform.position;
        shepherd[0].transform.rotation = shepherdPosition[0].transform.rotation;
        shepherd[0].SetActive(true);

        shepherd[0].GetComponent<ShepherdManager1>().resetDesFlag();
        for (int i = 0; i < des1.Length; i++) des1[i].SetActive(true);
    }

    //2페이지 초기화
    void resetPage2()
    {
        //다시보기 버튼 초기화
        button[1].SetActive(true);
        viewButtonTrigger[1].boolTrigger = false;
        viewButtonTrigger[1].mTriggered = false;

        // vr. 일단 오브젝트 빼고.
        viewButtonTriggerVR[0].mTriggered = false;
        viewButtonTriggerVR[0].boolTrigger = false;
        viewButtonTriggerVR[1].mTriggered = false;
        viewButtonTriggerVR[1].boolTrigger = false;

        //vr로 넘어가기 전에 [potal]
        buttonARVR[0].SetActive(false);
        viewTriggerARVR[0].mTriggered = false;
        //ar로 넘어가기 전 [돌아가기]
        viewTriggerARVR[1].mTriggered = false;

        //인식 무시하고 실행
        textManager.setMakerFlag(1, true);

        //양치기
        shepherd[1].transform.position = shepherdPosition[1].transform.position;
        shepherd[1].transform.rotation = shepherdPosition[1].transform.rotation;
        shepherd[1].SetActive(true);
        shepherd[1].GetComponent<ShepherdManager2>().resetDesFlag();
        //목적지
        for (int i = 0; i < des2.Length; i++) des2[i].SetActive(true);

        //마을사람
        people2[0].transform.position = people2Position[0].transform.position;
        people2[0].transform.rotation = people2Position[0].transform.rotation;
        people2[0].GetComponent<People2>().resetDesFlag();
        people2[0].SetActive(false);
        people2[1].transform.position = people2Position[1].transform.position;
        people2[1].transform.rotation = people2Position[1].transform.rotation;
        people2[1].GetComponent<People2>().resetDesFlag();
        people2[1].SetActive(false);
 

    }










}
