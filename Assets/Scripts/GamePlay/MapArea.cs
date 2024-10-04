using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Pokemon> wildPokemon;

    public Pokemon GetRandomWildPokemon()
    {
        int r = Random.Range(0, wildPokemon.Count);
        wildPokemon[r].Init();

        return wildPokemon[r];

    }

    
}
