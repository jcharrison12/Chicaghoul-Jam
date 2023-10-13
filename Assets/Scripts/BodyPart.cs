using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BodyPart: ScriptableObject
{
    [SerializeField]
    public enum PartOption { brain, face, arm, body, leg }
    public PartOption part;
    public int quality;
    public Sprite icon;
    [TextArea]
    public string epitaph;
   
}
