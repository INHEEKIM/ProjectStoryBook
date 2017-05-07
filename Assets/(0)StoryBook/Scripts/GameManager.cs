using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


    public static GameManager manager;



    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);  //don't destroy!!! other Scene
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

    }




}

