using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    public List<BodyPart> bodyParts = new List<BodyPart>();

    public void AddItem(BodyPart bpToAdd)
    {
        bodyParts.Add(bpToAdd);
    }
}
