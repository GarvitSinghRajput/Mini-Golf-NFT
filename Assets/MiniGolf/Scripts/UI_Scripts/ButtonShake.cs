using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonShake : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Transform transformSettings;

    private void Awake()
    {
        transformSettings = transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOShakeRotation(1f,15,10,10);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = transformSettings.position;
        transform.rotation = transformSettings.rotation;
    }
}
