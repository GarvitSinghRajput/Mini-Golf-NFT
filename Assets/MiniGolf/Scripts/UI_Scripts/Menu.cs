using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Menu : MonoBehaviour 
{
	public string[] courseNames;			//Array which will contain all the course names so that they can be loaded onto buttons.
	public string selectedCourse;			//The name of the selected course.

	//Pages
	public GameObject menuPage;		
	public GameObject playPage;

	//Multiplayer
	public bool isMultiplayer;
	public bool isOnlineMultiplayer;
	public int playerCount;

	//UI - Play
	public Button[] courseButtons;
	public Text selectedCourseText;

	//UI - Multiplayer
	public GameObject multiplayerOptions;	//The object holding the multiplayer options. Enabled or disabled depending on mode.
	public GameObject onlineMultiplayerOptions; //The object holding the Online Matchmaking options. Enabled or disabled depending on mode.
	public GameObject searchingPanel;
	public Text playerCountText;			//The text displaying how many players you have selected to play the game.
	public InputField[] playerNameInputs;	//Array containing all the input fields which you enter player names into.

	void Start ()
	{
		SetCourseButtons();
	}

	//Called when the scene loads. This function sets up the course buttons on
	//the play page so that the user can select one to play.
	void SetCourseButtons ()
	{
		courseNames = new string[Resources.LoadAll("Courses").Length];

		//Getting names of each course.
		for(int x = 0; x < courseNames.Length; x++){
			courseNames[x] = Resources.LoadAll("Courses")[x].name;
		}

		//Loops through the course buttons to set them up.
		for(int b = 0; b < courseButtons.Length; b++){
			if(b < courseNames.Length){
				courseButtons[b].gameObject.SetActive(true);

				//Deserializing the course to know how many holes it has.
				XmlSerializer serializer = new XmlSerializer(typeof(Course));
				TextAsset c = Resources.Load("Courses/" + courseNames[b]) as TextAsset;
				Stream stream = new MemoryStream(c.bytes);
				Course course = serializer.Deserialize(stream) as Course;
				stream.Close();

				//Setting the button text.
				courseButtons[b].transform.Find("Text").GetComponent<Text>().text = "<b>" + course.courseName + "</b>\n" + course.holes.Length + " Holes";
			}else{
				courseButtons[b].gameObject.SetActive(false);
			}
		}
	}

	//Called when you want to switch between pages by clicking on the menu buttons.
	//It sends over the page name to change to.
	public void SetPage (string page)
	{
		switch(page){
			case "menu":{
					menuPage.SetActive(true);
					playPage.SetActive(false);
				break;
			}
			case "play":{
					menuPage.SetActive(false);
					playPage.SetActive(true);
					isMultiplayer = false;
					isOnlineMultiplayer = false;
					multiplayerOptions.SetActive(false);
					onlineMultiplayerOptions.SetActive(false);
					searchingPanel.SetActive(false);
					break;
			}
			case "playmultiplayer":{
					menuPage.SetActive(false);
					playPage.SetActive(true);
					isMultiplayer = true;
					isOnlineMultiplayer = false;
					multiplayerOptions.SetActive(true);
					onlineMultiplayerOptions.SetActive(false);
					searchingPanel.SetActive(false);
					break;
			}
			case "onlineMultiplayer":
                {
					menuPage.SetActive(false);
					playPage.SetActive(true);
					isMultiplayer = false;
					isOnlineMultiplayer = true;
					multiplayerOptions.SetActive(false);
					onlineMultiplayerOptions.SetActive(true);
					searchingPanel.SetActive(false);
					break;
                }
		}
	}

	//Called when a course select button is pressed. It sends over an index number
	//Which correlates to a course name.
	public void SelectCourse (int buttonId)
	{
		selectedCourse = courseNames[buttonId];
		int courseHoles = int.Parse(courseButtons[buttonId].transform.Find("Text").GetComponent<Text>().text.Replace("<b>" + courseNames[buttonId] + "</b>\n", "").Replace(" Holes", ""));
		selectedCourseText.text = courseNames[buttonId] + " - " + courseHoles + " Holes";
	}

	//In the play page, this gets called when the player count slider gets changed.
	//It updates the text to the right of it, displaying the player count.
	public void UpdatePlayerCount (Slider slider)
	{
		playerCount = (int)slider.value;
		playerCountText.text = playerCount.ToString();
		SetPlayerNameInputs();
	}

	//Either enables or disables player name inputs depending on the player count.
	//Called whenever the player count slider gets changed.
	void SetPlayerNameInputs ()
	{
		for(int x = 0; x < playerNameInputs.Length; x++){
			if(x < playerCount){
				playerNameInputs[x].gameObject.SetActive(true);
			}else{
				playerNameInputs[x].gameObject.SetActive(false);
			}
		}
	}

	//Called when the "Play" button is pressed on the play page.
	//It sets player prefs and launches the Game scene.
	public void PlayCourse ()
	{
		//We need to check if the user has selected a course. 
		//We do this by just checking if the selectedCourse name length is more than 1.
		if(selectedCourse.Length > 1){
			PlayerPrefs.SetString("Course", selectedCourse);
			PlayerPrefs.SetInt("IsMultiplayer", 0);	//0 means no.

			//Are we playing multiplayer?
			if(isMultiplayer){
				PlayerPrefs.SetInt("IsMultiplayer", 1);	//1 means yes.
				PlayerPrefs.SetInt("PlayerCount", playerCount);

				//Saving each player name to player prefs.
				for(int x = 0; x < playerCount; x++){
					PlayerPrefs.SetString("Player" + x + "Name", playerNameInputs[x].text);
				}
			}
		}

        //Finally, we will load the Game level.
        if (isOnlineMultiplayer)
        {
			PlayerPrefs.SetInt("IsMultiplayer", 1); //1 means yes.
			PlayerPrefs.SetInt("PlayerCount", 2);

			PhotonNetwork.LoadLevel("Game");
        }
		SceneManager.LoadScene("Game");
	}

	//Called when the "Quit" button gets pressed.
	//It quits out of the game.
	public void QuitGame ()
	{
		Application.Quit();
	}
}
