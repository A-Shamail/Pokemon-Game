using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] List<Choices> choices;
    [SerializeField] Choices choiceText;
    // [SerializeField] int currentChoice = 0;
    // [SerializeField] float inputDelay = 0.25f;
    // float lastInputTime = 0f;
    bool canInput = true;

    // List<ChoiceText> ChoiceTexts;

    public IEnumerator ShowChoices(List<string> choices)
    {
        canInput = false;
        gameObject.SetActive(true);

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // choiceTexts = new List<choiceTexts>();
        foreach (var choice in choices)
        {
            var choiceTemp = Instantiate(choiceText, transform);
            choiceTemp.TextField.text = choice;
        }


        yield return new WaitUntil(() => canInput == true);

    }

    // public void HideChoices()
    // {
    //     gameObject.SetActive(false);
    // }

    // public void HandleUpdate()
    // {
    //     if (Time.time - lastInputTime > inputDelay)
    //     {
    //         canInput = true;
    //     }

    //     if (canInput)
    //     {
    //         if (Input.GetKeyDown(KeyCode.DownArrow))
    //         {
    //             lastInputTime = Time.time;
    //             canInput = false;
    //             currentChoice++;
    //             if (currentChoice >= choices.Count)
    //             {
    //                 currentChoice = 0;
    //             }
    //         }
    //         else if (Input.GetKeyDown(KeyCode.UpArrow))
    //         {
    //             lastInputTime = Time.time;
    //             canInput = false;
    //             currentChoice--;
    //             if (currentChoice < 0)
    //             {
    //                 currentChoice = choices.Count - 1;
    //             }
    //         }
    //     }

    //     for (int i = 0; i < choices.Count; i++)
    //     {
    //         if (i == currentChoice)
    //         {
    //             choices[i].TextField.color = Color.black;
    //         }
    //         else
    //         {
    //             choices[i].TextField.color = Color.white;
    //         }
    //     }

    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         DialogManager.Instance.DialogBox.ChoiceMade(currentChoice);
    //         HideChoices();
    //     }
    // }
}
