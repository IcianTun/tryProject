using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DetectorData", order = 1)]
public struct DetectorData
{
    public Transform transformData;
    public int aoeTouchingCount;
    public bool isOutOfBoundary;
}