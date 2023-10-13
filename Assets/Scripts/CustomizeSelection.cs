using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeSelection : MonoBehaviour
{
    public List<BodyPart> inventoryList;
    public List<BodyPart> bpOptions;
    public GameObject computerGrid;
    public TriggerComputer computerUI;
    public int activeCellNum = 0;

    // Start is called before the first frame update
    void Start()
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
            Debug.Log(temp);
            inventoryList.Add(temp);
        }
    }
    private void Update()
    {
        MoveHighlight();
    }
    public void LoadGrid()
    {
        for (int i = 0; i<inventoryList.Count; i++)
        {
            GameObject cell = new GameObject();
            GameObject cellObject = new GameObject();
            cellObject.AddComponent<Image>();
            //cellObject.AddComponent<SpriteRenderer>();
            cellObject.GetComponent<Image>().sprite = inventoryList[i].icon;
            //cellObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            //cellObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
            //cellObject.transform.localScale = new Vector3(150, 150, 1);
            cell.AddComponent<Image>();
            var tempColor = cell.GetComponent<Image>().color;
            tempColor.a = 0f;
            cell.GetComponent<Image>().color = tempColor;
            cell.transform.SetParent(computerGrid.transform, false);
            cellObject.transform.SetParent(cell.transform, false);
            cellObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(50, 50);
            var opacity = cellObject.GetComponent<Image>().color;
            opacity.a = .3f;
            cellObject.GetComponent<Image>().color = opacity;
        }
    }
    public void HighlightSelectInitial()
    {

        var temp = computerGrid.transform.GetChild(0).GetChild(0).GetComponent<Image>().color;
        temp.a = 1f;
        computerGrid.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = temp;
    }
    public void MoveHighlight()
    {

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (activeCellNum % 4 != 0)
            {
                var tempa = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
                tempa.a = .3f;
                computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = tempa;
                activeCellNum -= 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if((activeCellNum + 1)% 4 != 0)
            {
                var tempa = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
                tempa.a = .3f;
                computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = tempa;
                activeCellNum += 1;
            }

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(activeCellNum > 3)
            {
                var tempa = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
                tempa.a = .3f;
                computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = tempa;
                activeCellNum -= 4;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (activeCellNum < inventoryList.Count-1)
            {
                var tempa = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
                tempa.a = .3f;
                computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = tempa;
                activeCellNum += 4;
            }
        }
        var temp = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color;
        temp.a = 1f;
        computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>().color = temp;

    }
    

}
