using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] GameObject DialogText;
    [SerializeField] Canvas[] StageProp;
    [SerializeField] Animator[] StagePropAnim;


    private static int playstage;

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
        ChangeScene
    }
    void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        for (int i = 0; i < StageProp.Length; i++)
        {
            StageProp[i].enabled= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        playstage = DialogText.GetComponent<TypeWriterEffect>().PlayScene;
        if (playstage != (int)PlayScene.ChangeScene ){
            StopCoroutine((EnablePlaySceneAnim(playstage)));
            StartCoroutine(EnablePlaySceneAnim(playstage));
            Debug.Log(playstage);
        }
        else if(playstage ==(int) PlayScene.ChangeScene)
        {
            SceneManager.LoadScene("Stage");
        }
    }

    IEnumerator EnablePlaySceneAnim(int playstage)
    {
        yield return new WaitForSeconds(1);
        EnableStageProp((PlayScene) playstage);
    }
    void EnableStageProp(PlayScene stage)
    {
        int StageNum = (int) stage;
        if (StageProp[StageNum] == null) return;
        StageProp[StageNum].enabled = false;
         StageProp[StageNum].enabled = true;

        for (int i = 0; i < StageProp.Length; i++)
        {
            if (i != StageNum)
                StageProp[i].enabled = false;
        }
    }
}