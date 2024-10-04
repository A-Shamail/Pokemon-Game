using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Pokemon // this script adjusts the max stats based on the Level of the Pokemon
{
    public int HP { get; set; }

    [SerializeField] PokemonBase _base;
    [SerializeField] int level;

    public PokemonBase Base { get {
            return _base;
        } 
     } // reference to base class


    
    public int Level { get {
            return level;
        }
    }

    public List<Move> Moves { get; set; } // list of moves

    public void Init()
    {
        // Base = pBase;
        // Level = pLevel;
        Moves = new List<Move>();

        //iterate through moves list and add it to  moves depending on Level

        for (int i = 0; i < Base.LearnableMoves.Count; i++)
        {
            if (Base.LearnableMoves[i].Level <= Level)
            {
                Moves.Add(new Move(Base.LearnableMoves[i].Base));
            }

            if (Moves.Count >= 4)
            {
                break;
            }
        }

        HP = MaxHP;

    }

    public int Attack
    {
        get
        {
            return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5;
        }
    }

    public int Defense
    {
        get
        {
            return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5;
        }
    }

    public int SpAttack
    {
        get
        {
            return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5;
        }
    }

    public int SpDefense
    {
        get
        {
            return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5;
        }
    }

    public int Speed
    {
        get
        {
            return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5;
        }
    }

    public int MaxHP
    {
        get
        {
            return Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10 + Level;
        }
    }

    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {
        // if (Random.Range(0, 1f) > move.Base.Accuracy)
        // {
        //     return false;
        // }

        float Critical2 = 1f;
        if (Random.Range(0, 1f) < 0.0625f)
        {
            Critical2 = 2f;
        }

        var DamageDetails = new DamageDetails()
        {
            TypeEffectiveness = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2),
            Critical = Critical2
        };


        int damage = Mathf.FloorToInt((((2 * attacker.Level + 10) / 250f) * Random.Range(0.85f, 1f) * move.Base.Power * ((float)attacker.Attack / Defense) + 2) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2) * Mathf.FloorToInt(Critical2));

        HP = HP - damage;

        if (HP <= 0)
        {
            HP = 0;
            DamageDetails.Fainted = true;
        }

        return DamageDetails;
        

    }

    public Move GetEnemyMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

}

public class DamageDetails
{
    public bool Fainted { get; set; } = false;
    public float Critical { get; set; }
    public float TypeEffectiveness { get; set; }

}
