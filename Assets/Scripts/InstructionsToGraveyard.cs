using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsToGraveyard : MonoBehaviour
{
    public Button startbtn;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = startbtn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TaskOnClick()
    {
        SceneManager.LoadScene("graveyard-scene1");
    }
}
