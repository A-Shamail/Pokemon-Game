using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public MoveBase Base { get; set; } // creates private variable behind the scene. Used when dont want variables shpwn in inspector

    public int PP { get; set; }

    public Move(MoveBase pBase)
    {
        Base = pBase;
        PP = pBase.PP;
    }

    public bool Use()
    {
        if(PP <= 0)
        {
            return false;
        }

        PP--;
        return true;
    }
}
