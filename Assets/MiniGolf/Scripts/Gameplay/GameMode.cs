using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public void Play()
    {
        Application.LoadLevel("Menu");
    }

    public void PlayAsGuest()
    {
        Application.LoadLevel("MenuForGuest");
    }
}
