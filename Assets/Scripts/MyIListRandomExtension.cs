using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyIListRandomExtension{

    /// <summary>
    /// Shuffles the element order of the specified list kub.
    /// </summary>
    public static void TunShuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    /// <summary>
    /// Select n object in the list with IndexList provided kub.
    /// </summary>
    public static List<T> TunSelect<T>(this List<T> ts, int n, List<int> currentIndexList)
    {
        List<T> result = new List<T>();
        if (currentIndexList == null)
        {
            //Debug.Log("New index list hai");
            currentIndexList = new List<int>();
        }
        if (currentIndexList.Count == 0)
        {
            //Debug.Log("Add indexss");
            for (int i = 0; i < ts.Count; i++)
            {
                currentIndexList.Add(i);
            }
        }

        currentIndexList.TunShuffle();
        for (int i = n - 1; i >= 0; i--)
        {
            result.Add(ts[currentIndexList[i]]);
            currentIndexList.RemoveAt(i);
        }

        return result;
    }
    /// <summary>
    /// Select n object in the list with IndexList provided kub.
    /// </summary>
    public static List<T> TunSelect<T>(this List<T> ts, int n)
    {
        List<T> result = new List<T>();
        List<int> currentIndexList = new List<int>();
        if (currentIndexList.Count == 0)
        {
            //Debug.Log("Add indexss");
            for (int i = 0; i < ts.Count; i++)
            {
                currentIndexList.Add(i);
            }
        }

        currentIndexList.TunShuffle();
        for (int i = n - 1; i >= 0; i--)
        {
            result.Add(ts[currentIndexList[i]]);
            currentIndexList.RemoveAt(i);
        }

        return result;
    }

    /// <summary>
    /// Select random [0,n) in amount passed kub.
    /// </summary>
    public static List<int> TunRandomRanges(this List<int> ts, int range, int amount)
    {
        List<int> currentIndexList = new List<int>();
        for (int i = 0; i < range; i++)
        {
            currentIndexList.Add(i);
        }
        currentIndexList.TunShuffle();
        List<int> result = currentIndexList.GetRange(0, amount);
        result.Sort();
        return result;
    }

}
