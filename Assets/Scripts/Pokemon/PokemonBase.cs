using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject // the following contains the basic information for a Pokemon like type, stats, etc
{
    [SerializeField] string name; // need to use var outside class so expose them using properties

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves; // array with predefined functions


    public string GetName()
    {
        return name;
    }

    public string Name
    { // can use properties as variables now
        get
        {
            return name;
        } // getter for name
    }

    public string Description
    {
        get
        {
            return description;
        }
    } // getter for description

    public Sprite FrontSprite
    {
        get
        {
            return frontSprite;
        }
    } // getter for front sprite

    public Sprite BackSprite
    {
        get
        {
            return backSprite;
        }
    } // getter for back sprite

    public PokemonType Type1
    {
        get
        {
            return type1;
        }
    } // getter for type 1

    public PokemonType Type2
    {
        get
        {
            return type2;
        }
    } // getter for type 2

    public int MaxHP
    {
        get
        {
            return maxHP;
        }
    } // getter for max hp

    public int Attack
    {
        get
        {
            return attack;
        }
    } // getter for attack

    public int Defense
    {
        get
        {
            return defense;
        }
    } // getter for defense

    public int SpAttack
    {
        get
        {
            return spAttack;
        }
    } // getter for sp attack

    public int SpDefense
    {
        get
        {
            return spDefense;
        }
    } // getter for sp defense

    public int Speed
    {
        get
        {
            return speed;
        }
    } // getter for speed

    public List<LearnableMove> LearnableMoves
    {
        get
        {
            return learnableMoves;
        }
    } // getter for learnable moves


}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get
        {
            return moveBase;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
    }


}

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon,
    Fairy,
    Dark,
    Steel
}

public class TypeChart
{
    static float[][] chart =
    {
       
        //          Normal   Fire     Water    Electric  Grass    Ice      Fighting  Poison   Ground   Flying   Psychic  Bug     Rock    Ghost    Dragon   Fairy    Dark    Steel
        // Normal
        new float[] { 1f,     1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      0.5f,    0f,      1f,      1f,      1f,      0.5f },  
        // Fire
        new float[] { 1f,     0.5f,    0.5f,    1f,      2f,      2f,      1f,      1f,      1f,      1f,      1f,      2f,      0.5f,    1f,      0.5f,    1f,      1f,      2f },  
        // Water
        new float[] { 1f,     2f,      0.5f,    1f,      0.5f,    1f,      1f,      1f,      2f,      1f,      1f,      1f,      2f,      1f,      0.5f,    1f,      1f,      0.5f },  
        // Electric
        new float[] { 1f,     1f,      2f,      0.5f,    0.5f,    1f,      1f,      1f,      0f,      2f,      1f,      1f,      1f,      1f,      0.5f,    1f,      1f,      1f },  
        // Grass
        new float[] { 1f,     0.5f,    2f,      1f,      0.5f,    1f,      1f,      0.5f,    2f,      0.5f,    1f,      0.5f,    2f,      1f,      1f,      1f,      1f,      0.5f },  
        // Ice
        new float[] { 1f,     0.5f,    0.5f,    1f,      2f,      0.5f,    1f,      1f,      2f,      2f,      1f,      1f,      1f,      1f,      2f,      1f,      1f,      0.5f },  
        // Fighting
        new float[] { 2f,     1f,      1f,      1f,      1f,      2f,      1f,      0.5f,    1f,      0.5f,    0.5f,    0.5f,    0.5f,    0f,      1f,      1f,      2f,      2f },  
        // Poison
        new float[] { 1f,     1f,      1f,      1f,      2f,      1f,      1f,      0.5f,    0.5f,    1f,      1f,      1f,      0.5f,    0.5f,    1f,      1f,      1f,      0f },  
        // Ground
        new float[] { 1f,     2f,      1f,      2f,      0.5f,    1f,      1f,      2f,      1f,      0.5f,    1f,      2f,      0.5f,    2f,      1f,      1f,      1f,      2f },  
        // Flying
        new float[] { 1f,     1f,      1f,      0.5f,    2f,      1f,      2f,      1f,      1f,      1f,      1f,      0.5f,    2f,      1f,      1f,      1f,      2f,      0.5f },  
        // Psychic
        new float[] { 1f,     1f,      1f,      1f,      1f,      1f,      2f,      2f,      1f,      1f,      0.5f,    1f,      0.5f,    1f,      1f,      1f,      1f,      0.5f },  
        // Bug
        new float[] { 1f,     0.5f,    1f,      1f,      2f,      1f,      2f,      0.5f,    0.5f,    0.5f,    2f,      1f,      1f,      1f,      0.5f,    1f,      2f,      0.5f },  
        // Rock
        new float[] { 1f,     0.5f,    1f,      1f,      1f,      2f,      1f,      0.5f,    2f,      0.5f,    1f,      1f,      2f,      1f,      1f,      1f,      1f,      0.5f },  
        // Ghost
        new float[] { 0f,     1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      2f,      1f,      0.5f,    0.5f },  
        // Dragon
        new float[] { 1f,     0.5f,    0.5f,    0.5f,    0.5f,    2f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      1f,      2f,      2f,      1f,      0.5f },  
        // Fairy
        new float[] { 1f,     1f,      1f,      1f,      1f,      1f,      2f,      2f,      1f,      1f,      0.5f,    1f,      0.5f,    1f,      1f,      2f,      2f,      0.5f },  
        // Dark
        new float[] { 1f,     1f,      1f,      1f,      1f,      1f,      0.5f,    1f,      1f,      1f,      2f,      1f,      1f,      1f,      1f,      1f,      0.5f,    1f },  
        // Steel
        new float[] { 1f,     0.5f,      1f,      1f,      0.5f,    0.5f,    2f,      1f,      1f,      1f,      1f,      0.5f,    2f,      1f,      1f,      2f,      1f,      0.5f }
    };

    public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
    {
        if (attackType == PokemonType.None || defenseType == PokemonType.None)
        {
            return 1;
        }
        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}