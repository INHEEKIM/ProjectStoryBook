﻿using UnityEngine;
using System.Collections;
using Vuforia;
using System;

public class VirtualButton1 : MonoBehaviour, IVirtualButtonEventHandler{

    

    private GameObject resetButton;

    void Start () {
        resetButton = GameObject.Find("resetButton");
        resetButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

	}




    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        GameManager.manager.inactiveNext();
        //throw new NotImplementedException();
       
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        //throw new NotImplementedException();
    }

}
