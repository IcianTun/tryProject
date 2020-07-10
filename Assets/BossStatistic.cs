using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BossStatisticData
{
    public float timeUsed;
    public float attackUptimePercentages;
    public int playerHPLeft;
    public bool IsBeaten { get {return playerHPLeft>0; } }
}

public class BossStatistic : MonoBehaviour {

    public List<BossStatisticData> data;

    public float fitnessValue;

    

    #region tst
    //public BossStatisticData GetAverageData()
    //{
    //    float _timeUsed = 0 ;
    //    float _attackUptimePercentages = 0;
    //    int _playerHPLeft = 0;
    //    float beatenPercentage = 0;

    //    foreach( BossStatisticData d in data)
    //    {
    //        _timeUsed += d.timeUsed;
    //        _attackUptimePercentages += d.attackUptimePercentages;
    //        _playerHPLeft += d.playerHPLeft;

    //    }

    //    return new BossStatisticData
    //    {

    //    };
    //}
    #endregion
}
