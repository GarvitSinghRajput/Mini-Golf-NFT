using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class CourseCreator : MonoBehaviour 
{
	public Course course;					//The course class which will hold the info of this course.

	public Sprite curTile;
	public GameObject courseHolesParent;	//The parent gameobject which holds all the course holes and their tiles.
	public GameObject[] courseHoles;		//Array of all the course holes which hold the tiles.
	public int curHole;						//The current course hole that is being edited.
	public int holeCount;					//How many holes in total does this course have? 9 or 18.

	public GameObject[] placeableBalls;		//Array holding all the placeable balls for each hole.
	public GameObject[] placeableHoles;		//Array holding all the placeable holes for each hole.
	public int[] pars;						//The pars for all the holes.

	//Bools
	public bool canEditTiles;				//Can you currently place or remove tiles?
	public bool placingBall;				//Are you currently placing the ball?
	public bool placingHole;				//Are you currently placing the hole?

	//Preview
	public GameObject previewTile;			//The translucent tile that previews the tile you currently have selected.
	public int tileRotation;				//The rotation that the tile will be placed in.

	//UI
	public InputField courseNameInput;		
	public Text curHoleText; 
	public Button[] holeButtons;
	public InputField parInput;

	//UI - New Course Menu
	public GameObject newCourseMenu;
	public Dropdown courseHoleCountDropdown;

	//UI - Load Course Menu
	public GameObject loadCourseMenu;
	public Button[] loadCourseButtons;

	[Space(50)]

	//Sprites
	public Sprite[] courseSprites;			//Array of tile sprites in order. Course_1 = courseSprites[0], etc...

	void Start ()
	{
		course = new Course();
		newCourseMenu.active = true;
		canEditTiles = false;

		SetLoadCourseButtons();
	}

	//Creates a new course and sets up everything.
	public void CreateNewCourse ()
	{
		holeCount = (courseHoleCountDropdown.value + 1) * 9;
		newCourseMenu.active = false;
		pars = new int[holeCount];
		SetCourseHole(1);
		canEditTiles = true;
		course.holes = new CourseHole[holeCount];

		//Depending on how many holes you selected your course to be at the start
		//this loop disables the hole buttons you don't need.
		for(int x = 0; x < holeButtons.Length; x++){
			if(x > holeCount - 1){
				holeButtons[x].gameObject.active = false;
			}
		}
	}

	void Update ()
	{
		//Sends a raycast towards the tiles at the mouse position.
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if(canEditTiles && !placingBall && !placingHole){
			if(hit != null && hit.collider != null){
				previewTile.transform.position = hit.collider.gameObject.transform.position;
			}
		}

		//Drawing tiles or placing down balls or holes.
		//Did we press down on the left mouse button?
		if(Input.GetMouseButtonDown(0)){
			if(hit != null && hit.collider != null){
				if(canEditTiles && !placingBall && !placingHole){
					if(hit.collider.gameObject.tag == "CourseEditorTile"){
						hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = curTile;
						hit.collider.transform.rotation = Quaternion.Euler(0, 0, tileRotation);
					}
				}

				//Are we currently placing down the ball?
				if(placingBall){
					if(hit.collider.gameObject.tag == "CourseEditorTile"){
						placeableBalls[curHole - 1].transform.position = hit.collider.transform.position;
						placingBall = false;
					}
				}

				//Are we currently placing down the hole?
				if(placingHole){
					if(hit.collider.gameObject.tag == "CourseEditorTile"){
						placeableHoles[curHole - 1].transform.position = hit.collider.transform.position;
						placingHole = false;
					}
				}
			}
		}

		//The reason why the above if statment isn't 'if we are holding down the left mouse button', is
		//because if we are placing down the ball or hole, it will also place down a tile.

		//Erasing tiles.
		//Are we holding down the right mouse button?
		if(Input.GetMouseButton(1)){
			if(hit != null && hit.collider != null){
				if(canEditTiles && !placingBall && !placingHole){
					if(hit.collider.gameObject.tag == "CourseEditorTile"){
						hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = null;
					}
				}
			}
		}

		//Changes the rotation of the tiles you put down.
		if(Input.GetKeyDown(KeyCode.R)){
			previewTile.transform.rotation = Quaternion.Euler(0, 0, previewTile.transform.eulerAngles.z - 90);
			tileRotation = (int)previewTile.transform.eulerAngles.z;
		}
	}

	//Sets the current tile that you place down.
	//It sends over the tile button that was pressed and sets the current placable tile
	//to be the sprite that is on the button.
	public void SetCurTile (Button pressedButton)
	{
		curTile = pressedButton.image.sprite;
		previewTile.GetComponent<SpriteRenderer>().sprite = pressedButton.image.sprite;
	}

	//Sets the cameras perspective to the requested hole number.
	//Also sets up other things so that we can edit that hole.
	public void SetCourseHole (int hole)
	{
		courseHolesParent.transform.position = new Vector3(0 - (15 * (hole - 1)), 0, 0);
		curHoleText.text = "Hole: " + hole + "/" + holeCount;
		curHole = hole;
		parInput.text = pars[curHole - 1].ToString();
	}

	//Called when the "Place Ball" button gets pressed.
	//Makes it so that the player can place down the ball.
	public void PlaceBall ()
	{
		placingBall = true;
	}

	//Called when the "Place Hole" button gets pressed.
	//Makes it so that the player can place down the hole.
	public void PlaceHole ()
	{
		placingHole = true;
	}

	//Called after you enter in a par in the par input field.
	//Sets that hole's par.
	public void SetHolePar ()
	{
		int i;

		if(int.TryParse(parInput.text, out i)){
			pars[curHole - 1] = i;
		}
	}

	//SAVING THE COURSE

	//Called when the "Save Course" button is pressed.
	//Saves the course as a serialized XML file which can be de-serialized when wanting to play the course
	//in the GameManager.cs script.
	public void SaveCourse ()
	{
		//CREATING THE COURSE CLASS

		course.courseName = courseNameInput.text;

		//Looping through each hole
		for(int x = 0; x < course.holes.Length; x++){
			CourseHole hole = new CourseHole();

			hole.ballStartPosition = placeableBalls[x].transform.localPosition + new Vector3(0 + (15 * x), 0, 0);
			hole.holePosition = placeableHoles[x].transform.localPosition + new Vector3(0 + (15 * x), 0, 0);
			hole.par = pars[x];

			hole.tiles = new Tile[99];

			//Looping through each tile
			for(int t = 0; t < hole.tiles.Length; t++){
				SpriteRenderer sr = courseHoles[x].transform.GetChild(t).GetComponent<SpriteRenderer>();

				string tileName = "";

				if(sr.sprite != null){
					tileName = sr.sprite.name;
				}

				hole.tiles[t] = new Tile();
				hole.tiles[t].spriteName = tileName;
				hole.tiles[t].position = courseHoles[x].transform.GetChild(t).transform.localPosition + new Vector3(0 + (15 * x), 0, 0);
				hole.tiles[t].rotation = (int)courseHoles[x].transform.GetChild(t).transform.eulerAngles.z;
			}

			course.holes[x] = hole;
		}
			
		//SERIALIZING THE CLASS AS AN XML FILE

		XmlSerializer serializer = new XmlSerializer(typeof(Course));
		FileStream stream = new FileStream("Assets/MiniGolf/Resources/Courses/" + courseNameInput.text + ".xml", FileMode.Create);
		serializer.Serialize(stream, course);
		stream.Close();

		Debug.Log("Saved course as " + courseNameInput.text + ".xml");
	}

	//LOADING THE COURSE

	//Sets up the buttons that allow you to load in a course.
	public void SetLoadCourseButtons ()
	{
		TextAsset[] courses = Resources.LoadAll<TextAsset>("Courses");

		for(int x = 0; x < loadCourseButtons.Length; x++){
			if(x < courses.Length){
				loadCourseButtons[x].gameObject.active = true;
				loadCourseButtons[x].transform.Find("Text").GetComponent<Text>().text = courses[x].name;
			}else{
				loadCourseButtons[x].gameObject.active = false;
			}
		}
	}

	//Called when a load course button is pressed.
	//It sends an id which allocates to its index number when loaded in from the resources folder.
	//The course is then loaded in, with the tiles being placed as well as the balls and holes and other
	//information to do with the course, such as pars and the course name.
	public void LoadCourse (int id)
	{
		string courseToLoadName = Resources.LoadAll<TextAsset>("Courses")[id].name;

		//Deserializing the XML file
		XmlSerializer serializer = new XmlSerializer(typeof(Course));
		FileStream stream = new FileStream("Assets/MiniGolf/Resources/Courses/" + courseToLoadName + ".xml", FileMode.Open);
		Course courseToLoad = serializer.Deserialize(stream) as Course;
		stream.Close();

		courseNameInput.text = courseToLoad.courseName;

		pars = new int[courseToLoad.holes.Length];
		holeCount = courseToLoad.holes.Length;

		//Looping through each of the hole buttons on the right hand side of the screen.
		//Disabling the ones that exceed the amount of holes we have.
		for(int x = 0; x < holeButtons.Length; x++){
			if(x > holeCount - 1){
				holeButtons[x].gameObject.active = false;
			}
		}

		//Looping through each hole and setting up the information about it.
		for(int x = 0; x < courseToLoad.holes.Length; x++){
			CourseHole hole = courseToLoad.holes[x];

			pars[x] = hole.par;
			placeableBalls[x].transform.position = hole.ballStartPosition;
			placeableHoles[x].transform.position = hole.holePosition;

			//Looping through each tile on that hole and setting it to what it should be.
			for(int t = 0; t < hole.tiles.Length; t++){
				GameObject tile = courseHoles[x].transform.GetChild(t).gameObject;

				tile.transform.eulerAngles = new Vector3(0, 0, hole.tiles[t].rotation);

				int i = 0;

				if(hole.tiles[t].spriteName.Contains("Course")){
					if(int.TryParse(hole.tiles[t].spriteName.Replace("Course_", ""), out i)){
						tile.GetComponent<SpriteRenderer>().sprite = courseSprites[i - 1];
					}else{
						tile.GetComponent<SpriteRenderer>().sprite = null;
					}
				}else{
					tile.GetComponent<SpriteRenderer>().sprite = null;
				}
			}
		}

		course = courseToLoad;
		SetCourseHole(1);
		canEditTiles = true;
		newCourseMenu.active = false;
	}

	//Called when you click on the "Load Course" button.
	public void GoToLoadCourseMenu ()
	{
		loadCourseMenu.active = true;
	}

	//Called when you click on the "<" button in the load course menu.
	public void GoBackToNewCourseMenu ()
	{
		loadCourseMenu.active = false;
	}
}
