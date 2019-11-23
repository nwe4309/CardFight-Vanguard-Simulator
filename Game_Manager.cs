using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour 
{

    public bool isStandard;

    public GameObject selectedCard;
    GameObject previouslySelectedCard;

    public GameObject reshuffleButton;

    // Use this for initialization
    void Start()
    {
        if(GameObject.Find("Menu") == null)
        {
            isStandard = true;
        }
        else
        {
            isStandard = GameObject.Find("Menu").GetComponent<Menu>().standard;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the mouse was clicked
        if (Input.GetMouseButtonDown(0))
        {
            if(selectedCard != null)
            {
                selectedCard.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }
            // Checks to see if any card on the field is clicked
            selectedCard = SelectCard();
        }

        HighlightCard();
    }

    public void XButton()
    {
        Application.Quit();
    }

    public GameObject SelectCard()
    {
        // Stores the position of the mouse in world space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Casts a ray from the camera to the mouse and stores what was hit in a RaycastHit object
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        // If something was hit
        if (hit)
        {
            // If the object clicked on is a card
            if(hit.transform.GetComponent("Card") != null)
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }

    public void HighlightCard()
    {
        if(selectedCard != null)
        {
            selectedCard.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 180, 255);
        }
    }
}
