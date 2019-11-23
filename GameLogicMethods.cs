using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicMethods : MonoBehaviour {

    public enum Player
    {
        player,
        opponent
    }

    public GameObject availableSelection;

    HandManager handManager;
    Game_Manager gameManager;
    StateMachine stateMachine;
    FieldManager fieldManager;

    public List<GameObject> cardsToReshuffle;

    public GameObject cardSelectedDuringRide;
    public GameObject cardSelectedDuringMain;

    bool selectorsShown;
    bool selectorEnlarged;
    Vector3 selectorBaseScale;

    // Use this for initialization
    void Start ()
    {
        handManager = gameObject.GetComponent<HandManager>();
        gameManager = gameObject.GetComponent<Game_Manager>();
        stateMachine = gameObject.GetComponent<StateMachine>();
        fieldManager = gameManager.GetComponent<FieldManager>();

        cardsToReshuffle = new List<GameObject>();

        selectorsShown = false;

        selectorBaseScale = availableSelection.transform.localScale;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void FillReshuffleList(Player p)
    {
        if(p == Player.player)
        {
            // Set the confirm reshuffle button to active
            gameManager.reshuffleButton.SetActive(true);
            // If the mouse was clicked
            if (Input.GetMouseButtonDown(0))
            {
                // Loop through cards in the player's hand
                for (int i = 0; i < handManager.playerHand.Count; i++)
                {
                    // If the selected card is in the players hand
                    if (gameManager.selectedCard == handManager.playerHand[i])
                    {
                        bool cardRepeat = false;
                        // Loop through the cardsToReshuffle list
                        for (int j = 0; j < cardsToReshuffle.Count; j++)
                        {
                            // If the selected card is already in the list
                            if (gameManager.selectedCard == cardsToReshuffle[j])
                            {
                                // Don't add it
                                cardRepeat = true;
                                
                                // Remove the card from the list and deselect it.
                                // This allows the player to click on a selected card to remove it from the cards to be reshuffled
                                cardsToReshuffle.RemoveAt(j);
                                gameManager.selectedCard.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                                gameManager.selectedCard = null;
                            }
                        }

                        // If the card is not in the list
                        if (cardRepeat == false)
                        {
                            // Add it to the list
                            cardsToReshuffle.Add(gameManager.selectedCard);
                        }
                    }
                }
            }
        }
        else
        {
            // AI goes here
        }
    }

    public void HighlightCardsToReshuffle()
    {
        // Loops through the list of cards to be reshuffled
        for(int i = 0; i < cardsToReshuffle.Count; i ++)
        {
            // Highlights all those cards
            cardsToReshuffle[i].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 180, 255);
        }
    }

    public void ReshuffleCards()
    {
        int cardsReshuffled = cardsToReshuffle.Count;
        // Loops through the list of cards to be reshuffled
        for(int i = 0; i < cardsToReshuffle.Count; i ++)
        {
            // Takes the cards to be reshuffled at adds them to the bottom of the deck
            handManager.playerDeck.GetComponent<Deck>().AddToBottom(cardsToReshuffle[i]);
            // Unhighlights the cards
            cardsToReshuffle[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            // Moves the cards off screen
            cardsToReshuffle[i].transform.position = new Vector3(-100, -100, 0);
            // Removes the cards from the players hand
            handManager.playerHand.Remove(cardsToReshuffle[i]);
        }
        // Clears the cardsToReshuffle list
        cardsToReshuffle.Clear();
        // Draws as many new cards as were sent back to the deck
        for(int i = 0; i < cardsReshuffled; i ++)
        {
            Draw(Player.player);
        }

        // Changes the turn state
        stateMachine.currentTurnState = StateMachine.TurnState.Stand;
        // Makes the reshuffle button disappear
        gameManager.reshuffleButton.SetActive(false);
    }

    public void StandCards(Player p)
    {
        if(p == Player.player)
        {
            if (fieldManager.playerVC.GetComponent<VanguardCircle>().topCard != null && fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().isRevealed == false)
                fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().isRevealed = true;

            if (fieldManager.playerVC.GetComponent<VanguardCircle>().topCard != null && fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().isRested)
                fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().isRested = false;
            if (fieldManager.playerTLRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerTLRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested)
                fieldManager.playerTLRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested = false;
            if (fieldManager.playerTRRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerTRRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested)
                fieldManager.playerTRRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested = false;
            if (fieldManager.playerBLRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerBLRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested)
                fieldManager.playerBLRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested = false;
            if (fieldManager.playerCRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerCRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested)
                fieldManager.playerCRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested = false;
            if (fieldManager.playerBRRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerBRRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested)
                fieldManager.playerBRRG.GetComponent<RearGuardCircle>().currentCard.GetComponent<Card>().isRested = false;
        }
    }

    public void Draw(Player p)
    {
        // If it's the players turn
        if (p == Player.player)
            // Draw a card from the players deck and add it to the players hand
            handManager.playerHand.Add(handManager.playerDeck.GetComponent<Deck>().Draw());
        // Else if it's the opponents turn
        else
            // Draw a card from the opponents deck and add it to the opponents hand
            handManager.opponentHand.Add(handManager.opponentDeck.GetComponent<Deck>().Draw());
    }

    public void GAssist(Player p)
    {

    }

    public bool Ride(Player p)
    {
        if(p == Player.player)
        {
            // If the player left clicks
            if (Input.GetMouseButtonDown(0))
            {
                // If the selected card is in the players hand
                if(handManager.playerHand.Contains(gameManager.selectedCard))
                {
                    // Set the cardSelectedDuring ride equal to that selected card
                    cardSelectedDuringRide = gameManager.selectedCard;
                }
                
                // If the player's vanguard is selected
                if (fieldManager.playerVC.GetComponent<VanguardCircle>().topCard == gameManager.selectedCard)
                {
                    // Check to see if the player has selected a card from their hand, if they have then check to see if the grade is equal to or 1 higher than the current vanguard
                    if (cardSelectedDuringRide != null && 
                        (cardSelectedDuringRide.GetComponent<Card>().grade == fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade || 
                        cardSelectedDuringRide.GetComponent<Card>().grade == fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade+1))
                    {
                        // Push the selected card to the top of the soul stack
                        fieldManager.playerVC.GetComponent<VanguardCircle>().soul.Push(cardSelectedDuringRide);
                        // Removed the selected card from the players hand
                        handManager.playerHand.Remove(cardSelectedDuringRide);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void MainPhase(Player p)
    {
        if (p == Player.player)
        {
            // If the player left clicks
            if (Input.GetMouseButtonDown(0))
            {
                // If the selected card is in the players hand
                if (handManager.playerHand.Contains(gameManager.selectedCard))
                {
                    // Set the cardSelectedDuringMain equal to that selected card
                    cardSelectedDuringMain = gameManager.selectedCard;
                }

                // If the player's top left rearguard circle is selected
                if (fieldManager.playerTLRG == gameManager.selectedCard || 
                    (fieldManager.playerTLRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerTLRG.GetComponent<RearGuardCircle>().currentCard == gameManager.selectedCard))
                {
                    // Check to see if the player has selected a card from their hand and if it's grade is less than or equal to the vanguards grade
                    if (cardSelectedDuringMain != null && cardSelectedDuringMain.GetComponent<Card>().grade <= fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade)
                    {
                        // Set the top left rearguard circle's current card to the selected card
                        fieldManager.playerTLRG.GetComponent<RearGuardCircle>().SetCurrentCard(Player.player, cardSelectedDuringMain);
                        // Removed the selected card from the players hand
                        handManager.playerHand.Remove(cardSelectedDuringMain);

                        cardSelectedDuringMain = null;
                    }
                }
                // If the player's top right rearguard circle is selected
                else if (fieldManager.playerTRRG == gameManager.selectedCard ||
                    (fieldManager.playerTRRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerTRRG.GetComponent<RearGuardCircle>().currentCard == gameManager.selectedCard))
                {
                    // Check to see if the player has selected a card from their hand and if it's grade is less than or equal to the vanguards grade
                    if (cardSelectedDuringMain != null && cardSelectedDuringMain.GetComponent<Card>().grade <= fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade)
                    {
                        // Set the top right rearguard circle's current card to the selected card
                        fieldManager.playerTRRG.GetComponent<RearGuardCircle>().SetCurrentCard(Player.player, cardSelectedDuringMain);
                        // Removed the selected card from the players hand
                        handManager.playerHand.Remove(cardSelectedDuringMain);

                        cardSelectedDuringMain = null;
                    }
                }
                // If the player's bottom left rearguard circle is selected
                else if (fieldManager.playerBLRG == gameManager.selectedCard ||
                    (fieldManager.playerBLRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerBLRG.GetComponent<RearGuardCircle>().currentCard == gameManager.selectedCard))
                {
                    // Check to see if the player has selected a card from their hand and if it's grade is less than or equal to the vanguards grade
                    if (cardSelectedDuringMain != null && cardSelectedDuringMain.GetComponent<Card>().grade <= fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade)
                    {
                        // Set the bottom left rearguard circle's current card to the selected card
                        fieldManager.playerBLRG.GetComponent<RearGuardCircle>().SetCurrentCard(Player.player, cardSelectedDuringMain);
                        // Removed the selected card from the players hand
                        handManager.playerHand.Remove(cardSelectedDuringMain);

                        cardSelectedDuringMain = null;
                    }
                }
                // If the player's bottom right rearguard circle is selected
                else if (fieldManager.playerBRRG == gameManager.selectedCard ||
                    (fieldManager.playerBRRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerBRRG.GetComponent<RearGuardCircle>().currentCard == gameManager.selectedCard))
                {
                    // Check to see if the player has selected a card from their hand and if it's grade is less than or equal to the vanguards grade
                    if (cardSelectedDuringMain != null && cardSelectedDuringMain.GetComponent<Card>().grade <= fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade)
                    {
                        // Set the bottom right rearguard circle's current card to the selected card
                        fieldManager.playerBRRG.GetComponent<RearGuardCircle>().SetCurrentCard(Player.player, cardSelectedDuringMain);
                        // Removed the selected card from the players hand
                        handManager.playerHand.Remove(cardSelectedDuringMain);

                        cardSelectedDuringMain = null;
                    }
                }
                // If the player's center rearguard circle is selected
                else if (fieldManager.playerCRG == gameManager.selectedCard ||
                    (fieldManager.playerCRG.GetComponent<RearGuardCircle>().currentCard != null && fieldManager.playerCRG.GetComponent<RearGuardCircle>().currentCard == gameManager.selectedCard))
                {
                    // Check to see if the player has selected a card from their hand and if it's grade is less than or equal to the vanguards grade
                    if (cardSelectedDuringMain != null && cardSelectedDuringMain.GetComponent<Card>().grade <= fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade)
                    {
                        // Set the center rearguard circle's current card to the selected card
                        fieldManager.playerCRG.GetComponent<RearGuardCircle>().SetCurrentCard(Player.player, cardSelectedDuringMain);
                        // Removed the selected card from the players hand
                        handManager.playerHand.Remove(cardSelectedDuringMain);

                        cardSelectedDuringMain = null;
                    }
                }
            }
        }
    }

    public void ShowPossibleSelections()
    {

        // If it is the ride phase
        if(stateMachine.currentTurnState == StateMachine.TurnState.Ride)
        {
            // If a card is selected and it is in the player's hand, and it is a valid card to be rode upon the vanguard
            if(gameManager.selectedCard != null && handManager.playerHand.Contains(gameManager.selectedCard) &&
                (gameManager.selectedCard.GetComponent<Card>().grade == fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade ||
                        gameManager.selectedCard.GetComponent<Card>().grade == fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade + 1))
            {
                // Set the available selection prefab to the vanguard circle 
                availableSelection.transform.position = fieldManager.playerVC.transform.position;
                //availableSelection.transform.Translate(0, 0, -9);

                // If the selector has not already been shown
                if (selectorsShown == false)
                {
                    // Then create a copy of the available selection and flip the boolean
                    Instantiate(availableSelection);
                    selectorsShown = true;
                }
            }
            else
            {
                // If the above if was false, then find all available selectors on the screen and destroy them
                GameObject[] selectors = GameObject.FindGameObjectsWithTag("Available Selection");
                foreach (GameObject selection in selectors)
                    Destroy(selection);

                // Set the selector shown variable to false
                selectorsShown = false;
            }
        }
        else if(stateMachine.currentTurnState == StateMachine.TurnState.Main)
        {
            if(gameManager.selectedCard != null && handManager.playerHand.Contains(gameManager.selectedCard) &&
                gameManager.selectedCard.GetComponent<Card>().grade <= fieldManager.playerVC.GetComponent<VanguardCircle>().topCard.GetComponent<Card>().grade)
            {
                if (selectorsShown == false)
                {
                    // Set the available selection prefab to the Top left rearguard circle 
                    availableSelection.transform.position = fieldManager.playerTLRG.transform.position;
                    //availableSelection.transform.Translate(0, 0, -9);
                    // Then create a copy of the available selection
                    Instantiate(availableSelection);

                    // Set the available selection prefab to the Top right rearguard circle 
                    availableSelection.transform.position = fieldManager.playerTRRG.transform.position;
                    //availableSelection.transform.Translate(0, 0, -9);
                    // Then create a copy of the available selection
                    Instantiate(availableSelection);

                    // Set the available selection prefab to the bottom left rearguard circle 
                    availableSelection.transform.position = fieldManager.playerBLRG.transform.position;
                    //availableSelection.transform.Translate(0, 0, -9);
                    // Then create a copy of the available selection
                    Instantiate(availableSelection);

                    // Set the available selection prefab to the bottom right rearguard circle 
                    availableSelection.transform.position = fieldManager.playerBRRG.transform.position;
                    //availableSelection.transform.Translate(0, 0, -9);
                    // Then create a copy of the available selection
                    Instantiate(availableSelection);

                    // Set the available selection prefab to the center rearguard circle 
                    availableSelection.transform.position = fieldManager.playerCRG.transform.position;
                    //availableSelection.transform.Translate(0, 0, -9);
                    // Then create a copy of the available selection
                    Instantiate(availableSelection);

                    // Set the selector shown variable to true
                    selectorsShown = true;
                }
            }
            else
            {
                // If the above if was false, then find all available selectors on the screen and destroy them
                GameObject[] selectors = GameObject.FindGameObjectsWithTag("Available Selection");
                foreach (GameObject selection in selectors)
                    Destroy(selection);

                // Set the selector shown variable to false
                selectorsShown = false;
            }
        }
        
    }

    public void HighlightSelectionsWhenHovered()
    {
        // Stores the position of the mouse in world space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Casts a ray from the camera to the mouse and stores what was hit in a RaycastHit object
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        // If something was hit
        if (hit)
        {
            // If the the hit object is a player card circle or the current card that is in that circle
            if(hit.transform.gameObject == fieldManager.playerVC.GetComponent<VanguardCircle>().topCard ||
                hit.transform.gameObject == fieldManager.playerTLRG.GetComponent<RearGuardCircle>().currentCard || hit.transform.gameObject == fieldManager.playerTLRG ||
                hit.transform.gameObject == fieldManager.playerBLRG.GetComponent<RearGuardCircle>().currentCard || hit.transform.gameObject == fieldManager.playerBLRG ||
                hit.transform.gameObject == fieldManager.playerTRRG.GetComponent<RearGuardCircle>().currentCard || hit.transform.gameObject == fieldManager.playerTRRG ||
                hit.transform.gameObject == fieldManager.playerBRRG.GetComponent<RearGuardCircle>().currentCard || hit.transform.gameObject == fieldManager.playerBRRG ||
                hit.transform.gameObject == fieldManager.playerCRG.GetComponent<RearGuardCircle>().currentCard || hit.transform.gameObject == fieldManager.playerCRG
                )
            {
                // Loop through all the available selection objects that are on screen
                GameObject[] selectors = GameObject.FindGameObjectsWithTag("Available Selection");
                foreach (GameObject selection in selectors)
                {
                    // If the hit object's position is the same as the current selection's position, ignoring z value
                    if(selection.transform.position.x == hit.transform.position.x && selection.transform.position.y == hit.transform.position.y)
                    {
                        // If the selector is not currently enlarged
                        if (selectorEnlarged == false)
                        {
                            // Scale the selector up a bit and increase it's rotation speed
                            selection.transform.localScale *= 1.2f;
                            selection.GetComponent<RotationAnimation>().rotationSpeedModifier = 40;
                            selection.GetComponent<SpriteRenderer>().color = Color.cyan;
                            // Set the bool to true
                            selectorEnlarged = true;
                        }
                    }
                }
            }
        }
        // If not object was hit
        else
        {
            // Loop through all the available selection objects that are on screen
            GameObject[] selectors = GameObject.FindGameObjectsWithTag("Available Selection");
            foreach (GameObject selection in selectors)
            {
                // If a selector is enlarged
                if (selectorEnlarged == true)
                {
                    // Shrink it back to base scale and reset it's rotation speed
                    selection.transform.localScale = selectorBaseScale;
                    selection.GetComponent<RotationAnimation>().rotationSpeedModifier = 20;
                    selection.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }

            // Set the selectorEnlarged bool back to false
            selectorEnlarged = false;
        }
    }
}
