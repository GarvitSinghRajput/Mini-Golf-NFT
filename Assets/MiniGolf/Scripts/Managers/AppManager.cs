using System;
using System.Collections.Generic;
using UnityEngine;
using MoralisUnity.Web3Api;
using MoralisUnity;
using MoralisUnity.Core;

public class AppManager : MonoBehaviour
{
    public static event Action Started;
    private void Awake()
    {
        Started?.Invoke();
    }
}
