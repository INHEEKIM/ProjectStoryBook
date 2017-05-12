﻿/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewButtonTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        Other_Func
    }

    public enum WorldType
    {
        House,
        Sky
    }

    #region PUBLIC_MEMBER_VARIABLES
    public TriggerType triggerType = TriggerType.Other_Func;
    public WorldType worldType = WorldType.House;
    public float activationTime = 1.5f;
    public Material focusedMaterial;
    public Material nonFocusedMaterial;
    //public bool mFocuseState;
    public bool boolTrigger;
    public bool Focused { get; set; }
    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES
    private float mFocusedTime = 0;
    public bool mTriggered = false;
    private TransitionManager mTransitionManager;
    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        //
        boolTrigger = false;

        mTransitionManager = FindObjectOfType<TransitionManager>();
        mTriggered = false;
        //mFocuseState = false; // 포커스 상태 중이면 true
        mFocusedTime = 0;
        Focused = false;
        GetComponent<Renderer>().material = nonFocusedMaterial;
    }

    void Update()
    {
        if (mTriggered)
            return;

        UpdateMaterials(Focused);



        bool startAction = false;
        if (Input.GetMouseButtonUp(0))
        {
            startAction = true;
        }

        if (Focused)
        {
            // Update the "focused state" time
            mFocusedTime += Time.deltaTime;
            if ((mFocusedTime > activationTime) || startAction)
            {
                mTriggered = true;
                mFocusedTime = 0;

                gameObject.SetActive(false);
                boolTrigger = true; //이 값으로 조건 체크함

                //mFocuseState = false;
            }
        }
        else
        {
            // Reset the "focused state" time
            mFocusedTime = 0;
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private void UpdateMaterials(bool focused)
    {

        //포커스 중인 상태에 따른 버튼 Material 변경
        Renderer meshRenderer = GetComponent<Renderer>();
        if (focused)
        {
            if (meshRenderer.material != focusedMaterial)
                meshRenderer.material = focusedMaterial;
        }
        else
        {
            if (meshRenderer.material != nonFocusedMaterial)
                meshRenderer.material = nonFocusedMaterial;
        }

        float t = focused ? Mathf.Clamp01(mFocusedTime / activationTime) : 0;
        foreach (var rnd in GetComponentsInChildren<Renderer>())
        {
            if (rnd.material.shader.name.Equals("Custom/SurfaceScan"))
            {
                rnd.material.SetFloat("_ScanRatio", t);
            }
        }
    }

    //사용안함
    private IEnumerator ResetAfter(float seconds)
    {
        Debug.Log("Resetting View trigger after: " + seconds);

        yield return new WaitForSeconds(seconds);

        Debug.Log("Resetting View trigger: " + this.gameObject.name);

        // Reset variables
        mTriggered = false;
        mFocusedTime = 0;
        Focused = false;
        UpdateMaterials(false);
    }
    #endregion // PRIVATE_METHODS
}

