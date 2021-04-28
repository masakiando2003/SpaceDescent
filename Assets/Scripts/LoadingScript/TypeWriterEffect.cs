using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{

    [SerializeField] float WaitNextText = 2f;
    [SerializeField] string[] fullText;

    private float delay = 0.1f;
    private string currentText = "";
    private int mplayScene = 0;

    public int PlayScene { get=> mplayScene;} 
    void Start()
    {
        StartCoroutine(ShowText());
    }
    
    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++ )
        {
            for (int j = 0; j <= fullText[i].Length; j++)
            {
                currentText = fullText[i].Substring(0, j);
                this.GetComponent<Text>().text = currentText;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(WaitNextText);
            mplayScene++;
        }
    }
}
