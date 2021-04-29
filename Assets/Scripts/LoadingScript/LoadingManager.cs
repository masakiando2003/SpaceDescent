using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] GameObject dialogText;
    [SerializeField] private GameObject[] stages;
    [SerializeField] GameObject blackScreenImageObject;
    [SerializeField] float fadeTime = 2f;
    [SerializeField] float changeStageTime = 1f;

    private static int playStage;

    private IEnumerator enableStageProp;

    // Start is called before the first frame update
    enum PlayScene
    {
        Introduction,
        second,
        third,
        fourth,
        fifth,
        sixth,
        last,
        FadeScreen,
        ChangeScene
    }

    void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].SetActive(false);
        }

        StartCoroutine(UpdateStageChange());
        blackScreenImageObject.SetActive(false);
    }

    void Update()
    {
        if (playStage < (int) PlayScene.FadeScreen)
        {
            if (enableStageProp != null) StopCoroutine((EnablePlaySceneAnim(playStage)));
            enableStageProp = EnablePlaySceneAnim(playStage);
            StartCoroutine(enableStageProp);
            //     Debug.Log(playstage);
        }
        else if (playStage == (int) PlayScene.FadeScreen)
        {
            StartCoroutine(FadeIntoStage());
        }
        else if (playStage == (int) PlayScene.ChangeScene)
        {
            StopAllCoroutines();
            SceneManager.LoadScene("Stage");
        }
    }

    void EnableStageProp(PlayScene stage)
    {
        int StageNum = (int) stage;
        stages[StageNum].SetActive(true);
        for (int i = 0; i < stages.Length; i++)
        {
            if (i != StageNum)
                stages[i].SetActive(false);
        }
    }

    private IEnumerator UpdateStageChange()
    {
        while (playStage < (int) PlayScene.FadeScreen)
        {
            playStage = dialogText.GetComponent<TypeWriterEffect>().PlayScene;
            //Debug.Log("Playstage is " + playStage);

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("Finish Update playstage");
    }

    private IEnumerator FadeIntoStage()
    {
        blackScreenImageObject.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        playStage = (int) PlayScene.ChangeScene;
    }

    private IEnumerator EnablePlaySceneAnim(int playstage)
    {
        yield return new WaitForSeconds(changeStageTime);
        EnableStageProp((PlayScene) playstage);
        yield return null;
    }
}