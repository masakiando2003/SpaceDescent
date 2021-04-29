using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleXbutton : MonoBehaviour
{
    [SerializeField] private GameObject creditbox;
    // Start is called before the first frame update
    private void ShowCredit()
    {
        creditbox.SetActive(true);
    }

    private void CloseCredit()
    {
        creditbox.SetActive(false);

    }

    public void Toggle()
    {
        if(creditbox.activeSelf == false)
            creditbox.SetActive(true);
            else
            {
                creditbox.SetActive(false);
            }
    }
}
