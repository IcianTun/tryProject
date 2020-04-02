using System;
using System.Collections.Generic;
using UnityEngine;


public class Coordinate : MonoBehaviour {

    private static Coordinate _instance;
    public static Coordinate Instance { get { return _instance; } }

    static Dictionary<string,Transform> dict;
    public Inner inner;
    public Outer outer;

    public static float cellDistance = 7.5f;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        dict = new Dictionary<string, Transform>
        {
            { "Center", inner.Center },
            { "North", inner.North },
            { "East", inner.East },
            { "South", inner.South },
            { "West", inner.West },
            { "NorthWest", inner.NorthWest },
            { "NorthEast", inner.NorthEast },
            { "SouthEast", inner.SouthEast },
            { "SouthWest", inner.SouthWest },

            { "NorthBorder", outer.NorthBorder },
            { "EastBorder", outer.EastBorder },
            { "SouthBorder", outer.SouthBorder },
            { "WestBorder", outer.WestBorder },

            { "NorthBorderWest", outer.NorthBorderWest },
            { "NorthBorderEast", outer.NorthBorderEast },
            { "EastBorderNorth", outer.EastBorderNorth },
            { "EastBorderSouth", outer.EastBorderSouth },
            { "SouthBorderEast", outer.SouthBorderEast },
            { "SouthBorderWest", outer.SouthBorderWest },
            { "WestBorderSouth", outer.WestBorderSouth },
            { "WestBorderNorth", outer.WestBorderNorth },

            { "NWCorner", outer.NWCorner },
            { "NECorner", outer.NECorner },
            { "SECorner", outer.SECorner },
            { "SWCorner", outer.SWCorner },



        };
    }
    public static Transform getCoordinate(string coordinateName)
    {
        return dict[coordinateName];
    }
    public static Transform getCoordinate(CoordinateName coordinateName)
    {
        return dict[Enum.GetName(typeof(CoordinateName), coordinateName)];
    }


}

[System.Serializable]
public struct Inner
{
    public Transform Center;

    public Transform North;
    public Transform East;
    public Transform South;
    public Transform West;

    [Header("Inner Corner")]
    public Transform NorthWest;
    public Transform NorthEast;
    public Transform SouthEast;
    public Transform SouthWest;

}

[System.Serializable]
public struct Outer
{
    [Header("NESW Border")]
    public Transform NorthBorder;
    public Transform EastBorder;
    public Transform SouthBorder;
    public Transform WestBorder;

    [Header("BorderPlus")]
    public Transform NorthBorderWest;
    public Transform NorthBorderEast;
    public Transform EastBorderNorth;
    public Transform EastBorderSouth;
    public Transform SouthBorderEast;
    public Transform SouthBorderWest;
    public Transform WestBorderSouth;
    public Transform WestBorderNorth;
    
    [Header("Corner")]
    public Transform NWCorner;
    public Transform NECorner;
    public Transform SECorner;
    public Transform SWCorner;

}

public enum CoordinateName
{

    Center,

    North,
    East,
    South,
    West,

    NorthWest,
    NorthEast,
    SouthEast,
    SouthWest,

    NorthBorder,
    EastBorder,
    SouthBorder,
    WestBorder,

    NorthBorderWest,
    NorthBorderEast,
    EastBorderNorth,
    EastBorderSouth,
    SouthBorderEast,
    SouthBorderWest,
    WestBorderSouth,
    WestBorderNorth,

    NWCorner,
    NECorner,
    SECorner,
    SWCorner,
    none
}