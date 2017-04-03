using UnityEngine;
using System.Collections;

public class knight_move : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.eulerAngles.y += roteSpeed;
    }



    private ArrayList GetAnimationList()
    {
        ArrayList tmpArray = new ArrayList();

        //Debug.Log(gameObject.GetComponent<Animation>().GetClip("Walk").name);
        tmpArray.Add(gameObject.GetComponent<Animation>().GetClip("Walk"));
        tmpArray.Add(gameObject.GetComponent<Animation>().GetClip("Attack"));

//        gameObject.GetComponent<Animation>().CrossFade()

        return tmpArray;
    }
}
