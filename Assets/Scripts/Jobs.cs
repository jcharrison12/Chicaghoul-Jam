using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Jobs : ScriptableObject
{
    public int id;
    public string title;
    [TextArea]
    public string description;
    public int brainMin;
    public int faceMin;
    public int armMin;
    public int bodyMin;
    public int legMin;

}
