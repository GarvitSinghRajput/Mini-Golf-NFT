using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public Rigidbody2D rig;
	public bool canHit = true;
    public float powerMultiplier = 25.0f;
	public GameManager gameManager;

	void Update ()
	{
		if(!canHit){
			if(rig.velocity.magnitude < 0.05f){
                canHit = true;

				//If the game is multiplayer, then after this ball gets hit
				//end this players turn and allow the next player to hit the ball.
				if(gameManager.isMultiplayer){
					gameManager.NextPlayerTurn();
				}
			}
		}
	}

	//Called when the player prepares a hit and lets go of the left mouse button.
	//It sends the direction that the ball is going to move in.
	//And it sends the power which is how hard the ball gets hit.
	public void HitBall (Vector2 direction, float power)
	{
        rig.AddForce(direction * (power * powerMultiplier) * Time.deltaTime, ForceMode2D.Impulse);
	}
}
