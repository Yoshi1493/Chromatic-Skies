using System;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineHelper
{
    static WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();
    public static WaitForEndOfFrame EndOfFrame { get; }

    static Dictionary<float, WaitForSeconds> _waitForSeconds = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (!_waitForSeconds.ContainsKey(seconds))
        {
            _waitForSeconds.Add(seconds, new WaitForSeconds(seconds));
        }
        return _waitForSeconds[seconds];
    }

    static Dictionary<Func<bool>, WaitUntil> _waitUntil = new Dictionary<Func<bool>, WaitUntil>();
    public static WaitUntil WaitUntil(Func<bool> predicate)
    {
        if (!_waitUntil.ContainsKey(predicate))
        {
            _waitUntil.Add(predicate, new WaitUntil(predicate));
        }
        return _waitUntil[predicate];
    }
}
