
using System.Collections.Generic;
using UnityEngine;

public class Coordinate : MonoBehaviour {
    private static Coordinate _instance;

    public static Coordinate Instance { get { return _instance; } }

    Dictionary<string,Transform> dict;

    private void Awake()
    {
        dict = new Dictionary<string, Transform>();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        dict.Add("Center", Center);
        dict.Add("North", North);
        dict.Add("East", East);
        dict.Add("South", South);
        dict.Add("West", West);
        dict.Add("NorthWest", NorthWest);
        dict.Add("NorthEast", NorthEast);
        dict.Add("SouthEast", SouthEast);
        dict.Add("SouthWest", SouthWest);
    }
    public Transform Center;
    public Transform North;
    public Transform East;
    public Transform South;
    public Transform West;

    public Transform NorthWest;
    public Transform NorthEast;
    public Transform SouthEast;
    public Transform SouthWest;



    public Transform getCoordinate(string coordinateName)
    {
        return dict[coordinateName];
    }


}
