using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    idle,
    walk,
    interact
}

public class NPCController : MonoBehaviour, Interactable
{
    // Start is called before the first frame update

    [SerializeField] Dialog dialog;
    [SerializeField] List<Vector2> movementPattern;
    [SerializeField] float timeBetweenPattern = 2f;

    public Character character;
    public int currentPattern = 0;

    CharacterState state;
    float stateTime =  0f;

    
    // [SerializeField] Choices choiceText;

    private void Awake()
    {
        character = GetComponent<Character>();
    }
    public void Interact()
    {
        Debug.Log("Started Dialogue");

        // StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        if(state == CharacterState.idle)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        }
        // StartCoroutine(character.Move(new Vector2(-1, 0)));
    }

    private void Update()
    {
        
        if(DialogManager.Instance.isShowing)
        {
            return;
        }

        if(state == CharacterState.idle)
        {
            stateTime += Time.deltaTime;
            if(stateTime > timeBetweenPattern)
            {
                stateTime = 0f;
                if(movementPattern.Count > 0)
                {
                    StartCoroutine(Walk());
                }
                
            }
        }
        

        character.HandleUpdate();
    }

    IEnumerator Walk()
    {
        state = CharacterState.walk;
        yield return character.Move(movementPattern[currentPattern]);
        state = CharacterState.idle;
        currentPattern = (currentPattern + 1) % movementPattern.Count;
    }
}
