
using UnityEngine;

public class Coordinate : MonoBehaviour {
    private static Coordinate _instance;

    public static Coordinate Instance { get { return _instance; } }


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






}
