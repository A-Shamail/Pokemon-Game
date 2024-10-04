using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    List<Sprite> frames;
    float frameRate;

    int currentFrame;
    float frameTimer;

    public List<Sprite> Frames { get => frames; set => frames = value; }

    public SpriteAnimator(SpriteRenderer spriteRenderer, List<Sprite> frames, float frameRate = 0.16f)
    {
        this.spriteRenderer = spriteRenderer;
        this.frames = frames;
        this.frameRate = frameRate;
    }

    public void Start()
    {
        frameTimer = 0f;
        currentFrame = 0;
        spriteRenderer.sprite = frames[0];
    }

    public void HandleUpdate()
    {
        frameTimer += Time.deltaTime;
        if (frameTimer >= frameRate)
        {
            frameTimer -= frameRate;
            currentFrame = (currentFrame + 1) % frames.Count;
            spriteRenderer.sprite = frames[currentFrame];
        }
    }



}
