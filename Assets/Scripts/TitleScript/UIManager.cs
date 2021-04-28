using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private StartGame mstStartGame;

    private void Start()
    {
        mstStartGame = GetComponent<StartGame>();
    }

    public void _StartGame()
    {
        mstStartGame.LoadScene();
    }
}
