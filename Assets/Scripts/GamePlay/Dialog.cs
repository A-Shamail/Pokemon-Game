using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    // Start is called before the first frame update
    [SerializeField] List<string> lines;

    // public void AddLine(string line)
    // {
    //     lines.Add(line);
    // }

    // public void AddLines(List<string> lines)
    // {
    //     lines.AddRange(lines);
    // }

    // public void ClearLines()
    // {
    //     lines.Clear();
    // }

    public List<string> Lines
    {
        get { return lines; }
    } 
}
