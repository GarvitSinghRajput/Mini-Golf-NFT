Mini Golf Read Me File
<><><><><><><><><><><><><><>

CONTENTS
--------
>	What is This?
>	How to Play Mini Golf
>	Controls
>	Course Creator
>	Things to Know
>	How do I Add Tiles to the Game and Course Creator?
>	How do I Add More Than 10 Players?
>	Project Layout
>	Contact

<---------------------------------------------->
	What is This?
<---------------------------------------------->

This is a complete project for a mini golf game. This project comes
with the mini golf game, an in-editor course creator and fully
documented scripts. This game is ready to ship and allows you to
both modify and add to the project at ease.

Features
--------
>	Fully working singleplayer and multiplayer mini golf game.
>	Documented scripts.
> 	In-Editor course creator.
> 	Customisable project.



<---------------------------------------------->
	How to Play Mini Golf
<---------------------------------------------->

Mini Golf is a game consisting of courses which each have 8 or 18 holes.
With each hole, you need to hit the ball through it so that it sinks in
the hole. It is very similar to real life mini golf (or putt putt).

You start at the main menu. This is where you can choose a course to play.
You can hit the ball by clicking on it and draging backwards to load up 
the shot. You can then release the mouse button and watch the ball go.

You need to navigate the ball around the course, to sink it in the hole
to move onto the next one.

This game also includes multiplayer (not online though). On the main menu,
you can choose how many players you want, their names and then you each
take turns hitting your ball into the holes. At the end of the course, the
person who has the lowest score wins.



<---------------------------------------------->
	Controls
<---------------------------------------------->

In-Game
--------

>	Hit Ball: 			click and drag with the left mouse button.
>	Open Scoreboard: 	TAB key.
>	Quit to Menu: 		escape key.

Course Creator
--------

>	Rotate Tile: 		R key.



<---------------------------------------------->
	Course Creator
<---------------------------------------------->

With this project, there is a course creator. This is an in-editor scene
which allows you to create courses for the game. (WARNING: the course 
creator can only be used in the editor and not in the final build). 

Once you save your course, you can find it as an XML file, located in
the Resources/Courses folder.

You can also load up a course and continue to edit it by pressing the 
"Load Course" button when playing the CourseCreator scene.



<---------------------------------------------->
	Things to Know
<---------------------------------------------->

>	Don't try and play the course creator scene in the final build, as the courses
	get saves to the Resources folder which can only be done in the editor. You
	can though, change the file path in the CourseCreator.cs script to another
	folder.

>	If you try to run the Game scene in the editor things might not work. This is
	because when the scene runs, it checks for player prefs that would be set on
	the main menu by the player when launching the course. So if you want to run 
	the scene, just play the Menu scene and select the course you want to play from
	there.

>	This game does not have online multiplayer. The multiplayer that this game has,
	is designed for quick play at one computer. Each player takes turns using the
	mouse to hit their ball. Online multiplayer may come in a future update.



<---------------------------------------------->
	How do I Add Tiles to the Game and Course Creator?
<---------------------------------------------->

If you want to add your own tiles into the game it is quite easy.

> 	First, you need to create a 400x400px size sprite of your tile.

>	Name this sprite "Course_" and then the next number in the list of already
	existing tiles.

> 	Then, you need to create a prefab of that sprite, adding in the correct
	colliders and setting the tag to "CourseTile", layer to "Course" and 
	sorting layer to "Course". Name the prefab the same as the sprite.

> 	Then you need to go to the CourseCreator scene and add a tile button to 
	the left hand side panel. Copy/paste one of the existing ones and just
	change the image of the button to your tile sprite.

>	Then go to the _CourseCreator game object and add your title's sprite to
	the courseSprites array in the CourseCreator.cs script.

>	Then go to the GameManager.cs script and with the already existing list of
	tile prefab variables, add your one. e.g. public GameObject course_999;

> 	Then still in the GameManager.cs script, go down to the GetTilePrefab()
	function and add your tile name and variable to the existing switch statement.

> 	Finally add your prefab to your created variable and you should now be able
	to create a course in the course creator and then play it with your new tile.



