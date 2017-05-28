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

        //1페이지
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



    }



}

