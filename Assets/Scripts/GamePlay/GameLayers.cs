using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask solidObjectsLayer;
    [SerializeField] LayerMask grassLayer;

    [SerializeField] LayerMask playerLayer;

    [SerializeField] LayerMask fovLayer;

    public static GameLayers Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public LayerMask InteractableLayer { get => interactableLayer; }
    public LayerMask SolidObjectsLayer { get => solidObjectsLayer; }
    public LayerMask GrassLayer { get => grassLayer; }

    public LayerMask PlayerLayer { get => playerLayer; }

    public LayerMask FovLayer { get => fovLayer; }
}
