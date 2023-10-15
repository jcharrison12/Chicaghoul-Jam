using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeSelection : MonoBehaviour
{
    private List<BodyPart> inventoryList = new List<BodyPart>();
    public List<BodyPart> bpOptions;
    public GameObject computerGrid;
    public TriggerComputer computerUI;
    public int activeCellNum = 0;

    // Start is called before the first frame update
    void Awake()
    {   //this code is for testing with random body parts, final code will take body parts from inventory script
        for (int i = 0; i < 5; i++)
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
            //Debug.Log(temp);
            inventoryList.Add(temp);
            Debug.Log("Start inventory list length is " + inventoryList.Count);
        }
    }
    private void Update()
    {
        MoveHighlight();
    }
    public void LoadGrid()
    {
        //Debug.Log("Is this working?");
        Debug.Log("LoadGrid inventory list length is " + inventoryList.Count);
        for (int i = 0; i<inventoryList.Count; i++)
        {
            Debug.Log("Is this working?");
            GameObject cell = new GameObject(); //this is the base cell "location" theoretically controlled by the GridLayoutGroup
            GameObject cellObject = new GameObject(); //this is the actual object with image and BodyPart info
            //BodyPart cellBP = inventoryList[i];
            cellObject.AddComponent<Image>();
            //cellObject.AddComponent<SpriteRenderer>();
            cellObject.GetComponent<Image>().sprite = inventoryList[i].icon;
            cell.AddComponent<Image>();
            var tempColor = cell.GetComponent<Image>().color;
            tempColor.a = 0f;
            cell.GetComponent<Image>().color = tempColor;
            cell.transform.SetParent(computerGrid.transform, false);
            cellObject.transform.SetParent(cell.transform, false);
            cellObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(30, 30);
            var opacity = cellObject.GetComponent<Image>().color;
            opacity.a = .3f;
            cellObject.GetComponent<Image>().color = opacity;
            
        }
        
    }
    public void HighlightSelectInitial()
    {
        if (computerGrid.transform.childCount > 0)
        {
            var temp = computerGrid.transform.GetChild(0).GetChild(0).GetComponent<Image>().color;
            temp.a = 1f;
            computerGrid.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = temp;
        }
        
    }
    public void MoveHighlight()
    { 

        if(Input.GetKeyDown(KeyCode.LeftArrow) && computerUI.customSession)
        {
            if (activeCellNum % 4 != 0)
            {
                var tempa = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
                tempa.a = .3f;
                computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = tempa;
                activeCellNum -= 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && computerUI.customSession)
        {
            if((activeCellNum + 1)% 4 != 0 && (activeCellNum + 1) < inventoryList.Count-1)
            {
                var tempa = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
                tempa.a = .3f;
                computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = tempa;
                activeCellNum += 1;
            }

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && computerUI.customSession)
        {
            if(activeCellNum > 3)
            {
                var tempa = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
                tempa.a = .3f;
                computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = tempa;
                activeCellNum -= 4;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && computerUI.customSession)
        {
            if (activeCellNum < inventoryList.Count && (activeCellNum + 4) < inventoryList.Count)
            {
                var tempa = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
                tempa.a = .3f;
                computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = tempa;
                activeCellNum += 4;
            }
        }
        if (computerGrid.transform.childCount > 0)
        {
            var temp = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
            temp.a = 1f;
            computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = temp;
        }
    }
    

}
