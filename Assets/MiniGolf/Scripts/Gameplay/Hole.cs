using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour 
{
	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Ball"){
			
			//Is the ball travelling less than 8 units per second? This is here so that
			//a ball travelling really fast wont sink in the hole.
			if(col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 8){
				int ballId = 0;	//Ball Id relates back to the player whose ball it is.
					
				if(col.gameObject.name.Contains("Player")){
					ballId = int.Parse(col.gameObject.name.Replace("Player", ""));
				}

				GameObject.Find("_GameManager").GetComponent<GameManager>().SinkBall(ballId);
			}
		}
	}
}
