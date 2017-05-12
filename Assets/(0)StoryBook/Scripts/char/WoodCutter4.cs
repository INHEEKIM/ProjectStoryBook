using UnityEngine;
using System.Collections;

public class WoodCutter4 : MonoBehaviour {

    //목적지
    public GameObject[] destination;
    //양치기
    public GameObject shepherd;
    //동작 순서 체크
    private bool[] desFlag;


    //속도
    private float runSpeed = 30.0f;
    private float minDistance = 0.1f;


    private MarkerStateManager mMarkerStateManager;
    private Animator anim;

    void Awake()
    {
        mMarkerStateManager = GameObject.Find("MarkerManager").GetComponent<MarkerStateManager>();
        anim = GetComponent<Animator>();
        desFlag = new bool[10];
        for (int i = 0; i < desFlag.Length; i++)
            desFlag[i] = false;
        desFlag[0] = true;
    }
}
