using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomizeSelection : MonoBehaviour
{
    public GameObject previousSceneObj;
    public List<BodyPart> inventoryList = new List<BodyPart>();
    //public List<BodyPart> bpOptions;
    public GameObject computerGrid;
    public TriggerComputer computerUI;
    public int activeCellNum = 0;
    public GameObject Monster;
    public List<BodyPart> bpScores = new List<BodyPart>();
    public Sprite _checkSprite;
    public GameObject scoresObj;
    public Scoring scoreScript;
    

    private AudioSource myAudio;
    public AudioClip partSelect, partPlacement, pcConfirm;

    // Start is called before the first frame update
    void Awake()
    {
        //{   //this code is for testing with random body parts, final code will take body parts from inventory script
        scoresObj = FindFirstObjectByType<Scoring>().gameObject;
        scoreScript = scoresObj.GetComponent<Scoring>();

        //    //DontDestroyOnLoad(scoresObj);
        previousSceneObj = FindFirstObjectByType<BodyPartManager>().gameObject;
        inventoryList = previousSceneObj.GetComponent<BodyPartManager>().bodyParts;
        myAudio = GetComponent<AudioSource>();
    //    for (int i = 0; i < 5; i++)
    //    {
    //        BodyPart.PartOption tempEnum = (BodyPart.PartOption)i;
    //        BodyPart temp = ScriptableObject.CreateInstance<BodyPart>();
    //        List<BodyPart> results = bpOptions.FindAll(
    //          delegate (BodyPart bp)
    //          {
    //              return bp.part == tempEnum;
    //          }
    //          );
    //        temp = results[Random.Range(0, results.Count)];
    //        //Debug.Log(temp);
    //        inventoryList.Add(temp);
            
        
    }
    private void Update()
    {
        MoveHighlight();
        ChoosePart();
        SubmitMonster();
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
            cellObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(40, 40);
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
            if((activeCellNum + 1)% 4 != 0 && (activeCellNum + 1) < inventoryList.Count)
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
            var tempPart = ScriptableObject.CreateInstance<BodyPart>();
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
                    if (Monster.transform.GetChild(0).gameObject.GetComponent<BodyPartInstance>() == null)
                    {
                        Monster.transform.GetChild(0).gameObject.AddComponent<BodyPartInstance>();
                        Monster.transform.GetChild(0).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(0).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(0).GetComponent<Image>().color = temp;
                        bpScores.Add(selection.bpType);
                    }
                    else
                    {
                        tempPart = Monster.transform.GetChild(0).gameObject.GetComponent<BodyPartInstance>().bpType;
                        Monster.transform.GetChild(0).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(0).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(0).GetComponent<Image>().color = temp;
                        bpScores.Remove(tempPart);
                        bpScores.Add(selection.bpType);
                    }
                        break;
                    
                case BodyPart.PartOption.face:
                    if (Monster.transform.GetChild(1).gameObject.GetComponent<BodyPartInstance>() == null)
                    {
                        Monster.transform.GetChild(1).gameObject.AddComponent<BodyPartInstance>();
                        Monster.transform.GetChild(1).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(1).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(1).GetComponent<Image>().color = temp;
                        bpScores.Add(selection.bpType);
                    }
                    else
                    {
                        tempPart = Monster.transform.GetChild(1).gameObject.GetComponent<BodyPartInstance>().bpType;
                        Monster.transform.GetChild(1).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(1).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(1).GetComponent<Image>().color = temp;
                        bpScores.Remove(tempPart);
                        bpScores.Add(selection.bpType);
                    }
                    break;
                case BodyPart.PartOption.body:
                    if (Monster.transform.GetChild(2).gameObject.GetComponent<BodyPartInstance>() == null)
                    {
                        Monster.transform.GetChild(2).gameObject.AddComponent<BodyPartInstance>();
                        Monster.transform.GetChild(2).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(2).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(2).GetComponent<Image>().color = temp;
                        bpScores.Add(selection.bpType);
                    }
                    else
                    {
                        tempPart = Monster.transform.GetChild(2).gameObject.GetComponent<BodyPartInstance>().bpType;
                        Monster.transform.GetChild(2).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(2).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(2).GetComponent<Image>().color = temp;
                        bpScores.Remove(tempPart);
                        bpScores.Add(selection.bpType);
                    }
                    break;
                case BodyPart.PartOption.arm:
                    if (Monster.transform.GetChild(4).gameObject.GetComponent<BodyPartInstance>() == null)
                    {
                        Monster.transform.GetChild(4).gameObject.AddComponent<BodyPartInstance>();
                        Monster.transform.GetChild(4).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(4).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(4).GetComponent<Image>().color = temp;
                        Monster.transform.GetChild(3).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(3).GetComponent<Image>().color = temp;
                        Monster.transform.GetChild(3).localScale = new Vector3(-1, 1, 1);
                        bpScores.Add(selection.bpType);
                    }
                    else
                    {
                        tempPart = Monster.transform.GetChild(4).gameObject.GetComponent<BodyPartInstance>().bpType;
                        Monster.transform.GetChild(4).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(4).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(4).GetComponent<Image>().color = temp;
                        Monster.transform.GetChild(3).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(3).GetComponent<Image>().color = temp;
                        Monster.transform.GetChild(3).localScale = new Vector3(-1, 1, 1);
                        bpScores.Remove(tempPart);
                        bpScores.Add(selection.bpType);
                    }
                    break;
                case BodyPart.PartOption.leg:
                    if (Monster.transform.GetChild(6).gameObject.GetComponent<BodyPartInstance>() == null)
                    {
                        Monster.transform.GetChild(6).gameObject.AddComponent<BodyPartInstance>();
                        Monster.transform.GetChild(6).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(6).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(6).GetComponent<Image>().color = temp;
                        Monster.transform.GetChild(5).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(5).GetComponent<Image>().color = temp;
                        Monster.transform.GetChild(5).localScale = new Vector3(-1, 1, 1);
                        bpScores.Add(selection.bpType);
                    }
                    else
                    {
                        tempPart = Monster.transform.GetChild(6).gameObject.GetComponent<BodyPartInstance>().bpType;
                        Monster.transform.GetChild(6).gameObject.GetComponent<BodyPartInstance>().bpType = selection.bpType;
                        Monster.transform.GetChild(6).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(6).GetComponent<Image>().color = temp;
                        Monster.transform.GetChild(5).GetComponent<Image>().sprite = selectionImg.sprite;
                        Monster.transform.GetChild(5).GetComponent<Image>().color = temp;
                        Monster.transform.GetChild(5).localScale = new Vector3(-1, 1, 1);
                        bpScores.Remove(tempPart);
                        bpScores.Add(selection.bpType);
                    }
                    break;
            }
            for(int i = 0; i < computerGrid.transform.childCount; i++)
            {
                if(tempPart != null && tempPart == computerGrid.transform.GetChild(i).GetChild(0).GetComponent<BodyPartInstance>().bpType)
                {
                    var tempColor = computerGrid.transform.GetChild(i).GetChild(1).GetComponent<Image>().color;
                    tempColor.a = 0f;
                    computerGrid.transform.GetChild(i).GetChild(1).GetComponent<Image>().color = tempColor;
                    computerGrid.transform.GetChild(i).GetChild(1).GetComponent<Image>().sprite = null;
                }
            }
            GameObject checkbox = new GameObject();
            checkbox.AddComponent<Image>();
            //computerGrid.transform.GetChild(activeCellNum).gameObject.AddComponent<Image>();
            //computerGrid.transform.GetChild(activeCellNum).gameObject.GetComponent<Image>().sprite = _checkSprite;

            checkbox.GetComponent<Image>().sprite = _checkSprite;
            checkbox.transform.SetParent(computerGrid.transform.GetChild(activeCellNum), false);

        }

           
        
    }
    public void RemoveBodyParts()
    {
        for(int i = 0; i < computerGrid.transform.childCount; i++)
        {
            Destroy(computerGrid.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < Monster.transform.childCount; i++)
        {
            Destroy(Monster.transform.GetChild(i).gameObject.GetComponent<BodyPartInstance>());
            Monster.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            var temp = Monster.transform.GetChild(i).GetComponent<Image>().color;
            temp.a = 0f;
            Monster.transform.GetChild(i).GetComponent<Image>().color = temp;
        }
    }
    public void SubmitMonster()
    { 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            myAudio.PlayOneShot(pcConfirm);
            scoreScript.scores = bpScores;
            computerUI.ClosePanel();
            StartCoroutine(scoreScript.Waiting());

        }

    }


}
