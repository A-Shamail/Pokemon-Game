using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyScreen : MonoBehaviour
{
    [SerializeField] Text messageText;
    PartyMemberUI[] partyMembers;
    List<Pokemon> pokemon;

    int selection = 0;

    public void Init()
    {
        partyMembers = (GetComponentsInChildren<PartyMemberUI>());

        // for (int i = 0; i < partyMembers.Length; i++)
        // {
        //     partyMembers[i].Init();
        // }
    }

    public void SetMessageText(string message)
    {
        messageText.text = message;
    }

    // public void UpdateMemberSelection(int selectedMember)
    // {
    //     for (int i = 0; i < partyMembers.Count; i++)
    //     {
    //         if (i != selectedMember)
    //         {
    //             partyMembers[i].SetSelected(false);
    //         }
    //         else
    //         {
    //             partyMembers[i].SetSelected(true);
    //             selection = i;
    //         }
    //     }
    // }

    // public void HandleUpdate(Action onSelected, Action onBack)
    // {
    //     if (Input.GetKeyDown(KeyCode.RightArrow))
    //     {
    //         UpdateMemberSelection(selection + 1);
    //     }
    //     else if (Input.GetKeyDown(KeyCode.LeftArrow))
    //     {
    //         UpdateMemberSelection(selection - 1);
    //     }

    //     if (Input.GetKeyDown(KeyCode.Z))
    //     {
    //         onSelected();
    //     }
    //     else if (Input.GetKeyDown(KeyCode.X))
    //     {
    //         onBack();
    //     }
    // }

    public void UpdateMemberSelection(int selectedMember) 
    {

        for(int i = 0; i < pokemon.Count; i++)
        {
            if(i == selectedMember)
            {
                partyMembers[i].SetSelected(true);
            }
            else
            {
                partyMembers[i].SetSelected(false);
            }
        }

    }

    public void SetPartyData(List<Pokemon> pokemon)
    {
        this.pokemon = pokemon;
        for (int i = 0; i < partyMembers.Length; i++)
        {
            if (i < pokemon.Count)
            {
                partyMembers[i].SetData(pokemon[i]);
            }
            else
            {
                partyMembers[i].gameObject.SetActive(false);
            }
        }
        messageText.text = "Choose a Pokemon";
    }
}
