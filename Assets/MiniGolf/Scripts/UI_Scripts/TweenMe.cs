using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TweenMe : MonoBehaviour
{
    public bool playOnAwake;
    public float transtionTime = 1f;
    public float offsetTime = 0;
    public Ease onEnableEase = Ease.OutSine;
    public Ease onDisableEase = Ease.InSine;

    private Vector3 orignalPos;

    private void Awake()
    {
        orignalPos = transform.position;
    }

    private void OnEnable()
    {
        transform.DOMove(new Vector3(0, 0, 0), transtionTime).SetEase(onEnableEase);
    }

    private void OnDisable()
    {
        transform.position = orignalPos;
    }

    public void ClosePopup()
    {
        transform.DOMove(orignalPos, transtionTime).SetEase(onDisableEase);
        WaitAfterCall((value) => {
            gameObject.SetActive(false); 
        }, transtionTime + 0.25f);       
    }

    IEnumerator EffectShow(Action<int> can_get_callback, float time, int value = 0)
    {
        yield return new WaitForSeconds(time);
        if (can_get_callback != null)
        {
            can_get_callback(value);
        }
    }

    public void WaitAfterCall(Action<int> can_get_callback = null, float time = 0.2f, int value = 0)
    {
        StartCoroutine(EffectShow(can_get_callback, time, value));
    }
}
