using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtility : MonoBehaviour {

    private static MyUtility _instance;
    public static MyUtility Instance { get { return _instance; } }

    public GameObject SquareAoe;
    public GameObject CircleAoe;
    static Dictionary<AoeType, GameObject> dict;

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
        dict = new Dictionary<AoeType, GameObject>
        {
            { AoeType.Square, SquareAoe },
            { AoeType.Circle, CircleAoe },
        };
    }
    public static GameObject GetAoePrefabObject(AoeType aoeType)
    {
        return dict[aoeType];
    }
}
