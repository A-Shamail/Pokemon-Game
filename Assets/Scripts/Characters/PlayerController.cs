using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public LayerMask interactableLayer;
    public LayerMask solidObjectsLayer;
    public LayerMask grassLayer;
    public LayerMask portalLayer;


    public LayerMask TriggerableLayer{
        get => grassLayer | portalLayer;
    }

    private bool isMoving;
    private Vector2 input;

    private Animator animator;
    public event Action OnEncountered;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if (!isMoving && !PortalManager.IsTransitioning)
        {

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // cannot move diagonally
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }

        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckIfInteractable();
        }
    }

    IEnumerator Move(Vector3 targetPos) // a coroutine that gives position over a period of time. If a difference occurs, then move towards target by a small aount
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null; // stops execution of coroutine until next frame
        }
        transform.position = targetPos;

        isMoving = false;

        var trigs = Physics2D.OverlapCircleAll(transform.position, 0.2f, TriggerableLayer);
        foreach (var trig in trigs)
        {
            // print(trig);
            var triggerable = trig.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                animator.SetBool("isMoving", false);
                triggerable.onPlayerTriggered(this);
                break;
            }
        }
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }

        if (Physics2D.OverlapCircle(targetPos, 0.2f, interactableLayer) != null)
        {
            return false;
        }
        return true;
    }

    // private void CheckForEncounters()
    // {
    //     // Debug.Log("Checking for encounters");
    //     if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
    //     {
    //         if (UnityEngine.Random.Range(1, 101) <= 10)
    //         {
    //             animator.SetBool("isMoving", false);
                // OnEncountered();
    //         }
    //     }
    // }

    private void CheckIfInteractable()
    {
        // first get the direction which player is facing
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));

        if (Physics2D.OverlapCircle(facingDir + transform.position, 0.2f, interactableLayer) != null)
        {
            Debug.Log("Interactable object found");
            (Physics2D.OverlapCircle(facingDir + transform.position, 0.2f, interactableLayer).GetComponent<Interactable>() as Interactable).Interact();
        }
    }

    // public Character Character => character;
}
