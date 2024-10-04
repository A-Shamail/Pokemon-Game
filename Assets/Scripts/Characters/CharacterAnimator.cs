using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkLeftSprites;
    [SerializeField] List<Sprite> walkRightSprites;

    SpriteRenderer spriteRenderer;

    
    SpriteAnimator walkDownAnimator;
    SpriteAnimator walkUpAnimator;
    SpriteAnimator walkLeftAnimator;
    SpriteAnimator walkRightAnimator;

    public float MoveX { get; set; }
    public float MoveY { get; set; }

    public bool IsMoving { get; set; }

    public bool WasMoving;

    SpriteAnimator currentAnimator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        walkDownAnimator = new SpriteAnimator(spriteRenderer, walkDownSprites);
        walkUpAnimator = new SpriteAnimator(spriteRenderer, walkUpSprites);
        walkLeftAnimator = new SpriteAnimator(spriteRenderer, walkLeftSprites);
        walkRightAnimator = new SpriteAnimator(spriteRenderer, walkRightSprites);

        currentAnimator = walkDownAnimator;
    }

    private void Update()
    {

        var prevAnimator = currentAnimator;


        if(MoveX ==  1)
        {
            currentAnimator = walkRightAnimator;
        }
        else if(MoveX == -1)
        {
            currentAnimator = walkLeftAnimator;
        }
        else if(MoveY == 1)
        {
            currentAnimator = walkUpAnimator;
        }
        else if(MoveY == -1)
        {
            currentAnimator = walkDownAnimator;
        }

        if(currentAnimator != prevAnimator || WasMoving == IsMoving)
        {
            currentAnimator.Start();
        }

        if(IsMoving  )
        {
            currentAnimator.HandleUpdate();
        }
        else
        {
            spriteRenderer.sprite = currentAnimator.Frames[0];
        }

        WasMoving = IsMoving;
    }



    

}
