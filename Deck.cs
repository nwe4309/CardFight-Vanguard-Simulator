using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Deck : MonoBehaviour 
{

    public GameObject gameManager;

    public List<GameObject> deck;
    public TextAsset textFile;
    public GameObject card;
    public GameObject vanguardCircle;

    public string owner;

    // Calls before the start method
    void Awake()
    {
        // Fill the deck with cards from the text file
        FillDeck();

        // Loops through the deck
        for (int i = 0; i < deck.Count; i++)
        {
            // Finds the starter
            if (deck[i].GetComponent<Card>().isStarter == true)
            {
                // Places it onto the vanguard circle
                vanguardCircle.GetComponent<VanguardCircle>().soul.Push(deck[i]);
                // Removes it from the deck
                deck.RemoveAt(i);
            }
        }
    }

    // Use this for initialization
    void Start () 
	{

    }
	
	// Update is called once per frame
	void Update () 
	{
        if(deck.Count > 0)
        {
            //GameObject topCard = Draw();
            //topCard.transform.position = new Vector2(deck.Count, 0);
        }
            
	}

    /// <summary>
    /// Adds the passed in card to the top of the deck
    /// Top is beginning of list
    /// </summary>
    /// <param name="card">Card to be added to top of deck</param>
    public void AddToTop(GameObject card)
    {
        deck.Insert(0, card);
    }

    /// <summary>
    /// Adds the passed in card to the bottom of the deck
    /// Bottom is end of list
    /// </summary>
    /// <param name="card">Card to be added to the bottom of the deck</param>
    public void AddToBottom(GameObject card)
    {
        deck.Add(card);
    }

    /// <summary>
    /// Removes the top card from the deck and returns it
    /// </summary>
    /// <returns>Top card of the deck</returns>
    public GameObject Draw()
    {
        GameObject topCard = deck[0];
        deck.RemoveAt(0);
        return topCard;
    }

    /// <summary>
    /// Randomly orders the cards in the deck 5 times
    /// </summary>
    public void Shuffle()
    {
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < deck.Count; i++)
            {
                GameObject temp = deck[i];
                int randomIndex = Random.Range(i, deck.Count);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }
    }

    /// <summary>
    /// Reads a text file and fills the deck with those cards
    /// </summary>
    public void FillDeck()
    {
        // First splits the text file by individual cards and stores it in an array of strings
        string[] cards;
        cards = textFile.text.Split('|');

        // Loops through the list of cards from the text file
        // Starts at 1 since the first line is just to label the format for the cards
        for (int i = 1; i < cards.Length; i ++)
        {
            // Splits the current card into the individual information for itself
            string[] cardToMake = cards[i].Split(',');

            // Stores the Card script for easy access
            Card cardInfo = card.GetComponent<Card>();

            // Index 14 is how many of the same card to make so you run the card creation code that many times
            for (int j = 0; j < int.Parse(cardToMake[14]); j++)
            {
                // Reading text file ignores what's in the first spot so all indicies are moved up 1

                // Index 1 is the file path for the sprite image
                cardInfo.cardFrontPath = cardToMake[1];
                // Replaces any ' with , in the card name 
                cardToMake[2].Replace("'",",");
                // Index 2 is the card name
                cardInfo.cardName = cardToMake[2];
                // Index 3 is the card grade
                cardInfo.grade = int.Parse(cardToMake[3]);
                // Index 4 is the card power
                cardInfo.power = int.Parse(cardToMake[4]);
                // Index 5 is the card shield
                cardInfo.shield = int.Parse(cardToMake[5]);
                // Index 6 is the card skill
                cardInfo.skill = cardToMake[6];
                // Index 7 is the ability text
                cardInfo.abilityText = cardToMake[7];
                // Index 8 is the card critical
                cardInfo.critical = int.Parse(cardToMake[8]);
                // Index 9 is the cards clan
                cardInfo.clan = cardToMake[9];
                // Index 10 is the cards race
                cardInfo.race = cardToMake[10];
                // Index 11 is the cards trigger
                cardInfo.trigger = cardToMake[11];
                // Index 12 is the cards imaginary gift marker
                cardInfo.imaginaryGift = cardToMake[12];
                // Index 13 is if the card is a starter or not
                cardInfo.isStarter = bool.Parse(cardToMake[13]);

                // Sets the cards owner to whoever this deck script belongs to
                cardInfo.owner = owner;

                // Sets the position of the card to somewhere off screen
                card.transform.position = new Vector3(-100, -100, 0);

                // Add the created card to the deck list
                // Need to instantiate it now so when instantiated later, its not just a copy of a card prefab
                deck.Add(Instantiate(card));
            }

        }
    }

}
