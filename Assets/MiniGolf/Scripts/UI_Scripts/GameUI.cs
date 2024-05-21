using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour 
{
	//In-Game Info
	public Text courseName;				//The name of the course in the top left corner of the screen.
	public Text courseInfo;				//Text that displays the hole and the par in the top left corner of the screen.
	public Text strokeText;				//The text at the bottom right of the screen that displays the current stroke count of the player(s).

	public Image powerBar;				//The powerbar which visually displays the player's current power on the ball they are going to hit.

	public Text holeScoreText;			//The text that pops up when a ball sinks. Displaying "Par", "Bogey", "Birdie", etc.

	public Text playerTurnText;			//The text at the bottom of the screen displaying the current player whose turn it is.

	public Text[] playerNameTags;		//Array of all the available name tags.

	//Scoreboard
	public GameObject scoreBoard;		//The scoreboard which displays scores.
	public GameObject[] playerScores;	//The individual black bars with the player's name and then their score for each hole.

	//Game Manager
	public GameManager gameManager;

	void Update ()
	{
		//We need to make sure there actually are players in the game and that this code
		//doesn't run before the player's and their balls have been created.
		if(gameManager.players.Length > 0){
			
			//If the game is multiplayer, then this will make sure that the player name tags
			//follow the players. The text disapears when the player's ball goes down a hole.
			if(gameManager.isMultiplayer){
				for(int x = 0; x < gameManager.players.Length; x++){
					if(!gameManager.players[x].sunkBall){
						playerNameTags[x].transform.position = gameManager.players[x].ball.transform.position + new Vector3(0, 0.5f, 0);
						playerNameTags[x].gameObject.active = true;
					}else{
						playerNameTags[x].gameObject.active = false;
					}
				}
			}
		}
	}

	//If the game is multiplayer, then this function gets called in the GameManager.cs script
	//at the start of the game to set up the name tags for the players.
	public void SetPlayerNameTags ()
	{
		//Loop through each name tag and if there is a player that can take it, then
		//make it visible and make the text display the player's name.
		for(int x = 0; x < playerNameTags.Length; x++){
			if(x < gameManager.playerCount){
				playerNameTags[x].gameObject.active = true;
				playerNameTags[x].text = gameManager.players[x].name;
			}
		}
	}

	//Called at the start of the scene. Sets up the scoreboard to display the players on it.
	public void SetScoreBoardPlayers ()
	{
		//If the game isn't multiplayer, then we just have 1 slot with the name set to "Score".
		if(!gameManager.isMultiplayer){
			playerScores[0].active = true;
			playerScores[0].transform.Find("Name").GetComponent<Text>().text = "Score";

			for(int s = 0; s < 18; s++){
				if(s >= gameManager.course.holes.Length){
					playerScores[0].transform.Find("Score").GetChild(s).gameObject.active = false;
				}
			}
		}
		//Otherwise, if it is multiplayer then enable a slot for each player and display their name.
		else{
			for(int x = 0; x < gameManager.players.Length; x++){
				playerScores[x].active = true;
				playerScores[x].transform.Find("Name").GetComponent<Text>().text = gameManager.players[x].name;

				for(int s = 0; s < 18; s++){
					if(s >= gameManager.course.holes.Length){
						playerScores[x].transform.Find("Score").GetChild(s).gameObject.active = false;
					}
				}
			}
		}
	}

	//Called when the scoreboard is opened.
	//It updates each player's score for each hole on the scoreboard.
	public void UpdateScoreBoardPlayers ()
	{
		//Loops through all the players.
		for(int x = 0; x < gameManager.playerCount; x++){
			//Looping through each player's hole scores to add them to the scoreboard.
			for(int s = 0; s < gameManager.course.holes.Length; s++){
				Text scoreTextBox = playerScores[x].transform.Find("Score").GetChild(s).GetComponent<Text>();

				if(gameManager.players[x].scores[s] != 0){
					scoreTextBox.text = gameManager.players[x].scores[s].ToString();
				}else{
					scoreTextBox.text = "";
				}
			}

			//Calculating and displaying the total score for each player.
			int totalScore = 0;

			for(int t = 0; t < gameManager.players[x].scores.Length; t++){
				totalScore += gameManager.players[x].scores[t];
			}

			playerScores[x].transform.Find("TotalScore").GetComponent<Text>().text = totalScore.ToString();
		}
	}

	//Called when the "TAB" key is pressed or when the game ends.
	//It opens up the scoreboard and updates it.
	public void SetScoreBoard ()
	{
		scoreBoard.active = true;
		UpdateScoreBoardPlayers();
	}

	//Called when a ball gets sinked in a hole.
	//An animation that makes the text pop out then back in.
	public IEnumerator HoleScoreTextPopUp ()
	{
		holeScoreText.gameObject.active = true;

		//Make sure that the text is small and invisible.
		holeScoreText.transform.localScale = Vector3.zero;

		//Make the text zoom out towards the camera.
		while(holeScoreText.transform.localScale.x < 0.99f){
			Vector3 scale = holeScoreText.transform.localScale;
			holeScoreText.transform.localScale = Vector3.Lerp(scale, Vector3.one, 10 * Time.deltaTime);
			yield return null;
		}

		//Wait half a second...
		yield return new WaitForSeconds(0.5f);

		//Make the text zoom back into nothing.
		while(holeScoreText.transform.localScale.x > 0.01f){
			Vector3 scale = holeScoreText.transform.localScale;
			holeScoreText.transform.localScale = Vector3.Lerp(scale, Vector3.zero, 10 * Time.deltaTime);
			yield return null;
		}

		//Then disable the text.
		holeScoreText.gameObject.active = false;
	}

	//Updates the stroke text with the player or players current stroke.
	public void UpdatePlayerStrokeUI ()
	{
		//If the game is multiplayer then for each player: display their name, then their current stroke count.
		if(gameManager.isMultiplayer){
			strokeText.text = "";

			for(int x = 0; x < gameManager.playerCount; x++){
				strokeText.text += "\n" + gameManager.players[x].name + "'s Stroke: <size=30>" + gameManager.players[x].curStroke + "</size>";
			}
		}
		//Otherwise, if the game isn't multiplayer, just set the text to say "Score", then the current stroke count.
		else{
			strokeText.text = "Stroke: <size=30>" + gameManager.players[0].curStroke + "</size>";
		}
	}
}
