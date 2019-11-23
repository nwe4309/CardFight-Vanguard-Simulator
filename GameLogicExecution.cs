using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicExecution : MonoBehaviour {

    public enum Turn
    {
        playerTurn,
        opponentTurn
    }

    GameLogicMethods gameLogic;
    StateMachine stateMachine;
    Game_Manager gameManager;
    FieldManager fieldManager;

    Turn currentTurn;

	// Use this for initialization
	void Start ()
    {
        gameLogic = gameObject.GetComponent<GameLogicMethods>();
        stateMachine = gameObject.GetComponent<StateMachine>();
        gameManager = gameObject.GetComponent<Game_Manager>();
        fieldManager = gameObject.GetComponent<FieldManager>();

        // Flip coin to decide who goes first (or rock paper scissors)
        currentTurn = Turn.playerTurn;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(currentTurn == Turn.playerTurn)
        {
            switch(stateMachine.currentTurnState)
            {
                case StateMachine.TurnState.DrawHand:
                    stateMachine.currentTurnState = StateMachine.TurnState.Reshuffle;
                    break;
                case StateMachine.TurnState.Reshuffle:
                    gameLogic.FillReshuffleList(GameLogicMethods.Player.player);
                    gameLogic.HighlightCardsToReshuffle();
                    break;
                case StateMachine.TurnState.Stand:
                    gameLogic.StandCards(GameLogicMethods.Player.player);
                    stateMachine.currentTurnState = StateMachine.TurnState.Draw;
                    break;
                case StateMachine.TurnState.Draw:
                    gameLogic.Draw(GameLogicMethods.Player.player);
                    if (gameManager.isStandard)
                        stateMachine.currentTurnState = StateMachine.TurnState.Ride;
                    else
                        stateMachine.currentTurnState = StateMachine.TurnState.GAssist;
                    break;
                case StateMachine.TurnState.GAssist:
                    
                    break;
                case StateMachine.TurnState.Ride:
                    gameLogic.ShowPossibleSelections();
                    gameLogic.HighlightSelectionsWhenHovered();
                    if (gameLogic.Ride(GameLogicMethods.Player.player) == true)
                    {
                        if (gameManager.isStandard)
                            stateMachine.currentTurnState = StateMachine.TurnState.Main;
                        else
                            stateMachine.currentTurnState = StateMachine.TurnState.Stride;
                    }
                    break;
                case StateMachine.TurnState.Stride:

                    break;
                case StateMachine.TurnState.Main:
                    gameLogic.ShowPossibleSelections();
                    gameLogic.HighlightSelectionsWhenHovered();

                    gameLogic.MainPhase(GameLogicMethods.Player.player);
                    break;
            }
        }
        else if(currentTurn == Turn.opponentTurn)
        {
            if (stateMachine.currentTurnState == StateMachine.TurnState.Draw)
                gameLogic.Draw(GameLogicMethods.Player.opponent);
        }
	}
}
