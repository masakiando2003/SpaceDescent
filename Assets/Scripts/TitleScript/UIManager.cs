using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] float enterGameTime = 1f;
    [SerializeField] AudioClip playTitleButtonSE;
    AudioSource audioSource;

    private StartGame mstStartGame;

    private void Start()
    {
        Time.timeScale = 1;
        mstStartGame = GetComponent<StartGame>();

        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void _StartGame()
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(playTitleButtonSE);
        }
        StartCoroutine(EnterGame());
    }

    private IEnumerator EnterGame()
    {
        yield return new WaitForSeconds(enterGameTime);
        mstStartGame.LoadScene();
    }
}
