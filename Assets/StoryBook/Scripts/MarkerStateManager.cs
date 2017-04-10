using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkerStateManager : MonoBehaviour {

    private StateType CuboidMarker = StateType.Off;
    private StateType StoneMarker = StateType.Off;

    public enum StateType
    {
        On = 1,
        Off = -1
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
}
