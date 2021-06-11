using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    static CoroutineHandler _Instance = null;
    public static CoroutineHandler Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = (CoroutineHandler)FindObjectOfType(typeof(CoroutineHandler));

                if (_Instance == null)
                {
                    _Instance = new GameObject(typeof(CoroutineHandler).Name).AddComponent<CoroutineHandler>();
                    _Instance.transform.SetSiblingIndex(1);
                }
            }

            return _Instance;
        }
    }
}

public class Run
{
    public bool Finished { get; private set; }
    public bool Aborted { get; private set; }
    IEnumerator action;

    #region Run each frame
    public static Run EachFrame(Action action)
    {
        Run task = new Run();
        task.action = RunEachFrame(task, action);
        task.Start();
        return task;
    }

    static IEnumerator RunEachFrame(Run _task, Action _action)
    {
        _task.Finished = false;

        while (true)
        {
            if (_action != null && !_task.Aborted)
            {
                _action();
            }
            else break;
            yield return null;
        }

        _task.Finished = true;
    }
    #endregion

    #region Run every
    public static Run Every(float interval, Action action)
    {
        Run task = new Run();
        task.action = RunEvery(task, interval, action);
        task.Start();
        return task;
    }

    static IEnumerator RunEvery(Run _task, float _interval, Action _action)
    {
        _task.Finished = false;

        while (true)
        {
            if (_action != null && !_task.Aborted)
            {
                _action();
            }
            else break;

            yield return CoroutineHelper.WaitForSeconds(_interval);
        }

        _task.Finished = true;
    }
    #endregion

    #region Run after
    public static Run After(float delay, Action action)
    {
        Run task = new Run();
        task.action = RunAfter(task, delay, action);
        task.Start();
        return task;
    }

    static IEnumerator RunAfter(Run _task, float _delay, Action _action)
    {
        _task.Finished = false;

        yield return CoroutineHelper.WaitForSeconds(_delay);
        if (_action != null && !_task.Aborted)
        {
            _action();
        }

        _task.Finished = true;
    }
    #endregion

    #region Run when
    public static Run When(Func<bool> predicate, Action action)
    {
        Run task = new Run();
        task.action = RunWhen(task, predicate, action);
        task.Start();
        return task;
    }

    static IEnumerator RunWhen(Run _task, Func<bool> _predicate, Action _action)
    {
        _task.Finished = false;

        while (true)
        {
            yield return CoroutineHelper.WaitUntil(_predicate);

            if (_action != null && !_task.Aborted)
            {
                _action();
            }
            else break;
        }

        _task.Finished = true;
    }
    #endregion

    #region Run coroutine
    public static Run Coroutine(IEnumerator coroutine)
    {
        Run task = new Run();
        task.action = RunCoroutine(task, coroutine);
        task.Start();
        return task;
    }

    static IEnumerator RunCoroutine(Run _task, IEnumerator _coroutine)
    {
        _task.Finished = false;

        while (true)
        {
            if (_coroutine != null && !_task.Aborted)
            {
                yield return CoroutineHandler.Instance.StartCoroutine(_coroutine);
            }
            else break;
        }

        _task.Finished = true;
    }
    #endregion

    void Start()
    {
        if (action != null)
        {
            CoroutineHandler.Instance.StartCoroutine(action);
        }
    }

    public void Abort()
    {
        Aborted = true;
    }
}

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
