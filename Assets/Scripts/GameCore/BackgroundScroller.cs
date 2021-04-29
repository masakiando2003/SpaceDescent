using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = -1f;

    Material myMaterial;
    Vector2 offset;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    // Update is called once per frame 
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
        if(anim == null) { return; }
        anim.speed = backgroundScrollSpeed;
    }

    public void SetStage2ToTrue()
    {
        gameObject.GetComponent<Animator>().SetBool("IsStage2", true);
    }

    public void SetStage2ToFalse()
    {
        gameObject.GetComponent<Animator>().SetBool("IsStage2", false);
    }
}
