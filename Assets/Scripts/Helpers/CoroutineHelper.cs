using System;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineHelper
{
    static WaitForEndOfFrame _endOfFrame = new();
    public static WaitForEndOfFrame EndOfFrame { get => _endOfFrame; }

    static Dictionary<float, WaitForSeconds> _waitForSeconds = new();
    public static WaitForSeconds WaitForSeconds(float time)
    {
        if (!_waitForSeconds.ContainsKey(time))
        {
            _waitForSeconds.Add(time, new WaitForSeconds(time));
        }
        return _waitForSeconds[time];
    }

    static Dictionary<float, WaitForSecondsRealtime> _waitForSecondsRealtime = new();
    public static WaitForSecondsRealtime WaitForSecondsRealtime(float realTime)
    {
        if (!_waitForSecondsRealtime.ContainsKey(realTime))
        {
            _waitForSecondsRealtime.Add(realTime, new WaitForSecondsRealtime(realTime));
        }
        return _waitForSecondsRealtime[realTime];
    }

    static Dictionary<Func<bool>, WaitUntil> _waitUntil = new();
    public static WaitUntil WaitUntil(Func<bool> predicate)
    {
        if (!_waitUntil.ContainsKey(predicate))
        {
            _waitUntil.Add(predicate, new WaitUntil(predicate));
        }
        return _waitUntil[predicate];
    }
}
