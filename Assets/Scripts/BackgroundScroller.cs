using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = -0.5f;
    [SerializeField] float boostSpeedFactor = 1.5f;

    Material myMaterial;
    Vector2 offset;
    float speedFactor, normalSpeedFactor;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
        normalSpeedFactor = 1f;
        speedFactor = normalSpeedFactor;
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime * speedFactor;
    }

    public void BoostScrollSpeed()
    {
        speedFactor = boostSpeedFactor;
    }
    public void NormalScrollSpeed()
    {
        speedFactor = normalSpeedFactor;
    }
}
