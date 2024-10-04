using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    
    public Text TextField
    {
        get { return text; }
    }
}
