﻿using UnityEngine;
using System.Collections;
using Vuforia;

public class TextManager : MonoBehaviour {

    //text
    public GameObject[] text;
    //마커 확인
    private bool[] makerFlag;

    //1페
    public GameObject shepherd1;
    public AudioClip[] Page1_music;

    //2페
    public GameObject shepherd2;
    public AudioClip[] Page2_music;


    public AudioClip[] Page3_music;
    public AudioClip[] Page4_music;
    public AudioClip[] Page5_music;
    public AudioClip[] Page6_music;
    public AudioClip[] Page7_music;
    public AudioClip[] Page8_music;


    private AudioSource audioSource;


    private MarkerStateManager mMarkerStateManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();

        makerFlag = new bool[8];
        for (int i = 0; i < 8; i++) makerFlag[i] = false;
    }

    void Update()
    {
        #region 1페이지
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
                if (shepherd1.GetComponent<ShepherdManager1>().getDesFlag(0))
                {
                    Debug.Log("1");
                    //4
                    text[0].GetComponent<TextMesh>().text = "옛날 어느 마을에 양치기 소년이 살았어요.";
                    audioSource.clip = Page1_music[0];
                    audioSource.Play();
                }
                else if (shepherd1.GetComponent<ShepherdManager1>().getDesFlag(2))
                {
                    Debug.Log("2");
                    //4
                    text[0].GetComponent<TextMesh>().text = "어휴, 심심해! 뭐 재미있는 일 없을까?";
                    audioSource.clip = Page1_music[1]; audioSource.Play();
                }
                else if (shepherd1.GetComponent<ShepherdManager1>().getDesFlag(4))
                {
                    Debug.Log("3");
                    //7
                    text[0].GetComponent<TextMesh>().text = "궁리를 하던 양치기 소년은 \n마을을 향해 마구 달려가며 소리쳤어요.";
                    audioSource.clip = Page1_music[2]; audioSource.Play();
                }
                else if (shepherd1.GetComponent<ShepherdManager1>().getDesFlag(8))
                {
                    Debug.Log("4");
                    //
                    text[0].GetComponent<TextMesh>().text = "다음 페이지로 넘기세요.";
                }

            }


        }//1페이지
        #endregion

        #region 2페이지
        if (mMarkerStateManager.getTwoPageMarker() == MarkerStateManager.StateType.On)
        {
            //책 마커만 인식된 경우.
            if (!makerFlag[0])
            {
                if (mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On)
                    makerFlag[0] = true;
                else
                    text[0].GetComponent<TextMesh>().text = "양치기 카드와 마을사람 카드를 비춰주세요.";
            }
            //캐릭터 카드가 인식된 후.
            else
            {
                if (shepherd2.GetComponent<ShepherdManager2>().getDesFlag(0))
                {
                    //4
                    audioSource.clip = Page2_music[0];
                    audioSource.Play();
                    text[1].GetComponent<TextMesh>().text = "늑대다! 늑대가 나타났다!";
                    
                }
                else if (shepherd2.GetComponent<ShepherdManager2>().getDesFlag(4))
                {
                    //7
                    text[1].GetComponent<TextMesh>().text = "양치기소년의 목소리를 들은 마을사람들은 \n하나둘씩 소년의 주위로 모여들기 시작하더니";
                    audioSource.clip = Page2_music[1]; audioSource.Play();
                }
                else if (shepherd2.GetComponent<ShepherdManager2>().getDesFlag(5))
                {
                    //3
                    text[1].GetComponent<TextMesh>().text = "어디?! 어디에 늑대가 있다는 거냐!";
                    audioSource.clip = Page2_music[2]; audioSource.Play();
                }
                else if (shepherd2.GetComponent<ShepherdManager2>().getDesFlag(7))
                {
                    //
                    text[0].GetComponent<TextMesh>().text = "다음 페이지로 넘기세요.";
                }

            }


        }//2페이지
        #endregion


    }



}

