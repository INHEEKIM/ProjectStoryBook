using UnityEngine;
using System.Collections;

public class VR1Manager : MonoBehaviour {

    //버튼
    public ViewButtonTrigger[] viewButtonTrigger;
    //버튼 확인
    private bool[] buttonFlag;

    public GameObject ExitButton;
    
    void Awake()
    {
        for(int i = 0; i < viewButtonTrigger.Length; i++)
            buttonFlag[i] = false;
    }

    void Update()
    {
        if (viewButtonTrigger[0].boolTrigger == true)
        {
            buttonFlag[0] = true;
        }
            
        if (viewButtonTrigger[1].boolTrigger == true)
            buttonFlag[1] = true;

        if (buttonFlag[0] == true && buttonFlag[1] == true)
            ExitButton.SetActive(true);

    }



}
