using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Pokemon> pokemon;

    public List<Pokemon> Pokemons { get => pokemon; }

    private void Start()
    {
        // Debug.Log(GetRandomWildPokemon().Base.Name);

        for (int i = 0; i < pokemon.Count; i++)
        {
            pokemon[i].Init();
        }

        
    }

    public Pokemon GetHealthyPokemon()
    {
        for (int i = 0; i < pokemon.Count; i++)
        {
            if (pokemon[i].HP > 0)
            {
                return pokemon[i];
            }
        }

        return null;
    }
}
