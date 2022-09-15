using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
    }

    public void ModifyTimeScale(float endTimeValue, float timeToWait, Action OnCompleteHandler = null)
    {
        StartCoroutine(TimeScaleCoroutine(endTimeValue, timeToWait, OnCompleteHandler));
    }

    IEnumerator TimeScaleCoroutine(float endTimeValue, float timeToWait, Action OnCompleteHandler = null)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        Time.timeScale = endTimeValue;
        OnCompleteHandler?.Invoke();
    }
}
