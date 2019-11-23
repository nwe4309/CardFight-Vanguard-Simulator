using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour 
{
    public GameObject playerHandMidpoint;
    public GameObject opponentHandMidpoint;

    public GameObject playerDeck;
    public GameObject opponentDeck;

    public List<GameObject> playerHand;
    public List<GameObject> opponentHand;
    
	// Use this for initialization
	void Start () 
	{
        // Shuffles the player and opponents deck twice
        playerDeck.GetComponent<Deck>().Shuffle();
        playerDeck.GetComponent<Deck>().Shuffle();
        opponentDeck.GetComponent<Deck>().Shuffle();
        opponentDeck.GetComponent<Deck>().Shuffle();

        // Sets up both players hands
        SetupHand();
    }
	
	// Update is called once per frame
	void Update () 
	{
        // Loops through the players hand
		for(int i = 0; i < playerHand.Count; i ++)
        {
            // Set the position of the cards in hand to the correct spot
            //playerHand[i].transform.position = new Vector3(-6.8f + i, -9, -playerHand.Count + i);
            OrganizeHand(playerHand, playerHandMidpoint);
            // Make the cards in hand revealed
            playerHand[i].GetComponent<Card>().isRevealed = true;

            //// Stores the bounds of the cards in the hand
            //float leftBounds = playerHand[i].transform.position.x - (playerHand[i].GetComponent<BoxCollider2D>().size.x * playerHand[i].GetComponent<Card>().cardWidth) / 2f;
            //float rightBounds = playerHand[i].transform.position.x + (playerHand[i].GetComponent<BoxCollider2D>().size.x * playerHand[i].GetComponent<Card>().cardWidth) / 2f;
            //float upperBounds = playerHand[i].transform.position.y + (playerHand[i].GetComponent<BoxCollider2D>().size.y * playerHand[i].GetComponent<Card>().cardHeight) / 2f;
            //float lowerBounds = playerHand[i].transform.position.y - (playerHand[i].GetComponent<BoxCollider2D>().size.y * playerHand[i].GetComponent<Card>().cardHeight) / 2f;
        }

        // Hovers cards in the players hand when moused over
        HoverCard();

        // Loops through the opponents hand
        for(int j = 0; j < opponentHand.Count; j ++)
        {
            // Set the position of the cards in hand to the correct spot
            //opponentHand[j].transform.position = new Vector3(6.5f - j, 9, -opponentHand.Count + j);
            OrganizeHand(opponentHand, opponentHandMidpoint);
        }
	}

    public void SetupHand()
    {
        // Draws a starting hand of 5 cards for the player and opponent
        for (int i = 0; i < 5; i++)
        {
            playerHand.Add(playerDeck.GetComponent<Deck>().Draw());
            opponentHand.Add(opponentDeck.GetComponent<Deck>().Draw());
        }
    }

    public void HoverCard()
    {
        // Stores the position of the mouse in world space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Casts a ray from the camera to the mouse and stores what was hit in a RaycastHit object
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        // If something was hit
        if(hit)
        {
            // Loop through the players hands. Only loops through players hand since that is the over cards that should hover when being hovered over
            for(int i = 0; i < playerHand.Count; i ++)
            {
                // If the card being hovered over is in the list
                if(playerHand[i].transform.position == hit.transform.position)
                {
                    // Move the card up a little bit and set its z position to -80 so it is always the top thing shown
                    float yPos = playerHand[i].transform.position.y;
                    playerHand[i].transform.position = new Vector3(playerHand[i].transform.position.x, yPos + 0.5f, -80);
                }
            }
        }
    }

    public void OrganizeHand(List<GameObject> hand, GameObject midpoint)
    {
        // Used for spacing
        int counter = 1;
        // Spacing inbetween cards
        float spacing = 10.0f / hand.Count;
        for(int i = 0; i < hand.Count; i +=2)
        {
            // If the handsize is odd
            if(hand.Count % 2 == 1)
            {
                // If at the beginning of the loop
                if(i == 0)
                {
                    // Place the card at the midpoint
                    hand[i].transform.position = midpoint.transform.position;
                }
                else
                {
                    // Position the card to the left of the midpoint
                    hand[i - 1].transform.position = midpoint.transform.position;
                    hand[i - 1].transform.Translate(-spacing * counter, 0, 0);

                    // Position the next card to the right of the midpoint
                    hand[i].transform.position = midpoint.transform.position;
                    hand[i].transform.Translate(spacing * counter, 0, 0);

                    // Increase the counter
                    counter++;
                }
            }
            // If the handsize is even
            else
            {
                //If at the beginning of the loop
                if (i == 0)
                {
                    // Place the card at the midpoint but half a spacing to the left
                    hand[i].transform.position = midpoint.transform.position;
                    hand[i].transform.Translate(-(spacing/2) * counter, 0, 0);

                    // Place the next card at the midpoint but half a spacing to the right
                    hand[i + 1].transform.position = midpoint.transform.position;
                    hand[i + 1].transform.Translate((spacing / 2) * counter, 0, 0);
                }
                else
                {
                    // Place the card at the midpoint then move it 1 and a half spacings to the left
                    hand[i].transform.position = midpoint.transform.position;
                    hand[i].transform.Translate((spacing / 2) + -spacing * counter, 0, 0);

                    // Place the next card at the midpoint the move it 1 and a half spacing to the right
                    hand[i + 1].transform.position = midpoint.transform.position;
                    hand[i + 1].transform.Translate(-(spacing / 2) + spacing * counter, 0, 0);
                }
                // Increase the counter
                counter++;
            }
        }
    }
}
