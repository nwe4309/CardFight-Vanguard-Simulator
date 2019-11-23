using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardCircle : MonoBehaviour 
{

    public Stack<GameObject> soul = new Stack<GameObject>();
    public GameObject topCard;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
        // If there is a card in the vanguard circle
        if (soul.Count > 0)
        {
            // Set the top card to be the top card of the stack
            topCard = soul.Peek();

            topCard.transform.position = gameObject.transform.position;

            // Set the top card's z value to be the above the other cards
            topCard.transform.position = new Vector3(topCard.transform.position.x, topCard.transform.position.y, -9);

            // Loop through each card in the soul
            foreach(GameObject card in soul)
            {
                // If the card being looked at is not the top card
                if(card != topCard)
                {
                    // Set it's z value to be 0
                    card.transform.position = new Vector3(topCard.transform.position.x, topCard.transform.position.y, 0);
                }
            }
        }
	}
}
