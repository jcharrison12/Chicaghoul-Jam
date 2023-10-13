using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{ 
    public List<BodyPartInstance> bodyParts = new();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
        public void AddItem(BodyPartInstance bpToAdd)
    {
        bodyParts.Add(bpToAdd);
    }
}