<---------------------------------------------->
	How do I Add More Than 10 Players?
<---------------------------------------------->

By default, this project can allow for up to 10 players to play the game at once.
If you want to make this number larger, here is how.

>	First, go to the Menu scene and navigate to the PlayersSlider game object. There,
	set the Max Value to how ever many plays you want there to be.

>	Then, go to the PlayerNames game object and add copy/paste the children input fields 
	until there is the amount you need. Then go to the _Menu game object and add those
	new input fields to the playerNameInputs array in the CourseCreator script.

>	Next, go to the Game scene and under the PlayerNameTags game object, copy/paste the
	children like before to the amount you need. Then go to _GameManager object, then
	GameUI script, then add the new player name tags to the playerNameTags array.

>	Then, go to the Scoreboard game object and then its child Players. Like before, copy
	/paste the children to the amount you need then go to the GameUI script again and
	add the new ones to the playerScores array.



<---------------------------------------------->
	Project Layout
<---------------------------------------------->

This is the folder and file layout of the project so it can be easier to comprehend.

> 	MiniGolf
	>	Materials
		>	DragLine.mat
	> 	PhysicsMaterials
		>	GolfBall.physicsMaterial2D
	>	Prefabs
		>	Course
			>	Course_1.prefab
			>	Course_2.prefab
			>	Course_3.prefab
			>	Course_4.prefab
			>	Course_5.prefab
			>	Course_6.prefab
			>	Course_7.prefab
			>	Course_8.prefab
			>	Course_9.prefab
			>	Course_10.prefab
			>	Course_11.prefab
			>	Course_12.prefab
			>	Course_13.prefab
			>	Course_14.prefab
			>	Course_15.prefab
			>	Course_16.prefab
			>	Course_17.prefab
			>	Course_18.prefab
			>	Course_19.prefab
			>	Course_20.prefab
			>	Course_21.prefab
			>	Course_22.prefab
			>	Course_23.prefab
		>	GolfBall.prefab
		>	Hole.prefab
	>	Resources
		>	Courses
			> (XML course files)
	>	Scenes
		>	CourseCreator.unity
		> 	Game.unity
		>	Menu.unity
	>	Scripts
		>	Ball.cs
		>	Course.cs
		>	CourseCreator.cs
		>	CourseHole.cs
		>	GameManager.cs
		>	GameUI.cs
		>	Hole.cs
		>	Menu.cs
		>	MouseManager.cs
		>	Player.cs
		>	Tile.cs
	>	Sprites
		>	Course
			>	Course_1.png
			>	Course_2.png
			>	Course_3.png
			>	Course_4.png
			>	Course_5.png
			>	Course_6.png
			>	Course_7.png
			>	Course_8.png
			>	Course_9.png
			>	Course_10.png
			>	Course_11.png
			>	Course_12.png
			>	Course_13.png
			>	Course_14.png
			>	Course_15.png
			>	Course_16.png
			>	Course_17.png
			>	Course_18.png
			>	Course_19.png
			>	Course_20.png
			>	Course_21.png
			>	Course_22.png
			>	Course_23.png
		>	DragLine.png
		>	GolfBall.png
		>	Hole.png
		>	Title.png
	> 	ReadMe.txt



Tags
--------

>	CourseTile
The tiles that make up the courses have this tag.

>	CourseEditorTile
The tiles that make up the course creator have this tag.

>	Ball
The golf balls have this tag.



Sorting Layers
--------

>	Course
The tiles that make up the course have this sorting layer.

>	Hole
The holes on each course have this sorting layer.

>	Ball
All the balls have this sorting layer.

>	UI
All UI elements have this sorting layer.



Layers
--------

> 	Course

>	Ball



<---------------------------------------------->
	Contact
<---------------------------------------------->

If you need to contact me for any reason to do with this project in terms
of how to use it, future update ideas or any other inquiry, you can 
contact me at any of the following outlets:

>	Email: buckleydaniel101@gmail.com
>	Steam: Sothern

Thank you for purchasing this project and I hope you have the best of luck
in using it or certain features of it in your project.

- Daniel