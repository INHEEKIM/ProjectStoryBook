using UnityEngine;
using System.Collections;
using Vuforia;

public class TextManager : MonoBehaviour {

    //text
    public GameObject[] text;
    //마커 확인
    private bool[] makerFlag;

    //1페
    public GameObject shepherd1;



    private MarkerStateManager mMarkerStateManager;

    void Awake()
    {
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

        makerFlag = new bool[8];
        for (int i = 0; i < 8; i++) makerFlag[i] = false;
    }

    void Update()
    {
        if (mMarkerStateManager.getOnePageMarker() == MarkerStateManager.StateType.On)
        {
            //책 마커만 인식된 경우.
            if (!makerFlag[0])
            {
                if (mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On)
                    makerFlag[0] = true;
                else
                    text[0].GetComponent<TextMesh>().text = "양치기 카드를 비춰주세요.";
            }
            //캐릭터 카드가 인식된 후.
            else
            {
                text[0].GetComponent<TextMesh>().text = "옛날 어느 마을에 양치기 소년이 살았어요.";
            }

        }

    }



}

