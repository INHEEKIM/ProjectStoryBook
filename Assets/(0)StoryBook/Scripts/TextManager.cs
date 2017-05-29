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
    public AudioClip[] Page1_music;

    //2페
    public GameObject shepherd2;
    public AudioClip[] Page2_music;
    public GameObject[] shoutButton2;

    //3페
    public GameObject shepherd3;
    public GameObject[] people3;
    public AudioClip[] Page3_music;

    //4페
    public GameObject shepherd4;
    public GameObject people4;
    public AudioClip[] Page4_music;

    //5페
    public GameObject shepherd5;
    public GameObject people5;
    public AudioClip[] Page5_music;

    //6페
    public GameObject shepherd6;
    public AudioClip[] Page6_music;

    //7페
    public GameObject shepherd7;
    public AudioClip[] Page7_music;
    public GameObject[] shoutButton7;

    //8페
    public GameObject shepherd8;
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
            //vr일 때.
            if (shoutButton2[0].GetComponent<ViewButtonTrigger>().voiceTrigger)
            {
                audioSource.clip = Page2_music[0];
                audioSource.Play();
                shoutButton2[0].GetComponent<ViewButtonTrigger>().voiceTrigger = false;
            }
            if (shoutButton2[1].GetComponent<ViewButtonTrigger>().voiceTrigger)
            {
                audioSource.clip = Page2_music[0];
                audioSource.Play();
                shoutButton2[1].GetComponent<ViewButtonTrigger>().voiceTrigger = false;
            }


            //책 마커만 인식된 경우.
            if (!makerFlag[1])
            {
                if (mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getPersonsMarker() == MarkerStateManager.StateType.On)
                    makerFlag[1] = true;
                else
                    text[1].GetComponent<TextMesh>().text = "양치기 카드와 마을사람 카드를 비춰주세요.";
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
                else if (shepherd2.GetComponent<ShepherdManager2>().getDesFlag(9))
                {
                    //7
                    text[1].GetComponent<TextMesh>().text = "양치기소년의 목소리를 들은 마을사람들은 \n하나둘씩 소년의 주위로 모여들기 시작하더니";
                    audioSource.clip = Page2_music[1]; audioSource.Play();
                    shepherd2.GetComponent<ShepherdManager2>().setDesFlag(9, false);

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
                    text[1].GetComponent<TextMesh>().text = "다음 페이지로 넘기세요.";
                }

            }


        }//2페이지
        #endregion

        #region 3페이지
        if (mMarkerStateManager.getThreePageMarker() == MarkerStateManager.StateType.On)
        {
            //책 마커만 인식된 경우.
            if (!makerFlag[2])
            {
                if (mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getPersonsMarker() == MarkerStateManager.StateType.On)
                    makerFlag[2] = true;
                else
                    text[2].GetComponent<TextMesh>().text = "양치기 카드와 마을사람 카드를 비춰주세요.";
            }
            //캐릭터 카드가 인식된 후.
            else
            {
                if (shepherd3.GetComponent<ShepherdManager3>().getDesFlag(0))
                {
                    //5
                    audioSource.clip = Page3_music[0];
                    audioSource.Play();
                    text[2].GetComponent<TextMesh>().text = "마을 사람들은 모두 몽둥이를 들고 \n목장으로 달려갔어요.";
                }
                else if (people3[0].GetComponent<People3>().getDesFlag(9))
                {
                    //3
                    text[2].GetComponent<TextMesh>().text = "여, 여기에 늑대가 나타났다는 게냐!";
                    audioSource.clip = Page3_music[1]; audioSource.Play();
                    people3[0].GetComponent<People3>().setDesFlag(9, false);
                }
                else if (people3[1].GetComponent<People3>().getDesFlag(9))
                {
                    //2
                    text[2].GetComponent<TextMesh>().text = "어디? 어디에 있는 거냐!";
                    audioSource.Stop();
                    audioSource.clip = Page3_music[2]; audioSource.Play();
                    people3[1].GetComponent<People3>().setDesFlag(9, false);
                }
                else if (shepherd3.GetComponent<ShepherdManager3>().getDesFlag(9))
                {
                    //5
                    text[2].GetComponent<TextMesh>().text = "헤헤헤! 거짓말인데! \n심심해서 장난 좀 쳐본 거에요! 아하하!";
                    audioSource.clip = Page3_music[3]; audioSource.Play();
                    shepherd3.GetComponent<ShepherdManager3>().setDesFlag(9, false);
                }
                else if (people3[0].GetComponent<People3>().getDesFlag(8))
                {
                    //2
                    text[2].GetComponent<TextMesh>().text = "저런 못된 녀석 같으니라고!";
                    audioSource.clip = Page3_music[5]; audioSource.Play();
                    people3[0].GetComponent<People3>().setDesFlag(8, false);
                }
                else if (people3[1].GetComponent<People3>().getDesFlag(8))
                {
                    //2
                    text[2].GetComponent<TextMesh>().text = "어디서 저런 거짓말을 해!";
                    audioSource.clip = Page3_music[6]; audioSource.Play();
                    people3[1].GetComponent<People3>().setDesFlag(8, false);
                }
                else if (shepherd3.GetComponent<ShepherdManager3>().getDesFlag(8))
                {
                    //3
                    text[2].GetComponent<TextMesh>().text = "마을 사람들은 화를 내며 돌아갔어요.";
                    audioSource.clip = Page3_music[4]; audioSource.Play();
                    shepherd3.GetComponent<ShepherdManager3>().setDesFlag(8, false);
                }
                else if (shepherd3.GetComponent<ShepherdManager3>().getDesFlag(10))
                {
                    //
                    text[2].GetComponent<TextMesh>().text = "다음 페이지로 넘기세요.";
                }

            }


        }//3페이지
        #endregion

        #region 4페이지
        if (mMarkerStateManager.getFourPageMarker() == MarkerStateManager.StateType.On)
        {
            //책 마커만 인식된 경우.
            if (!makerFlag[3])
            {
                if (mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getPersonsMarker() == MarkerStateManager.StateType.On)
                    makerFlag[3] = true;
                else
                    text[3].GetComponent<TextMesh>().text = "양치기 카드와 마을사람 카드를 비춰주세요.";
            }
            //캐릭터 카드가 인식된 후.
            else
            {
                if (shepherd4.GetComponent<ShepherdManager4>().getDesFlag(0))
                {
                    //5
                    audioSource.clip = Page4_music[0];
                    audioSource.Play();
                    text[3].GetComponent<TextMesh>().text = "소년은 마을 사람들이 \n놀라는 모습이 재미있었어요.";
                }
                else if (shepherd4.GetComponent<ShepherdManager4>().getDesFlag(9))
                {
                    //6
                    text[3].GetComponent<TextMesh>().text = "며칠 후 양치기 소년은 또 거짓말을 했어요.";
                    audioSource.clip = Page4_music[1]; audioSource.Play();
                    shepherd4.GetComponent<ShepherdManager4>().setDesFlag(9, false);
                }
                else if (shepherd4.GetComponent<ShepherdManager4>().getDesFlag(8))
                {
                    //2
                    text[3].GetComponent<TextMesh>().text = "늑대다! 늑대가 나타났다!";
                    audioSource.Stop();
                    audioSource.clip = Page4_music[2]; audioSource.Play();
                    shepherd4.GetComponent<ShepherdManager4>().setDesFlag(8, false);
                }
                else if (people4.GetComponent<WoodCutter4>().getDesFlag(9))
                {
                    
                    text[3].GetComponent<TextMesh>().text = "꼬마야, 무슨 일이 일어난거니?";
                    audioSource.clip = Page4_music[3]; audioSource.Play();
                    people4.GetComponent<WoodCutter4>().setDesFlag(9, false);
                }
                else if (shepherd4.GetComponent<ShepherdManager4>().getDesFlag(7))
                {
                    
                    text[3].GetComponent<TextMesh>().text = "아저씨, 도와주세요! 목장에 늑대가 나타났어요! ";
                    audioSource.clip = Page4_music[4]; audioSource.Play();
                    shepherd4.GetComponent<ShepherdManager4>().setDesFlag(7, false);
                }
                else if (shepherd4.GetComponent<ShepherdManager4>().getDesFlag(10))
                {
                    text[3].GetComponent<TextMesh>().text = "다음 페이지로 넘기세요.";
                }

            }


        }//4페이지
        #endregion


        #region 5페이지
        if (mMarkerStateManager.getFivePageMarker() == MarkerStateManager.StateType.On)
        {
            //책 마커만 인식된 경우.
            if (!makerFlag[4])
            {
                if (mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getPersonsMarker() == MarkerStateManager.StateType.On)
                    makerFlag[4] = true;
                else
                    text[4].GetComponent<TextMesh>().text = "양치기 카드와 마을사람 카드를 비춰주세요.";
            }
            //캐릭터 카드가 인식된 후.
            else
            {
                if (shepherd5.GetComponent<ShepherdManager5>().getDesFlag(0))
                {
                    audioSource.clip = Page5_music[0];
                    audioSource.Play();
                    text[4].GetComponent<TextMesh>().text = "사람들은 또 몽둥이를 가지고 \n목장으로 헐레벌떡 달려갔어요.";
                }
                else if (shepherd5.GetComponent<ShepherdManager5>().getDesFlag(9))
                {
                    text[4].GetComponent<TextMesh>().text = "또 거짓말인데! 하하하!";
                    audioSource.clip = Page5_music[1]; audioSource.Play();
                    shepherd5.GetComponent<ShepherdManager5>().setDesFlag(9, false);
                }
                else if (people5.GetComponent<People5>().getDesFlag(9))
                {
                    text[4].GetComponent<TextMesh>().text = "못된 녀석 같으니라고! 이젠 두 번 다시 속지 않을 테다!";
                    audioSource.Stop();
                    audioSource.clip = Page5_music[2]; audioSource.Play();
                    people5.GetComponent<People5>().setDesFlag(9, false);
                }
                else if (people5.GetComponent<People5>().getDesFlag(8))
                {
                    text[4].GetComponent<TextMesh>().text = "마을 사람들은 무척 화를 내며 돌아갔어요.";
                    audioSource.clip = Page5_music[3]; audioSource.Play();
                    people5.GetComponent<People5>().setDesFlag(8, false);

                }
                else if (people5.GetComponent<People5>().getDesFlag(7))
                {
                    text[4].GetComponent<TextMesh>().text = "다음 페이지로 넘기세요.";
                }

            }


        }//5페이지
        #endregion

        #region 6페이지
        if (mMarkerStateManager.getSixPageMarker() == MarkerStateManager.StateType.On)
        {
            //책 마커만 인식된 경우.
            if (!makerFlag[5])
            {
                if (mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getSheepMarker() == MarkerStateManager.StateType.On
                    )
                    makerFlag[5] = true;
                else
                    text[5].GetComponent<TextMesh>().text = "양치기 카드와 늑대 카드와 양 카드를 비춰주세요.";
            }
            //캐릭터 카드가 인식된 후.
            else
            {
                if (shepherd6.GetComponent<ShepherdManager6>().getDesFlag(14))
                {
                    audioSource.clip = Page6_music[0];
                    audioSource.Play();
                    text[5].GetComponent<TextMesh>().text = "그러던 어느 날, \n목장에 진짜 무시무시한 늑대가 나타났어요.";
                    shepherd6.GetComponent<ShepherdManager6>().setDesFlag(14, false);
                }
                else if (shepherd6.GetComponent<ShepherdManager6>().getDesFlag(13))
                {
                    text[5].GetComponent<TextMesh>().text = "깜짝 놀란 소년은 \n마을을 향해 부리나케 달려갔답니다.";
                    audioSource.clip = Page6_music[1]; audioSource.Play();
                    shepherd6.GetComponent<ShepherdManager6>().setDesFlag(13, false);
                }
                else if (shepherd6.GetComponent<ShepherdManager6>().getDesFlag(12))
                {
                    text[5].GetComponent<TextMesh>().text = "다음 페이지로 넘기세요.";
                }

            }


        }//6페이지
        #endregion

        #region 7페이지
        if (mMarkerStateManager.getSevenPageMarker() == MarkerStateManager.StateType.On)
        {
            //vr일 때.
            if (shoutButton7[0].GetComponent<ViewButtonTrigger>().voiceTrigger)
            {
                audioSource.clip = Page7_music[0];
                audioSource.Play();
                shoutButton7[0].GetComponent<ViewButtonTrigger>().voiceTrigger = false;
            }
            if (shoutButton7[1].GetComponent<ViewButtonTrigger>().voiceTrigger)
            {
                audioSource.clip = Page7_music[0];
                audioSource.Play();
                shoutButton7[1].GetComponent<ViewButtonTrigger>().voiceTrigger = false;
            }


            //책 마커만 인식된 경우.
            if (!makerFlag[6])
            {
                if (mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On)
                    makerFlag[6] = true;
                else
                    text[6].GetComponent<TextMesh>().text = "양치기 카드를 비춰주세요.";
            }
            //캐릭터 카드가 인식된 후.
            else
            {
                if (shepherd7.GetComponent<ShepherdManager7>().getDesFlag(9))
                {
                    audioSource.clip = Page7_music[0];
                    audioSource.Play();
                    text[6].GetComponent<TextMesh>().text = "도와주세요! 진짜 늑대가 나타났어요!";
                    shepherd7.GetComponent<ShepherdManager7>().setDesFlag(9, false);
                }
                //else if (shepherd7.GetComponent<ShepherdManager7>().getDesFlag(8))
                //{
                //    text[6].GetComponent<TextMesh>().text = "양치기 소년의 외침이 들렸지만 \n마을사람들은 꿈쩍도 하지 않았어요.";
                //    audioSource.clip = Page7_music[1]; audioSource.Play();
                //    shepherd7.GetComponent<ShepherdManager7>().setDesFlag(8, false);
                //}
                else if (shepherd7.GetComponent<ShepherdManager7>().getDesFlag(8))
                {
                    text[6].GetComponent<TextMesh>().text = "제발 도와주세요. 이번엔 진짜로 나타났다고요!";
                    audioSource.clip = Page7_music[2]; audioSource.Play();
                    shepherd7.GetComponent<ShepherdManager7>().setDesFlag(8, false);
                }
                else if (shepherd7.GetComponent<ShepherdManager7>().getDesFlag(7))
                {
                    text[6].GetComponent<TextMesh>().text = "양치기 소년이 호소했지만 \n마을사람들은 들은 체도 하지 않았어요.";
                    audioSource.clip = Page7_music[3]; audioSource.Play();
                    shepherd7.GetComponent<ShepherdManager7>().setDesFlag(7, false);
                }
                else if (shepherd7.GetComponent<ShepherdManager7>().getDesFlag(10))
                {
                    text[6].GetComponent<TextMesh>().text = "다음 페이지로 넘기세요.";
                }

            }


        }//7페이지
        #endregion


        #region 8페이지
        if (mMarkerStateManager.getEightPageMarker() == MarkerStateManager.StateType.On)
        {
            //책 마커만 인식된 경우.
            if (!makerFlag[7])
            {
                if (mMarkerStateManager.getCharMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getWolfMarker() == MarkerStateManager.StateType.On &&
                    mMarkerStateManager.getSheepMarker() == MarkerStateManager.StateType.On
                    )
                    makerFlag[7] = true;
                else
                    text[7].GetComponent<TextMesh>().text = "양치기 카드와 늑대 카드와 양 카드를 비춰주세요.";
            }
            //캐릭터 카드가 인식된 후.
            else
            {
                if (shepherd8.GetComponent<ShepherdManager8>().getDesFlag(0))
                {
                    audioSource.clip = Page8_music[0];
                    audioSource.Play();
                    text[7].GetComponent<TextMesh>().text = "그 틈에 늑대는 양들을 몽땅 잡아먹어 버렸답니다.";
                }
                else if (shepherd8.GetComponent<ShepherdManager8>().getDesFlag(5))
                {
                    text[7].GetComponent<TextMesh>().text = "어떡해! 내 양들!";
                    audioSource.clip = Page8_music[1]; audioSource.Play();
                    shepherd8.GetComponent<ShepherdManager8>().setDesFlag(5, false);
                }
                else if (shepherd8.GetComponent<ShepherdManager8>().getDesFlag(6))
                {
                    text[7].GetComponent<TextMesh>().text = "끝.";
                }

            }


        }//8페이지
        #endregion
    }



}

