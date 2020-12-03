using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//this class is use to edit the name and sentences of an npc
public class Dialogue
{
    [TextArea(3,10)]
    public string[] sentences;
    public string name;
}
