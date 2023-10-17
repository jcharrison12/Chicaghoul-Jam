using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TriggerComputer : MonoBehaviour
{
    public Canvas promptcanvas;
    public TextMeshProUGUI prompt;
    public Canvas monsterGUI;
    public GameObject player;
    public bool computerOpen;
    public bool customSession;
    public CustomizeSelection gridlogic;
    // Start is called before the first frame update
    void Start()
    {
        promptcanvas.gameObject.SetActive(false);
        prompt.gameObject.SetActive(false);
        monsterGUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && computerOpen)
        {
            CustomPanel();
        }
        else if (Input.GetKeyDown(KeyCode.E) && customSession)
        {
            ClosePanel();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if(collision.gameObject.tag == "Player")
        {
            promptcanvas.gameObject.SetActive(true);
            prompt.gameObject.SetActive(true);
            computerOpen = true;
            
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            promptcanvas.gameObject.SetActive(false);
            prompt.gameObject.SetActive(false);
            computerOpen = false;
        }
    }

    private void CustomPanel()
    {
        monsterGUI.gameObject.SetActive(true);
        customSession = true;
        computerOpen = false;
        promptcanvas.gameObject.SetActive(false);
        prompt.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        gridlogic.LoadGrid();
        gridlogic.HighlightSelectInitial();

    }

    public void ClosePanel()
    {
        gridlogic.RemoveBodyParts();
        monsterGUI.gameObject.SetActive(false);
        customSession = false;
        computerOpen = true;
        promptcanvas.gameObject.SetActive(true);
        prompt.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
       
    }
}
