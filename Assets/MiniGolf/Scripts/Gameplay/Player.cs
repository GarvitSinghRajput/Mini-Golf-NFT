using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public string name;		//Name of the player.
	public Ball ball;		//The ball that this player will be hitting.
	public int curStroke;	//The current stroke this player has on the current hole.
	public int[] scores;	//An array of the scores for each hole. Each array element is 1 hole's score.
	public bool sunkBall;	//Has the player sunk their ball?
}
