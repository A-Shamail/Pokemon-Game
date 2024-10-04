using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{

    private CharacterAnimator animator;
    public float moveSpeed;

    public bool IsMoving { get; private set; }

    private Vector2 input;

    
    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
    }

    
    public IEnumerator Move(Vector2 moveVec, Action onMoveOver =  null) // a coroutine that gives position over a period of time. If a difference occurs, then move towards target by a small aount
    {
        

        input.x = moveVec.x;
        input.y = moveVec.y;

        animator.MoveX = Mathf.Clamp(input.x, -1f, 1f);
        animator.MoveY = Mathf.Clamp(input.y, -1f, 1f);

        var targetPos = transform.position;
        targetPos.x += input.x;
        targetPos.y += input.y;

        if (IsWalkable(targetPos))
        {
            IsMoving = true;
            while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null; // stops execution of coroutine until next frame
            }
            transform.position = targetPos;
            IsMoving = false;

            onMoveOver?.Invoke();

        }
        else
        {
            yield break;
        }
    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

     private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.SolidObjectsLayer) != null)
        {
            return false;
        }

        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.Instance.InteractableLayer) != null)
        {
            return false;
        }


        return true;
    }

    
}
