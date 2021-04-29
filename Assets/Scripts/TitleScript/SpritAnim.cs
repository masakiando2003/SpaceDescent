using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SpritAnim : MonoBehaviour
{
    public float _speed = 1f;
    public int _FrameRate = 30;
    public bool _Loop = false;
    public Sprite[] _Sprites;
    
    private float mTimePerFrame = 0f;
    private float mElapsedTime = 0f;
    private int mCurrentFrame = 0;
    private Image mImage;

    // Start is called before the first frame update
    void Start()
    {
        mImage = GetComponent<Image>();
        LoadSpriteSHeet();
    }

    private void LoadSpriteSHeet()
    {
        if (_Sprites != null && _Sprites.Length > 0)
        {
            mTimePerFrame = 1f / _FrameRate;
            Play();
        }else
            Debug.Log("Failed to load sprite sheet");
    }

    private void Play()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        mElapsedTime += Time.deltaTime * _speed;
        if (mElapsedTime >= mTimePerFrame)
        {
            mElapsedTime = 0f;
            ++mCurrentFrame;
            Setsprite();
            if (mCurrentFrame >= _Sprites.Length)
            {
                if (_Loop) mCurrentFrame = 0;
                    else
                enabled = false;
            }
        }
    }

    private void Setsprite()
    {
        if (mCurrentFrame >= 0 && mCurrentFrame < _Sprites.Length)
            mImage.sprite = _Sprites[mCurrentFrame];
    }
}
