using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{ 
    public List<BodyPart> bodyParts = new List<BodyPart>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void AddItem(BodyPart bpToAdd)
    {
        bodyParts.Add(bpToAdd);
    }
}
