using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Card : MonoBehaviour 
{

    public string cardFrontPath;
    public Sprite cardFront;
    public Sprite cardBack;

    public float cardWidth = 0.241f;
    public float cardHeight = 0.240f;

    public string cardName;
    public int grade;
    public int power;
    public int shield;
    public string skill;
    public string abilityText;
    public int critical;
    public string clan;
    public string race;
    public string trigger;
    public string imaginaryGift;
    public bool isStarter;

    public bool isRested;
    public bool isRevealed;

    public string owner;

	// Use this for initialization
	void Start () 
	{
        isRested = false;
        isRevealed = false;

        gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;

        cardFront = Resources.Load<Sprite>(cardFrontPath);
    }
	
	// Update is called once per frame
	void Update () 
	{
        // If isRevealed is true, set sprite to the actual cards sprite
		if(isRevealed)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cardFront;
            transform.localScale = new Vector3(cardWidth, cardHeight, 1);
            // Scales the boxcollider size with the size of card
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.66f / cardWidth, 2.395f / cardHeight);
        }
        // Else set the sprite to the cards back
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;
            transform.localScale = new Vector3(1,1,1);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.66f, 2.395f);
        }

        // If the card is on the players size of the field, don't rotate it
        if(owner == "Player")
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        // If the card is on the opponents size of the field, rotate 180 degrees
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
    }

    void OnMouseEnter()
    {
        // If the card belongs to the player
        if (owner == "Player")
        {
            // Set the large cards sprite to the sprite of the card under the mouse
            GameObject.Find("Large Card").GetComponent<SpriteRenderer>().sprite = cardFront;
        }

        // If the card belongs to the opponent, only show the large view if the card is revealed
        if(owner == "Opponent" && isRevealed)
        {
            // Set the large cards sprite to the sprite of the card under the mouse
            GameObject.Find("Large Card").GetComponent<SpriteRenderer>().sprite = cardFront;
        }
    }
}
