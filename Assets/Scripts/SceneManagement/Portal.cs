using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Portal : MonoBehaviour, IPlayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] DestinationIdentifier destination;
    [SerializeField] Transform spawnPoint;

    PlayerController player;
    public void onPlayerTriggered(PlayerController player)
    {
        this.player = player;
        PortalManager.NextSpawnPoint = spawnPoint.position;
        PortalManager.IsTransitioning = true;
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        var destPortal = FindObjectsOfType<Portal>().First(x => x != this && x.destination == this.destination);
        player.transform.position = destPortal.SpawnPoint.position;
        PortalManager.IsTransitioning = false;
        
        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;
}

public enum DestinationIdentifier
{
    A, B, C, D, E
}




// public static class PortalManager
// {
//     public static Vector3? NextSpawnPoint;
//     public static bool IsTransitioning;
// }
