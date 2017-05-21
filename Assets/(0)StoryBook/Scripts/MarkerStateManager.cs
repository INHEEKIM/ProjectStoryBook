using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkerStateManager : MonoBehaviour {


    private PageType BookMarker = PageType.Nothing; //페이지를 넘겼을 때에 따른 페이지 별 상태를 저장

    //캐릭터 마커
    private StateType CuboidMarker = StateType.Off; // 임시용 큐브마커 상태 1페이즈

    private StateType CharMarker = StateType.Off;
    private StateType WolfMarker = StateType.Off;
    private StateType PersonsMarker = StateType.Off;
    private StateType SheepMarker = StateType.Off;

    //페이즈 마커
    private StateType OnePageMarker = StateType.Off; // 임시용 스톤마커 상태 저장
    private StateType TwoPageMarker = StateType.Off; // 임시용 chip마커 상태 저장
    private StateType ThreePageMarker = StateType.Off;
    private StateType FourPageMarker = StateType.Off;
    private StateType FivePageMarker = StateType.Off;
    private StateType SixPageMarker = StateType.Off;
    private StateType SevenPageMarker = StateType.Off;
    private StateType EightPageMarker = StateType.Off;

    public enum PageType
    {
        Nothing = -1,
        Page1 = 1,
        Page2 = 2,
        Page3 = 3,
        Page4 = 4,
        Page5 = 5,
        Page6 = 6,
        Page7 = 7,
        Page8 = 8,
        Page9 = 9,
        Page10 = 10
    }

    public enum StateType
    {
        On = 1,
        Off = -1
    }

    public PageType getBookMarkerPageNumber()
    {
        return BookMarker;
    }

    public void setBookMarkerPageNumber(PageType newPageNumber)
    {
        BookMarker = newPageNumber;
    }

    public StateType getCuboidMarker()
    {
        return CuboidMarker;
    }
    public void setCuboidMarker(StateType newState)
    {
        CuboidMarker = newState;
    }

    public StateType getCharMarker()
    {
        return CharMarker;
    }

    public void setCharMarker(StateType newState)
    {
        CharMarker = newState;
    }

    public StateType getWolfMarker()
    {
        return WolfMarker;
    }

    public void setWolfMarker(StateType newState)
    {
        WolfMarker = newState;
    }

    public StateType getPersonsMarker()
    {
        return PersonsMarker;
    }

    public void setPersonsMarker(StateType newState)
    {
        PersonsMarker = newState;
    }

    public StateType getSheepMarker()
    {
        return SheepMarker;
    }

    public void setSheepMarker(StateType newState)
    {
        SheepMarker = newState;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

    public StateType getOnePageMarker()
    {
        return OnePageMarker;
    }

    public void setOnePageMarker(StateType newState)
    {
        OnePageMarker = newState;
    }

    public StateType getTwoPageMarker()
    {
        return TwoPageMarker;
    }

    public void setTwoPageMarker(StateType newState)
    {
        TwoPageMarker = newState;
    }

    public StateType getThreePageMarker()
    {
        return ThreePageMarker;
    }

    public void setThreePageMarker(StateType newState)
    {
        ThreePageMarker = newState;
    }

    public StateType getFourPageMarker()
    {
        return FourPageMarker;
    }

    public void setFourPageMarker(StateType newState)
    {
        FourPageMarker = newState;
    }

    public StateType getFivePageMarker()
    {
        return FivePageMarker;
    }

    public void setFivePageMarker(StateType newState)
    {
        FivePageMarker = newState;
    }

    public StateType getSixPageMarker()
    {
        return SixPageMarker;
    }

    public void setSixPageMarker(StateType newState)
    {
        SixPageMarker = newState;
    }

    public StateType getSevenPageMarker()
    {
        return SevenPageMarker;
    }

    public void setSevenPageMarker(StateType newState)
    {
        SevenPageMarker = newState;
    }

    public StateType getEightPageMarker()
    {
        return EightPageMarker;
    }

    public void setEightPageMarker(StateType newState)
    {
        EightPageMarker = newState;
    }

    
}
