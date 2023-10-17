using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Jobs : ScriptableObject
{
    
    public string title;
    [TextArea]
    public string description;
    public List<int> mins = new List<int>();

}
