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
    public GameObject sheep8Position;
    //목적지
    public GameObject[] des1;   //다 켬
    public GameObject[] des2;   //다 켬
    public GameObject[] des3;   
    public GameObject[] des4;
    public GameObject[] des5; 
    public GameObject[] des6;
    public GameObject[] des7; 
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

        //4페
        if (viewButtonTrigger[3].boolTrigger)
            resetPage4();
        //5페
        if (viewButtonTrigger[4].boolTrigger)
            resetPage5();
        //6페
        if (viewButtonTrigger[5].boolTrigger)
            resetPage6();
        //8페
        if (viewButtonTrigger[7].boolTrigger)
            resetPage8();

    }

    #region 1페이지 초기화
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
    #endregion

    #region 2페이지 초기화
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
    #endregion


    #region 4페이지 초기화
    void resetPage4()
    {
        button[3].SetActive(true);
        viewButtonTrigger[3].boolTrigger = false;
        viewButtonTrigger[3].mTriggered = false;

        textManager.setMakerFlag(3, true);

        //양치기
        shepherd[3].transform.position = shepherdPosition[3].transform.position;
        shepherd[3].transform.rotation = shepherdPosition[3].transform.rotation;
        shepherd[3].SetActive(true);
        shepherd[3].GetComponent<ShepherdManager4>().resetDesFlag();
        
        //나무꾼
        people4[0].transform.position = people4Position[0].transform.position;
        people4[0].transform.rotation = people4Position[0].transform.rotation;
        people4[0].SetActive(true);
        people4[0].GetComponent<WoodCutter4>().resetDesFlag();

        //나머지
        people4[1].transform.position = people4Position[1].transform.position;
        people4[1].transform.rotation = people4Position[1].transform.rotation;
        people4[1].SetActive(false);
        people4[1].GetComponent<People4>().resetDesFlag();
        people4[2].transform.position = people4Position[2].transform.position;
        people4[2].transform.rotation = people4Position[2].transform.rotation;
        people4[2].SetActive(false);
        people4[2].GetComponent<People4>().resetDesFlag();

        //목적지
        for (int i = 0; i < des4.Length; i++) des4[i].SetActive(true);


    }

    #endregion

    #region 5페이지 초기화
    void resetPage5()
    {
        button[4].SetActive(true);
        viewButtonTrigger[4].boolTrigger = false;
        viewButtonTrigger[4].mTriggered = false;

        textManager.setMakerFlag(4, true);

        //양치기
        shepherd[4].transform.position = shepherdPosition[4].transform.position;
        shepherd[4].transform.rotation = shepherdPosition[4].transform.rotation;
        shepherd[4].SetActive(true);
        shepherd[4].GetComponent<ShepherdManager5>().resetDesFlag();

        people5[0].transform.position = people5Position[0].transform.position;
        people5[0].transform.rotation = people5Position[0].transform.rotation;
        people5[0].GetComponent<People5>().resetDesFlag();
        people5[0].SetActive(false);
        people5[1].transform.position = people5Position[1].transform.position;
        people5[1].transform.rotation = people5Position[1].transform.rotation;
        people5[1].GetComponent<People5>().resetDesFlag();
        people5[1].SetActive(false);
        people5[2].transform.position = people5Position[2].transform.position;
        people5[2].transform.rotation = people5Position[2].transform.rotation;
        people5[2].GetComponent<People5>().resetDesFlag();
        people5[2].SetActive(false);

        //목적지
        for (int i = 0; i < 7; i++) des5[i].SetActive(true);
        for (int i = 7; i < des5.Length; i++) des5[i].SetActive(false);


    }

    #endregion

    #region 6페이지 초기화
    void resetPage6()
    {
        button[5].SetActive(true);
        viewButtonTrigger[5].boolTrigger = false;
        viewButtonTrigger[5].mTriggered = false;

        textManager.setMakerFlag(5, true);

        //양치기
        shepherd[5].transform.position = shepherdPosition[5].transform.position;
        shepherd[5].transform.rotation = shepherdPosition[5].transform.rotation;
        shepherd[5].SetActive(true);
        shepherd[5].GetComponent<ShepherdManager6>().resetDesFlag();

        //늑대
        wolf6.transform.position = wolf6Position.transform.position;
        wolf6.transform.rotation = wolf6Position.transform.rotation;
        wolf6.SetActive(true);
        wolf6.GetComponent<WolfManager6>().resetDesFlag();

        //양
        sheep6[0].transform.position = sheep6Position[0].transform.position;
        sheep6[0].transform.rotation = sheep6Position[0].transform.rotation;
        sheep6[0].SetActive(true);
        sheep6[0].GetComponent<Sheep6>().resetDesFlag();
        sheep6[1].transform.position = sheep6Position[1].transform.position;
        sheep6[1].transform.rotation = sheep6Position[1].transform.rotation;
        sheep6[1].SetActive(true);
        sheep6[1].GetComponent<Sheep6>().resetDesFlag();
        sheep6[2].transform.position = sheep6Position[2].transform.position;
        sheep6[2].transform.rotation = sheep6Position[2].transform.rotation;
        sheep6[2].SetActive(true);
        sheep6[2].GetComponent<Sheep6>().resetDesFlag();

        //목적지
        for (int i = 0; i < des6.Length; i++) des6[i].SetActive(true);


    }

    #endregion


    #region 8페이지 초기화
    void resetPage8()
    {
        button[7].SetActive(true);
        viewButtonTrigger[7].boolTrigger = false;
        viewButtonTrigger[7].mTriggered = false;

        textManager.setMakerFlag(7, true);

        //양치기
        shepherd[7].transform.position = shepherdPosition[7].transform.position;
        shepherd[7].transform.rotation = shepherdPosition[7].transform.rotation;
        shepherd[7].SetActive(true);
        shepherd[7].GetComponent<ShepherdManager8>().resetDesFlag();

        sheep8[1].SetActive(true);
        sheep8[2].SetActive(true);

        //늑대
        wolf8.transform.position = wolf8Position.transform.position;
        wolf8.transform.rotation = wolf8Position.transform.rotation;
        wolf8.GetComponent<WolfManager8>().resetDesFlag();
        wolf8.SetActive(false);

        //양
        sheep8[0].transform.position = sheep8Position.transform.position;
        sheep8[0].transform.rotation = sheep8Position.transform.rotation;
        sheep8[0].GetComponent<Sheep8>().resetDesFlag();
        sheep8[0].SetActive(false);

        //목적지
        for (int i = 0; i < des8.Length; i++) des8[i].SetActive(true);


    }

    #endregion



}
