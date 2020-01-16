
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
    public Transform north;

}

//using UnityEngine;

//public class Coordinate : Singleton<Coordinate> {

//    public string myGlobalVar = "whatever";
//    public Vector3 north = new Vector3(0,0.51f,8);

//}

