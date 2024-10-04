using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class LongGrass : MonoBehaviour, IPlayerTriggerable
// {
//     public void onPlayerTriggered(PlayerController player)
//     {
//         if (UnityEngine.Random.Range(1, 101) <= 10)
//         {
//             GameController.StartBattle();
//         }
//     }
// }

public class LongGrass : MonoBehaviour, IPlayerTriggerable
{
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void onPlayerTriggered(PlayerController player)
    {
        if (UnityEngine.Random.Range(1, 101) <= 10)
        {
            gameController.StartBattle();
        }
    }
}