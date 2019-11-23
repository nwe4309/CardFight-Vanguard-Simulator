using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearGuardCircle : MonoBehaviour {

    public GameObject currentCard;

    [SerializeField] private GameObject nullCard;

    public void SetCurrentCard(GameLogicMethods.Player p, GameObject card)
    {
        // If it's the players turn
        if(p == GameLogicMethods.Player.player)
        {
            if (currentCard != nullCard)
            {
                // Add the current card to the players dropzone
                GameObject.Find("Game Manager").GetComponent<FieldManager>().playerDropZone.GetComponent<DropZone>().dropZone.Add(currentCard);
                // Update the current cards position
                currentCard.transform.position = GameObject.Find("Game Manager").GetComponent<FieldManager>().playerDropZone.transform.position;
            }

            // Set the current card to the new passed in card
            currentCard = card;
        }
    }

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(currentCard == null)
        {
            nullCard.transform.position = gameObject.transform.position;
            currentCard = Instantiate(nullCard);
        }

        if(currentCard != null)
            currentCard.transform.position = gameObject.transform.position;

	}
}
