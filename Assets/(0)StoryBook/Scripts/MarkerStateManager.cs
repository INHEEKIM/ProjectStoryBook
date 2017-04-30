using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkerStateManager : MonoBehaviour {


    private PageType BookMarker = PageType.Nothing; //페이지를 넘겼을 때에 따른 페이지 별 상태를 저장
    private StateType CuboidMarker = StateType.Off; // 임시용 큐브마커 상태 저장
    private StateType StoneMarker = StateType.Off; //임시용 스톤마커 상태 저장
    private StateType ChipMarker = StateType.Off; //임시용 chip마커 상태 저장

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

    public StateType getStoneMarker()
    {
        return StoneMarker;
    }

    public void setStoneMarker(StateType newState)
    {
        StoneMarker = newState;
    }

    public StateType getChipMarker()
    {
        return ChipMarker;
    }

    public void setChipMarker(StateType newState)
    {
        ChipMarker = newState;
    }
}
