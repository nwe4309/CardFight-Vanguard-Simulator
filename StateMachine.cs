using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour {

    public enum GameState
    {
        PauseMenu,
        Options,
        Game
    }

    public enum TurnState
    {
        DrawHand,
        Reshuffle,
        Stand,
        Draw,
        GAssist,
        Ride,
        Stride,
        Main,
        Battle,
        Guard,
        End
    }

    public enum BattleState
    {
        DriveCheck,
        DamageCheck,
        Null
    }

    public enum BattleSubState
    {
        SelectAttacker,
        SelectBooster,
        SelectTarget,
        SelectGuardUnits,
        Resolve,
        Null
    }

    public GameState currentGameState;
    public TurnState currentTurnState;
    public BattleState currentBattleState;
    public BattleSubState currentBattleSubState;

    public GameObject currentPhaseText;

    // Use this for initialization
    void Start ()
    {
        currentGameState = GameState.Game;
        currentTurnState = TurnState.DrawHand;
        currentBattleState = BattleState.Null;
        currentBattleSubState = BattleSubState.Null;
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentTurnState)
        {
            case TurnState.DrawHand:
                currentPhaseText.GetComponent<Text>().text = "Draw Hand Phase";
                break;
            case TurnState.Reshuffle:
                currentPhaseText.GetComponent<Text>().text = "Reshuffle Phase";
                break;
            case TurnState.Stand:
                currentPhaseText.GetComponent<Text>().text = "Stand Phase";
                break;
            case TurnState.Draw:
                currentPhaseText.GetComponent<Text>().text = "Draw Phase";
                break;
            case TurnState.GAssist:
                currentPhaseText.GetComponent<Text>().text = "G Assist Phase";
                break;
            case TurnState.Ride:
                currentPhaseText.GetComponent<Text>().text = "Ride Phase";
                break;
            case TurnState.Stride:
                currentPhaseText.GetComponent<Text>().text = "Stride Phase";
                break;
            case TurnState.Main:
                currentPhaseText.GetComponent<Text>().text = "Main Phase";
                break;
            case TurnState.Battle:
                currentPhaseText.GetComponent<Text>().text = "Battle Phase";
                break;
            case TurnState.Guard:
                currentPhaseText.GetComponent<Text>().text = "Guard Phase";
                break;
            case TurnState.End:
                currentPhaseText.GetComponent<Text>().text = "End Phase";
                break;
        }
	}
}
