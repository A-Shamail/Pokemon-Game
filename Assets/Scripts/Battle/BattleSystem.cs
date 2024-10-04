using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BattleState
{
    Start,
    PlayerAction,
    PlayerMove,
    EnemyMove,
    Busy,
   ChoosingANewPokemon,
    BattleOver
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] PartyScreen partyScreen;
    [SerializeField] BattleDialogueBox dialogueBox;

    [SerializeField] AudioClip battleMusic;

    // [serializeField] AudioClip gameOverMusic;
    [SerializeField] AudioClip victoryMusic;


    public event Action<bool> OnBattleOver;


    BattleState state;
    int currentAction;
    int currentMove;
    int currentPokemon;

    PokemonParty playerParty;
    Pokemon wildPokemon;


    public void StartBattle(PokemonParty playersParty, Pokemon wildPokemon1)
    {
        playerParty = playersParty;
        wildPokemon = wildPokemon1;
        AudioManager.Instance.PlayMusic(battleMusic, true, false);
        StartCoroutine(SetupBattle(playerParty, wildPokemon));

    }

    public IEnumerator SetupBattle(PokemonParty playersParty, Pokemon wildPokemon1)
    {
        partyScreen.Init();
        playerUnit.Setup(playerParty.GetHealthyPokemon());
        enemyUnit.Setup(wildPokemon);

        playerHud.SetData(playerUnit.Pokemon);
        enemyHud.SetData(enemyUnit.Pokemon);

        dialogueBox.SetMoveNames(playerUnit.Pokemon.Moves);

        yield return dialogueBox.TypeDialogue("A wild " + enemyUnit.Pokemon.Base.Name + " appeared.");
        yield return new WaitForSeconds(1f);

        PlayerAction();

    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogueBox.TypeDialogue("Choose an action."));
        dialogueBox.EnableActionSelector(true);
    }

    void OpenPartyScreen()
    {

        state = BattleState.ChoosingANewPokemon;
        partyScreen.SetPartyData(playerParty.Pokemons);
        partyScreen.gameObject.SetActive(true);
        // dialogueBox.EnableActionSelector(false);
        // dialogueBox.EnableDialogueText(false);
        // dialogueBox.EnableMoveSelector(false);
        // console.log("OpenPartyScreen");
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelector(true);

    }

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
        else if (state == BattleState.ChoosingANewPokemon)
        {
            HandlePartySelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
            {
                currentAction++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
            {
                currentAction--;
            }
        }

        dialogueBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.Instance.PlaySoundEffect(CommonAudios.Select);
            if (currentAction == 0)
            {
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                OpenPartyScreen();
            }
            else if (currentAction == 2)
            {
                //implement Run functionality
                // yield return dialogueBox.TypeDialogue("You got away safely.");
                OnBattleOver(true);
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 1)
            {
                currentMove++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
            {
                currentMove--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 2)
            {
                currentAction++;
                currentAction++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 1)
            {
                currentAction--;
                currentAction--;
            }
        }
    
        dialogueBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.Instance.PlaySoundEffect(CommonAudios.Select);
            dialogueBox.EnableMoveSelector(false);
            dialogueBox.EnableDialogueText(true);
            StartCoroutine(PlayerMoveTurn());
            // yield return new WaitForSeconds(1f);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            dialogueBox.EnableMoveSelector(false);
            dialogueBox.EnableDialogueText(true);
            PlayerAction();
        }

    }

    IEnumerator PlayerMoveTurn()
    {
        AudioManager.Instance.PlaySoundEffect(CommonAudios.Select);
        state = BattleState.Busy;
        var move = playerUnit.Pokemon.Moves[currentMove];
        yield return dialogueBox.TypeDialogue(playerUnit.Pokemon.Base.Name + " used " + move.Base.MoveName);
        yield return new WaitForSeconds(1f);


        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);
        enemyUnit.PlayHitAnimation();
        AudioManager.Instance.PlaySoundEffect(CommonAudios.Hit);

        var isFainted = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(isFainted);
        if (isFainted.Fainted)
        {
            yield return dialogueBox.TypeDialogue(enemyUnit.Pokemon.Base.Name + " fainted.");
            enemyUnit.PlayFaintAnimation();
            AudioManager.Instance.PlaySoundEffect(CommonAudios.Faint);
            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
        }
        else
        {
            StartCoroutine(EnemyMoveTurn());
        }

    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
        {
            yield return dialogueBox.TypeDialogue("A critical hit!");
        }
        if (damageDetails.TypeEffectiveness > 1f)
        {
            yield return dialogueBox.TypeDialogue("It's super effective!");
        }
        else if (damageDetails.TypeEffectiveness < 1f)
        {
            yield return dialogueBox.TypeDialogue("It's not very effective!");
        }
    }

    IEnumerator EnemyMoveTurn()
    {
        state = BattleState.EnemyMove;
        var move = enemyUnit.Pokemon.GetEnemyMove();
        yield return dialogueBox.TypeDialogue(enemyUnit.Pokemon.Base.Name + " used " + move.Base.MoveName);
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);
        playerUnit.PlayHitAnimation();
        AudioManager.Instance.PlaySoundEffect(CommonAudios.Hit);

        var isFainted = playerUnit.Pokemon.TakeDamage(move, enemyUnit.Pokemon);
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(isFainted);
        if (isFainted.Fainted)
        {
            yield return dialogueBox.TypeDialogue(playerUnit.Pokemon.Base.Name + " fainted.");
            playerUnit.PlayFaintAnimation();
            AudioManager.Instance.PlaySoundEffect(CommonAudios.Faint);
            yield return new WaitForSeconds(2f);

            var nextPokemon = playerParty.GetHealthyPokemon();
            if (nextPokemon != null)
            {
                playerUnit.Setup(nextPokemon);
                playerHud.SetData(nextPokemon);
                dialogueBox.SetMoveNames(nextPokemon.Moves);
                
                yield return dialogueBox.TypeDialogue("Go " + nextPokemon.Base.Name + "!");
            }
            else
            {
                OnBattleOver(false);
            }
            // OnBattleOver(false);

        }
        else
        {
            PlayerAction();
        }
    }

    void HandlePartySelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentPokemon++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentPokemon--;
        }

        if (currentPokemon < 0)
        {
            currentPokemon = 0;
        }
        else if (currentPokemon > playerParty.Pokemons.Count - 1)
        {
            currentPokemon = playerParty.Pokemons.Count - 1;
        }
        
        partyScreen.UpdateMemberSelection(currentPokemon);

        if(Input.GetKeyDown(KeyCode.Z))
        {
            var selectedMember = playerParty.Pokemons[currentPokemon];
            if(selectedMember.HP <= 0)
            {
                partyScreen.SetMessageText("You can't send out a fainted Pokemon.");
                return;
            }
            if(selectedMember == playerUnit.Pokemon)
            {
                partyScreen.SetMessageText("You can't switch with the same Pokemon.");
                return;
            }
            partyScreen.gameObject.SetActive(false);
            dialogueBox.EnableActionSelector(true);
            state = BattleState.Busy;
            StartCoroutine(SwitchPokemon(selectedMember));
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            partyScreen.gameObject.SetActive(false);
            dialogueBox.EnableActionSelector(true);
            state = BattleState.Busy;
            // StartCoroutine(SwitchPokemon(playerUnit.Pokemon));
        }
    }

    IEnumerator SwitchPokemon(Pokemon newPokemon)
    {
        if(playerUnit.Pokemon.HP > 0)
        {
            yield return dialogueBox.TypeDialogue("Return " + playerUnit.Pokemon.Base.Name + "!");
            playerUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);
        }
        playerUnit.Setup(newPokemon);
        playerHud.SetData(newPokemon);
        dialogueBox.SetMoveNames(newPokemon.Moves);

        yield return dialogueBox.TypeDialogue("Go " + newPokemon.Base.Name + "!");
        state = BattleState.EnemyMove;
        StartCoroutine(EnemyMoveTurn());
    }


}
