using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using MoralisUnity;
using MoralisUnity.Web3Api.Models;

public class GameManager : MonoBehaviour 
{
	public static string ContractAddress = "0xd9145CCE52D386f254917e481eB44e9943F39138";
	public static string ContractAbi = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"ApprovalForAll\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"_tokenId\",\"type\":\"uint256\"},{\"internalType\":\"string\",\"name\":\"_tokenUrl\",\"type\":\"string\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"buyItem\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"},{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"safeBatchTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"setApprovalForAll\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"},{\"indexed\":false,\"internalType\":\"uint256[]\",\"name\":\"values\",\"type\":\"uint256[]\"}],\"name\":\"TransferBatch\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"TransferSingle\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"string\",\"name\":\"value\",\"type\":\"string\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"URI\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address[]\",\"name\":\"accounts\",\"type\":\"address[]\"},{\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"}],\"name\":\"balanceOfBatch\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"\",\"type\":\"uint256[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"}],\"name\":\"isApprovedForAll\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes4\",\"name\":\"interfaceId\",\"type\":\"bytes4\"}],\"name\":\"supportsInterface\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"uri\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
	public static ChainList ContractChain = ChainList.mumbai;

	public Ball curBall;			//The current ball that is being hit. (in multiplayer, whoever's turn it currently is. Their ball)

	//Multiplayer
	public bool isMultiplayer;		//Is this a multiplayer game?
	public Player[] players;		//Array of all the players in the game.

	public int playerCount;			//The amount of players in the multiplayer game.
	public int curPlayerTurn;		//The player id whose turn it currently is.
	public int ballsLeft;			//The amount of player balls left, playing on the course.

	//Course
	public Course course;			//The course class which contains all the info about the course.
	public GameObject courseParent;	//The parent game object that holds all the holes and their tiles.
	public bool courseFinished;		//Set to true when the course has been complete.

	//Current Hole
	public int curHole;

	//UI
	public GameUI ui;

	[Space(50)]

	//Tile Prefabs
	public GameObject course_1;
	public GameObject course_2;
	public GameObject course_3;
	public GameObject course_4;
	public GameObject course_5;
	public GameObject course_6;
	public GameObject course_7;
	public GameObject course_8;
	public GameObject course_9;
	public GameObject course_10;
	public GameObject course_11;
	public GameObject course_12;
	public GameObject course_13;
	public GameObject course_14;
	public GameObject course_15;
	public GameObject course_16;
	public GameObject course_17;
	public GameObject course_18;
	public GameObject course_19;
	public GameObject course_20;
	public GameObject course_21;
	public GameObject course_22;
	public GameObject course_23;
    public GameObject course_24;

	public GameObject golfBallPrefab;
	public GameObject holePrefab;

	void Start ()
	{
		//The below player prefs are loaded in from when they were set on the main menu.

		isMultiplayer = PlayerPrefs.GetInt("IsMultiplayer") == 1? true : false;

		if(isMultiplayer){
			playerCount = PlayerPrefs.GetInt("PlayerCount");
			players = new Player[playerCount];

			for(int x = 0; x < playerCount; x++){
				players[x] = new Player();
				players[x].name = PlayerPrefs.GetString("Player" + x + "Name");
			}
		}else{
			playerCount = 1;
			players = new Player[1];
			players[0] = new Player();
		}

		LoadCourse(PlayerPrefs.GetString("Course"));
	}

	//Called when the scene gets launched. It loads in the file containing the course.
	void LoadCourse (string courseName)
	{
		//READING XML FILE

		XmlSerializer serializer = new XmlSerializer(typeof(Course));
		TextAsset c = Resources.Load("Courses/" + courseName) as TextAsset;
		Stream stream = new MemoryStream(c.bytes);
		course = serializer.Deserialize(stream) as Course;
		stream.Close();

		BuildCourse();
	}

	//Called after LoadCourse(). This builds the course by spawning in the tiles, balls and holes.
	void BuildCourse ()
	{
		if(isMultiplayer){
			//Creates a ball for each player.
			for(int p = 0; p < playerCount; p++){
				GameObject ballObj = Instantiate(golfBallPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				ballObj.name = "Player" + p;
				ballObj.transform.parent = courseParent.transform;

				ballObj.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);

				//Setting up the ball.
				Ball b = ballObj.GetComponent<Ball>();
				b.gameManager = this;
				players[p].ball = b;

				//Setting the player's score array to be the same size as how many holes there are.
				players[p].scores = new int[course.holes.Length];
			}

			//Setting up variables.
			curBall = players[0].ball;
			curPlayerTurn = 0;
			ballsLeft = playerCount;

			//Setting up ui stuff.
			ui.playerTurnText.gameObject.SetActive(true);
			ui.playerTurnText.text = players[curPlayerTurn].name + "'s Turn";
			ui.UpdatePlayerStrokeUI();
			ui.SetPlayerNameTags();
		}else{
			//Creates the ball.
			GameObject ballObj = Instantiate(golfBallPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			ballObj.name = "GolfBall";
			ballObj.transform.parent = courseParent.transform;

			//Setting up the ball.
			Ball b = ballObj.GetComponent<Ball>();
			b.gameManager = this;
			players[0].ball = b;
			curBall = players[0].ball;

			//Setting the player's score array to be the same size as how many holes there are.
			players[0].scores = new int[course.holes.Length];

			//Disabling the turn text as we don't need that in single player.
			ui.playerTurnText.gameObject.SetActive(false);
		}

		//Loops through all the holes in the course.
		for(int x = 0; x < course.holes.Length; x++){
			GameObject holeContainer = new GameObject("Hole" + (x + 1));

			holeContainer.transform.parent = courseParent.transform;
			holeContainer.transform.position = new Vector3(0 + (15 * (x)), 0, 0);

			//Loops through all the tiles that make up the hole.
			for(int t = 0; t < course.holes[x].tiles.Length; t++){
				Tile tile = course.holes[x].tiles[t];

				GameObject spawnPrefab = GetTilePrefab(tile.spriteName);

				if(spawnPrefab != null){
					//Creates the tile.
					GameObject tileObj = Instantiate(spawnPrefab, tile.position, Quaternion.Euler(0, 0, tile.rotation)) as GameObject; 
					tileObj.transform.parent = holeContainer.transform;
				}
			}

			//Creates and places the hole.
			GameObject holeObj = Instantiate(holePrefab, course.holes[x].holePosition, Quaternion.identity) as GameObject;
			holeObj.name = "Hole" + (x + 1);
			holeObj.transform.parent = holeContainer.transform;
		}


		//Loops through all the balls and sets their position to be the start position on the first hole.
		for(int b = 0; b < players.Length; b++){
			players[b].ball.transform.position = course.holes[0].ballStartPosition;
		}

		//Sets the current hole to be the first one.
		curHole = 1;

		//Sets the UI stuff to display the course info in the top left. Also scoreboard names.
		ui.courseName.text = course.courseName;	
		ui.courseInfo.text = "Hole: " + curHole + "/" + course.holes.Length + "\nPar: " + course.holes[curHole - 1].par;
		ui.SetScoreBoardPlayers();
	}

	void Update ()
	{
		//If we press the escape key, then quit to the menu.
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene("Menu");
		}

		//Is the course currently in progress?
		if(!courseFinished){
			//If we press down on the TAB key, the scoreboard appears.
			if(Input.GetKeyDown(KeyCode.Tab)){
				ui.SetScoreBoard();
			}

			//If we press up on the TAB key, the scoreboard disapears.
			if(Input.GetKeyUp(KeyCode.Tab)){
				ui.scoreBoard.SetActive(false);
			}
		}
	}

	//Called when the ball sinks into the hole.
	public void SinkBall (int id)
	{
		//We can disable the ball since it is now in the hole.
		players[id].ball.gameObject.SetActive(false);
		ui.holeScoreText.text = GetHoleScore(id);

		//Add the player's stroke to their score.
		players[id].scores[curHole - 1] = players[id].curStroke;

		//Play the hole score pop up text.
		ui.StartCoroutine(ui.HoleScoreTextPopUp());

		if(isMultiplayer){
			ballsLeft--;
			players[id].sunkBall = true;

			if(ballsLeft >= 1){
				NextPlayerTurn();
			}else{
				if(curHole == course.holes.Length){
					CourseComplete();
				}else{
					NextHole();
				}
			}
		}else{
			if(curHole == course.holes.Length){
				CourseComplete();
			}else{
				NextHole();
			}
		}
	}

	//Called after the SinkBall() function. This sets up the next hole and lerps the camera over to it.
	void NextHole ()
	{
		//Setting the player's balls to be not sunk. Setting the player's cur stroke to 0.
		for(int x = 0; x < players.Length; x++){
			players[x].sunkBall = false;
			players[x].curStroke = 0;
		}

		curHole++;
		ui.courseInfo.text = "Hole: " + curHole + "/" + course.holes.Length + "\nPar: " + course.holes[curHole - 1].par;

		//Loops through the players and makes their balls visible and sets their positions.
		for(int x = 0; x < players.Length; x++){
			players[x].ball.gameObject.SetActive(true);
			players[x].ball.transform.localPosition = course.holes[curHole - 1].ballStartPosition;
		}

		//If it's multiplayer, then we can set the current players turn to be the first player.
		if(isMultiplayer){
			ballsLeft = playerCount;
			curPlayerTurn = 0;
			curBall = players[curPlayerTurn].ball;
			ui.playerTurnText.text = players[curPlayerTurn].name + "'s Turn";
		}

		//Updating the stoke UI causes the strokes to be displayed as 0 on the screen, since it's a new hole.
		ui.UpdatePlayerStrokeUI();

		//Move the camera over to the next hole.
		StartCoroutine(LerpToNextHole());
	}

	//This lerps the camera over to the next hole after the ball has been sunk.
	IEnumerator LerpToNextHole ()
	{
		yield return new WaitForSeconds(1.5f);

		Vector3 targetPos = new Vector3(-15 * (curHole - 1), 0, 0);
		int hole = curHole;

		while(courseParent.transform.position != targetPos){
			courseParent.transform.position = Vector3.Lerp(courseParent.transform.position, targetPos, Time.deltaTime * 5);

			//This is here, so that if this co routine gets called again while this one
			//is still currently going, this one will stop. This is because if 2 of these
			//co routines are going at once, it would result in the course not moving properly.
			if(curHole != hole){
				break;
			}

			yield return null;
		}
	}

	//Called once all the holes have been completed on the course.
	//Enables the scoreboard and sets a timer for 10 seconds before
	//Returning to the main menu.
	void CourseComplete ()
	{
		courseFinished = true;
		ui.SetScoreBoard();
		StartCoroutine(CourseCompleteTimer());
	}

	//Called by the above function. This waits 10 seconds then returns
	//to the main menu.
	IEnumerator CourseCompleteTimer ()
	{
		yield return new WaitForSeconds(10);

		SceneManager.LoadScene("Menu");
	}

	//If multiplayer. Called after a player hits their ball. Enables the next player
	//to be able to hit their ball.
	public void NextPlayerTurn ()
	{
		curPlayerTurn++;

		if(curPlayerTurn == playerCount){
			curPlayerTurn = 0;
		}

		if(players[curPlayerTurn].sunkBall){
			NextPlayerTurn();
			return;
		}

		curBall = players[curPlayerTurn].ball;
		ui.playerTurnText.text = players[curPlayerTurn].name + "'s Turn";
	}

	//Returns a tile prefab from the name sent over. e.g. "course_grass" returns Course_Grass.prefab.
	GameObject GetTilePrefab (string name)
	{
		switch(name){
			case "Course_1":{
				return course_1;

			}
			case "Course_2":{
				return course_2;

			}
			case "Course_3":{
				return course_3;

			}
			case "Course_4":{
				return course_4;

			}
			case "Course_5":{
				return course_5;

			}
			case "Course_6":{
				return course_6;

			}
			case "Course_7":{
				return course_7;

			}
			case "Course_8":{
				return course_8;

			}
			case "Course_9":{
				return course_9;

			}
			case "Course_10":{
				return course_10;

			}
			case "Course_11":{
				return course_11;

			}
			case "Course_12":{
				return course_12;

			}
			case "Course_13":{
				return course_13;

			}
			case "Course_14":{
				return course_14;

			}
			case "Course_15":{
				return course_15;

			}
			case "Course_16":{
				return course_16;

			}
			case "Course_17":{
				return course_17;

			}
			case "Course_18":{
				return course_18;

			}
			case "Course_19":{
				return course_19;

			}
			case "Course_20":{
				return course_20;

			}
			case "Course_21":{
				return course_21;

			}
			case "Course_22":{
				return course_22;

			}
			case "Course_23":{
				return course_23;

			}
            case "Course_24":
            {
                return course_24;

            }
        }

		//If the sent sprite name doesn't match up with any of the prefabs
		//Then we just return null.
		return null;
	}

	//Returns the names given to scores on holes relative to par.
	public string GetHoleScore (int id)
	{
		int par = course.holes[curHole - 1].par;

		if(players[id].curStroke >= par + 4)
			return players[id].curStroke + " Strokes";

		if(players[id].curStroke == par + 3)
			return "Triple-Bogey";

		if(players[id].curStroke == par + 2)
			return "Double-Bogey";

		if(players[id].curStroke == par + 1)
			return "Bogey";

		if(players[id].curStroke == par)
			return "Par";

		if(players[id].curStroke == par - 1)
			return "Birdie";

		if(players[id].curStroke == par - 2)
			return "Eagle";

		if(players[id].curStroke == par - 3)
			return "Albatross";

		if(players[id].curStroke == par - 4)
			return "Condor";

		return "";
	}
}
