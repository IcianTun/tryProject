using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DetectorData", order = 1)]

[System.Serializable]
public struct DetectorData
{
    public Transform transformData;
    public int aoeTouchingCount;
    public bool isOutOfBoundary;
}

[System.Serializable]
public struct OverlapDetectData
{
    public Vector3 position;
    public Quaternion rotation;
    public int aoeTouchingCount;
    public bool isOutOfBoundary;
}
