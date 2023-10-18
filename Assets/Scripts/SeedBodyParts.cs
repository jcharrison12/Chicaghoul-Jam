using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBodyParts : MonoBehaviour
{
    private List<BodyPart> graveyardList = new List<BodyPart>();
    public List<BodyPart> bpOptions;
    public List<GameObject> gravePlots;
    public int numPartTypes = 5;
    public int numGraves;
    // Start is called before the first frame update
    void Awake()
    {
        numGraves = gravePlots.Count;
        for (int i = 0; i < numPartTypes; i++)
        {
            BodyPart.PartOption tempEnum = (BodyPart.PartOption)i;
            BodyPart temp = ScriptableObject.CreateInstance<BodyPart>();
            List<BodyPart> results = bpOptions.FindAll(
              delegate (BodyPart bp)
              {
                  return bp.part == tempEnum;
              }
              );
            temp = results[Random.Range(0, results.Count)];
            graveyardList.Add(temp);
        }
        for(int i = 0; i < numGraves - numPartTypes; i++)
        {
            graveyardList.Add(bpOptions[Random.Range(0, bpOptions.Count)]);
        }
        for(int i = 0; i < numGraves; i++)
        {
            gravePlots[i].AddComponent<BodyPartInstance>();
            var temp = graveyardList[Random.Range(0, graveyardList.Count)];
            gravePlots[i].GetComponent<BodyPartInstance>().bpType = temp;
            graveyardList.Remove(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
