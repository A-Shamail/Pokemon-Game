using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Roaming,
    Battle,
    Dialog
}
public class GameController : MonoBehaviour
{

    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera mainCamera;



    GameState state;

    private void Start()
    {
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;

        DialogManager.Instance.OnShowDialog += OnShowDialogHandler;
        DialogManager.Instance.OnCloseDialog += OnCloseDialogHandler;
    }

    void OnShowDialogHandler()
    {
        state = GameState.Dialog;
    }

    void OnCloseDialogHandler()
    {
        if (state == GameState.Dialog)
        {
            state = GameState.Roaming;
        }
    }

    public void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);


        var playerParty = playerController.GetComponent<PokemonParty>();
        var wildPokemon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildPokemon();

        battleSystem.StartBattle(playerParty, wildPokemon);

    }

    void EndBattle(bool winOrLose)
    {
        state = GameState.Roaming;
        battleSystem.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        if (winOrLose)
        {
            ;
        }
        else
        {
            ;
        }
    }

    private void Update() // responsible for giving states/ controlling active scenes
    {
        if (state == GameState.Roaming)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }

}
