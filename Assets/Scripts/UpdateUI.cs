﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {

	// [UI CANVASSES] -----------------------------------------------------------------------------------
	[Header("UI Canvasses")]
	public GameObject mainGameUI;
	public GameObject choiceScreenUI;

	// [PLAYER INFORMATION] -----------------------------------------------------------------------------
	[Header("Player Scores")]
	// Player scores
	public int player1Score = 0;
	public int player2Score = 0;
	public int player3Score = 0;
	public int player4Score = 0;
	
	// Variables for each player's score object
	public GameObject player1ScoreObject;
	public GameObject player2ScoreObject;
	public GameObject player3ScoreObject;
	public GameObject player4ScoreObject;

	// Variables for getting the text component from the player's score object
	Text player1ScoreText;
	Text player2ScoreText;
	Text player3ScoreText;
	Text player4ScoreText;

	[Header("Player Sprites")]
	// Player sprite objects
	public GameObject player1SpriteObject;
	public GameObject player2SpriteObject;
	public GameObject player3SpriteObject;
	public GameObject player4SpriteObject;

	// Player sprites (can be used for storing specific sprites)
	public Sprite player1Sprite;
	public Sprite player2Sprite;
	public Sprite player3Sprite;
	public Sprite player4Sprite;

	// Player icon (used for winner displays)
	public Sprite player1Icon;
	public Sprite player2Icon;
	public Sprite player3Icon;
	public Sprite player4Icon;

	// [SCORING INFORMATION] -----------------------------------------------------------------------------
	[Header("Winning Score")]
	// The target score required for a player to win
	public int targetWinScore = 0;
	
	public int winningPlayerNumber = 0;

	// [ROUND INFORMATION] -------------------------------------------------------------------------------
	[Header("Round Information")]
	// Round number
	public float roundNumber = 0;
	
	public GameObject roundNumberObject;
	
	Text roundNumberText;

	// Round countdown
	public bool showCountdown = true;
	public bool countdownActive = false;

	public float roundCountdown = 10;
	int roundCountdownInt;

	public GameObject roundCountdownTextObject;
	public GameObject roundCountdownLabel;

	Text roundCountdownText;
	

	// [MODIFIER INFORMATION] -------------------------------------------------------------------------------
	[Header("Modifers")]
	public string modifierLBString;
	public string modifierRBString;

	public GameObject modifierLBTextObject;
	public GameObject modifierRBTextObject;

	Text modifierLBText;
	Text modifierRBText;

	public bool modifierLBActive = false;
	public bool modifierRBActive = false;
	public bool modifierLBUsed = false;
	public bool modifierRBUsed = false;

	public GameObject modifierLBSprite;
	public GameObject modifierRBSprite;

	Sprite modifierLBActiveIcon;
	Sprite modifierLBInactiveIcon;
	Sprite modifierRBActiveIcon;
	Sprite modifierRBInactiveIcon;

	// [ROUND WINNER] --------------------------------------------------------------------------------------
	[Header("Round Winner")]


	public int roundWinnerPlayerNumber;

	public GameObject roundWinnerTextObject;

	Text roundWinnerText;

	public GameObject roundWinnerPlayerIconObject;
	public GameObject roundWinnerPlayerHeadObject;

	public Sprite roundWinnerPlayerIcon;
	public Sprite roundWinnerPlayerHead;

	// [GAMEMODE] --------------------------------------------------------------------------------------
	[Header("Gamemode")]

	public string gamemodeString;
	public string gamemodeDescriptionString;

	public GameObject gamemodeTextObject;
	public GameObject gamemodeDescriptionTextObject;

	Text gamemodeText;
	Text gamemodeDescriptionText;

	// Pre-round countdown
	public float roundStartCountdown = 3;
	public bool preRoundCountdownActive = false;
	public GameObject roundStartCountdownTextObject;
	Text roundStartCountdownText;

	// [COLOURS] --------------------------------------------------------------------------------------
	Color transparent = new Color(1.0f, 1.0f, 1.0f, 0.35f);
	Color nonTransparent = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	Color orangeAlert = new Color (1.0f, 0.8f, 0.0f, 1.0f);

	// [DISPLAY BOOLS] --------------------------------------------------------------------------------------
	[Header("Show Elements")]
	public bool showMain = false;
	public bool showPreRound = false;
	public bool showRoundWinner = false;
	public bool showChoiceScreen = false;

	// [CHOICE SCREEN] --------------------------------------------------------------------------------------
	[Header("Choice Screen")]

	// New dictator information
	public GameObject newDictatorIconObject;
	public GameObject newDictatorHeadObject;
	public GameObject newDictatorTextObject;

	Text newDictatorText;

	// Gamemode choices text
	public GameObject gamemodeChoiceATextObject;
	public GameObject gamemodeChoiceBTextObject;
	public GameObject gamemodeChoiceCTextObject;
	public GameObject gamemodeChoiceDTextObject;

	Text gamemodeChoiceAText;
	Text gamemodeChoiceBText;
	Text gamemodeChoiceCText;
	Text gamemodeChoiceDText;

	// Modifiers choice text
	public GameObject modifierATextObject;
	public GameObject modifierBTextObject;
	public GameObject modifierCTextObject;
	public GameObject modifierDTextObject;
	public GameObject modifierETextObject;
	public GameObject modifierFTextObject;
	public GameObject modifierGTextObject;
	public GameObject modifierHTextObject;

	Text modifierAText;
	Text modifierBText;
	Text modifierCText;
	Text modifierDText;
	Text modifierEText;
	Text modifierFText;
	Text modifierGText;
	Text modifierHText;

	// Remaining time
	float choiceTimerCountdown = 10;

	public GameObject choiceTimerTextObject;

	Text choiceTimerText;

	// Use this for initialization
	void Start () 
	{
		// Load and assign the required assets
		modifierLBActiveIcon = Resources.Load <Sprite> ("UI/Controller Buttons/ControllerButton_LB_On");
		modifierLBInactiveIcon = Resources.Load <Sprite> ("UI/Controller Buttons/ControllerButton_LB_Off");
		modifierRBActiveIcon = Resources.Load <Sprite> ("UI/Controller Buttons/ControllerButton_RB_On");
		modifierRBInactiveIcon = Resources.Load <Sprite> ("UI/Controller Buttons/ControllerButton_RB_Off");

		// Assign the text components to our text variables
		player1ScoreText = player1ScoreObject.GetComponent<Text> ();
		player2ScoreText = player2ScoreObject.GetComponent<Text> ();
		player3ScoreText = player3ScoreObject.GetComponent<Text> ();
		player4ScoreText = player4ScoreObject.GetComponent<Text> ();

		roundNumberText = roundNumberObject.GetComponent<Text> ();
		roundCountdownText = roundCountdownTextObject.GetComponent<Text> ();

		modifierLBText = modifierLBTextObject.GetComponent<Text> ();
		modifierRBText = modifierRBTextObject.GetComponent<Text> ();

		roundWinnerText = roundWinnerTextObject.GetComponent<Text> ();

		gamemodeText = gamemodeTextObject.GetComponent<Text> ();
		gamemodeDescriptionText = gamemodeDescriptionTextObject.GetComponent<Text> ();

		roundStartCountdownText = roundStartCountdownTextObject.GetComponent<Text> ();

		newDictatorText = newDictatorTextObject.GetComponent<Text> ();

		gamemodeChoiceAText = gamemodeChoiceATextObject.GetComponent<Text> ();
		gamemodeChoiceBText = gamemodeChoiceBTextObject.GetComponent<Text> ();
		gamemodeChoiceCText = gamemodeChoiceCTextObject.GetComponent<Text> ();
		gamemodeChoiceDText = gamemodeChoiceDTextObject.GetComponent<Text> ();

		modifierAText = modifierATextObject.GetComponent<Text> ();
		modifierBText = modifierBTextObject.GetComponent<Text> ();
		modifierCText = modifierCTextObject.GetComponent<Text> ();
		modifierDText = modifierDTextObject.GetComponent<Text> ();
		modifierEText = modifierETextObject.GetComponent<Text> ();
		modifierFText = modifierFTextObject.GetComponent<Text> ();
		modifierGText = modifierGTextObject.GetComponent<Text> ();
		modifierHText = modifierHTextObject.GetComponent<Text> ();

		choiceTimerText = choiceTimerTextObject.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () 
	{
		// Display and update all of the main game UI if true
		if (showMain) 
		{
			mainGameUI.SetActive (true);

			UpdatePlayerScoreUI ();
			UpdatePlayerSprites ();
			UpdateRoundUI ();
			UpdateModifierUI ();
		} 
		else 
		{
			mainGameUI.SetActive (false);
		}

		// Display the round countdown UI elements if true
		if (showCountdown)
		{
			roundCountdownTextObject.SetActive(true);
			roundCountdownLabel.SetActive(true);

			// Start and update the round countdown if true
			if (countdownActive && roundCountdown > 0)
			{
				roundCountdown -= Time.deltaTime;

				UpdateRoundCountdownUI ();
			}
			else
			{
				// - TEMPORARY RESET: REMOVE TO STOP RESET -
				roundCountdown = 30;

			}
		}
		else
		{
			roundCountdownTextObject.SetActive(false);
			roundCountdownLabel.SetActive(false);
		}

		// Display the round winner UI elements if true
		if (showRoundWinner) 
		{
			roundWinnerPlayerIconObject.SetActive (true);
			roundWinnerPlayerHeadObject.SetActive (true);
			roundWinnerTextObject.SetActive (true);
			UpdateRoundWinnerUI ();
		} 
		else 
		{
			roundWinnerPlayerIconObject.SetActive (false);
			roundWinnerPlayerHeadObject.SetActive (false);
			roundWinnerTextObject.SetActive (false);
		}

		// Display the pre-round UI elements if true
		if (showPreRound) 
		{
			gamemodeTextObject.SetActive (true);
			gamemodeDescriptionTextObject.SetActive (true);
			roundStartCountdownTextObject.SetActive (true);

			// Start and update the pre-round countdown if true
			if (preRoundCountdownActive && roundStartCountdown > 0)
			{
				roundStartCountdown -= Time.deltaTime;
				
				UpdatePreRoundInfo();
			}
			else
			{
				// - TEMPORARY RESET: REMOVE TO STOP RESET -
				roundStartCountdown = 3;
			}


		} 
		else 
		{
			gamemodeTextObject.SetActive (false);
			gamemodeDescriptionTextObject.SetActive (false);
			roundStartCountdownTextObject.SetActive (false);
		}

		// Update choice screen if canvas is active
		if (showChoiceScreen) 
		{
			choiceScreenUI.SetActive (true);
			UpdateChoiceScreen ();

			if (choiceTimerCountdown > 0)
			{
				choiceTimerCountdown -= Time.deltaTime;
			}
			else
			{
			}

		} 
		else 
		{
			choiceScreenUI.SetActive (false);
		}

		// WINNER CHECK (UNFINISHED / MISPLACED!)
		CheckForWinner ();

		if (PlayerHasBecomeWinner ()) 
		{
			// Debug.Log ("YES");
		} 
		else 
		{
			// Debug.Log ("NO");
		}
	}

	// Used for updating only the player score text
	void UpdatePlayerScoreUI()
	{
		player1ScoreText.text = player1Score.ToString ();
		player2ScoreText.text = player2Score.ToString ();
		player3ScoreText.text = player3Score.ToString ();
		player4ScoreText.text = player4Score.ToString ();
	}

	// Used for updating the player's head sprite for UI
	void UpdatePlayerSprites()
	{
		player1SpriteObject.GetComponent<Image>().sprite = player1Sprite;
		player2SpriteObject.GetComponent<Image>().sprite = player2Sprite;
		player3SpriteObject.GetComponent<Image>().sprite = player3Sprite;
		player4SpriteObject.GetComponent<Image>().sprite = player4Sprite;
	}

	// Used for updating the current round number text
	void UpdateRoundUI()
	{
		roundNumberText.text = "ROUND " + roundNumber.ToString ();
	}

	// Used for updating the round countdown text
	void UpdateRoundCountdownUI()
	{
		roundCountdownText.text = roundCountdown.ToString("0.0");
	}

	// Used for updating the modifer text and sprites
	void UpdateModifierUI()
	{
		modifierLBText.text = modifierLBString;
		modifierRBText.text = modifierRBString;


		// Change modifier button sprite depending on if it is active or not
		if (modifierLBActive) 
		{
			modifierLBSprite.GetComponent<Image> ().sprite = modifierLBActiveIcon;
			modifierLBText.color = orangeAlert;
		} 
		else 
		{
			modifierLBSprite.GetComponent<Image> ().sprite = modifierLBInactiveIcon;
		}

		if (modifierRBActive) 
		{
			modifierRBSprite.GetComponent<Image> ().sprite = modifierRBActiveIcon;
			modifierRBText.color = orangeAlert;
		} 
		else 
		{
			modifierRBSprite.GetComponent<Image> ().sprite = modifierRBInactiveIcon;
		}

		// Change modifier button and text transparency if modifier has been used
		if (modifierLBUsed) 
		{
			modifierLBSprite.GetComponent<Image> ().color = transparent;
			modifierLBText.color = transparent;
		} 
		else 
		{
			modifierLBSprite.GetComponent<Image> ().color = nonTransparent;

			if (!modifierLBActive)
			{
				modifierLBText.color = nonTransparent;
			}

		}

		if (modifierRBUsed) 
		{
			modifierRBSprite.GetComponent<Image> ().color = transparent;
			modifierRBText.color = transparent;
		} 
		else 
		{
			modifierRBSprite.GetComponent<Image> ().color = nonTransparent;

			if (!modifierRBActive)
			{
				modifierRBText.color = nonTransparent;
			}

		}

	}

	// Used for updating the round winner sprites and text based on the winner's player number
	void UpdateRoundWinnerUI()
	{
		
		if (roundWinnerPlayerNumber == 1) 
		{
			roundWinnerPlayerIcon = player1Icon;
			roundWinnerPlayerHead = player1Sprite;
		}
		
		if (roundWinnerPlayerNumber == 2) 
		{
			roundWinnerPlayerIcon = player2Icon;
			roundWinnerPlayerHead = player2Sprite;
		}
		
		if (roundWinnerPlayerNumber == 3) 
		{
			roundWinnerPlayerIcon = player3Icon;
			roundWinnerPlayerHead = player3Sprite;
		}
		
		if (roundWinnerPlayerNumber == 4) 
		{
			roundWinnerPlayerIcon = player4Icon;
			roundWinnerPlayerHead = player4Sprite;
		}
		
		roundWinnerPlayerIconObject.GetComponent<Image> ().sprite = roundWinnerPlayerIcon;
		roundWinnerPlayerHeadObject.GetComponent<Image> ().sprite = roundWinnerPlayerHead;
		
		roundWinnerText.text = "PLAYER " + roundWinnerPlayerNumber.ToString () + " WINS THE ROUND!";
		
	}

	// Used for updating the pre-round text
	void UpdatePreRoundInfo()
	{
		gamemodeText.text = gamemodeString;
		gamemodeDescriptionText.text = gamemodeDescriptionString;
		roundStartCountdownText.text = roundStartCountdown.ToString ("0.0");
	}

	void UpdateChoiceScreen()
	{
		// Since we set the icon and head for round winner, re-use for Dictator Choice header
		newDictatorIconObject.GetComponent<Image> ().sprite = roundWinnerPlayerIcon;
		newDictatorHeadObject.GetComponent<Image> ().sprite = roundWinnerPlayerHead;
		newDictatorText.text = "PLAYER " + roundWinnerPlayerNumber.ToString ();

		// Change the gamemode text (should be replaced with randomised gamemode(s))
		gamemodeChoiceAText.text = "GAMEMODE A*";
		gamemodeChoiceBText.text = "GAMEMODE B*";
		gamemodeChoiceCText.text = "GAMEMODE C*";
		gamemodeChoiceDText.text = "GAMEMODE D*";

		// Change the modifier text (should be replaced with randomised modifier(s))
		modifierAText.text = "MODIFIER A*";
		modifierBText.text = "MODIFIER B*";
		modifierCText.text = "MODIFIER C*";
		modifierDText.text = "MODIFIER D*";
		modifierEText.text = "MODIFIER E*";
		modifierFText.text = "MODIFIER F*";
		modifierGText.text = "MODIFIER G*";
		modifierHText.text = "MODIFIER H*";

		choiceTimerText.text = "TIME REMAINING: " + choiceTimerCountdown.ToString ("0.0");
	}

	// Checks if a player has reached the target score then assigns the winning player number accordingly
	void CheckForWinner()
	{
		if (player1Score >= targetWinScore) 
		{
			winningPlayerNumber = 1;
		} 
		else if (player2Score >= targetWinScore) 
		{
			winningPlayerNumber = 2;
			roundWinnerPlayerHead = player2Sprite;
		} 
		else if (player3Score >= targetWinScore) 
		{
			winningPlayerNumber = 3;
			roundWinnerPlayerHead = player3Sprite;
		} 
		else if (player4Score >= targetWinScore) 
		{
			winningPlayerNumber = 4;
			roundWinnerPlayerHead = player4Sprite;
		} 
		else 
		{
			winningPlayerNumber = 0;
		}
	}

	bool PlayerHasBecomeWinner()
	{
		bool winnerAvailable = false;

		if (winningPlayerNumber > 0) 
		{
			winnerAvailable = true;
		}

		return winnerAvailable;
	}

}