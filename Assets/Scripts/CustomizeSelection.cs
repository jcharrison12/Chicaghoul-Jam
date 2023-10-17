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
    public GameObject Monster;
    public List<BodyPart> bpScores = new List<BodyPart>();
    public Sprite _checkSprite;

    private AudioSource myAudio;
    public AudioClip partSelect, partPlacement;

    // Start is called before the first frame update
    void Awake()
    {   //this code is for testing with random body parts, final code will take body parts from inventory script

        myAudio = GetComponent<AudioSource>();
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
        ChoosePart();
    }
    public void LoadGrid()
    {
        
        
        for (int i = 0; i<inventoryList.Count; i++)
        {
           
            GameObject cell = new GameObject(); //this is the base cell "location" theoretically controlled by the GridLayoutGroup
            GameObject cellObject = new GameObject(); //this is the actual object with image and BodyPart info
            //BodyPart cellBP = inventoryList[i];
            cellObject.AddComponent<Image>();
            cellObject.AddComponent<BodyPartInstance>();
            cellObject.GetComponent<BodyPartInstance>().bpType = inventoryList[i];
            //cellObject.AddComponent<SpriteRenderer>();
            cellObject.GetComponent<Image>().sprite = inventoryList[i].icon;
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
                myAudio.PlayOneShot(partSelect);
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
                myAudio.PlayOneShot(partSelect);
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
                myAudio.PlayOneShot(partSelect);
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
                myAudio.PlayOneShot(partSelect);
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
    public void ChoosePart()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            myAudio.PlayOneShot(partPlacement);
            BodyPart.PartOption BPslot;
            var selectionImg = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<Image>();
            //var flipped = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<SpriteRenderer>().flipY;
            var selection = computerGrid.transform.GetChild(activeCellNum).GetChild(0).GetComponent<BodyPartInstance>();
            BPslot = selection.bpType.part;
            var temp = Monster.transform.GetChild(0).GetComponent<Image>().color;
            temp.a = 1f;
            switch (BPslot)
            {
                case BodyPart.PartOption.brain:
                    Monster.transform.GetChild(0).GetComponent<Image>().sprite = selectionImg.sprite;
                    Monster.transform.GetChild(0).GetComponent<Image>().color = temp;
                    break;
                case BodyPart.PartOption.face:
                    Monster.transform.GetChild(1).GetComponent<Image>().sprite = selectionImg.sprite;
                    Monster.transform.GetChild(1).GetComponent<Image>().color = temp;
                    break;
                case BodyPart.PartOption.body:
                    Monster.transform.GetChild(2).GetComponent<Image>().sprite = selectionImg.sprite;
                    Monster.transform.GetChild(2).GetComponent<Image>().color = temp;
                    break;
                case BodyPart.PartOption.arm:
                    Monster.transform.GetChild(4).GetComponent<Image>().sprite = selectionImg.sprite;
                    Monster.transform.GetChild(4).GetComponent<Image>().color = temp;
                    Monster.transform.GetChild(3).GetComponent<Image>().sprite = selectionImg.sprite;
                    Monster.transform.GetChild(3).GetComponent<Image>().color = temp;
                    Monster.transform.GetChild(3).localScale = new Vector3(-1, 1, 1);
                    break;
                case BodyPart.PartOption.leg:
                    Monster.transform.GetChild(6).GetComponent<Image>().sprite = selectionImg.sprite;
                    Monster.transform.GetChild(6).GetComponent<Image>().color = temp;
                    Monster.transform.GetChild(5).GetComponent<Image>().sprite = selectionImg.sprite;
                    Monster.transform.GetChild(5).GetComponent<Image>().color = temp;
                    Monster.transform.GetChild(5).localScale = new Vector3(-1, 1, 1);
                    break;

            }
          
            GameObject checkbox = new GameObject();
            checkbox.AddComponent<Image>();
            //computerGrid.transform.GetChild(activeCellNum).gameObject.AddComponent<Image>();
            //computerGrid.transform.GetChild(activeCellNum).gameObject.GetComponent<Image>().sprite = _checkSprite;

            checkbox.GetComponent<Image>().sprite = _checkSprite;
            checkbox.transform.SetParent(computerGrid.transform.GetChild(activeCellNum), false);

        }

           
        
    }
    

}
