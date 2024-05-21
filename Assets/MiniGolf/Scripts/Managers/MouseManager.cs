using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour 
{
	public bool preparingHit;		//Are we currently preparing a hit?
	public Vector3 mousePositon;	//The world position of the mouse.
	public float maxPower;			//The maximum power that the ball can be hit with.

	private float hitPower;			//The current power that the ball will be hit with.
	private Vector2 hitDirection;	//The direction of the hit.

	public GameManager gameManager;	

	void Start ()
	{
		//gameManager.ui.powerBar.maxValue = maxPower;
	}

	void Update ()
	{
		//Gets the position of the mouse in the world
		mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositon = new Vector3(mousePositon.x, mousePositon.y, 0.0f);

		//Did we just press down on the left mouse button?
		if(Input.GetMouseButtonDown(0)){

			//Send out a raycast from where our mouse is on screen
			RaycastHit2D hit = Physics2D.Raycast(mousePositon, Vector2.zero);

			//Has the raycast hit something and does that something have a collider?
			if(hit != null && hit.collider != null){

				//If so, is the name of that object "GolfBall"?
				if(hit.collider.tag == "Ball"){
					if(gameManager.players[gameManager.curPlayerTurn].ball.canHit){
						preparingHit = true;
						gameManager.players[gameManager.curPlayerTurn].ball.transform.Find("Line").gameObject.active = true;
					}
				}
			}
		}

		//Are we holding down the left mouse button?
		if(Input.GetMouseButton(0)){
			
			//Are we currently preparing a hit?
			if(preparingHit){
				if(Vector2.Distance(mousePositon, gameManager.curBall.transform.position) * 20 < maxPower){
					hitPower = Vector2.Distance(mousePositon, gameManager.curBall.transform.position) * 20;
				}else{
					hitPower = maxPower;
				}

				hitDirection = gameManager.curBall.transform.position - mousePositon;
			}
		}

		//Did we let go of the left mouse button?
		if(Input.GetMouseButtonUp(0)){
			
			//Are we currently preparing a hit?
			//If so, set preparingHit to false and hit the ball
			if(preparingHit){
				preparingHit = false;
				gameManager.players[gameManager.curPlayerTurn].ball.HitBall(hitDirection.normalized, hitPower);
				gameManager.players[gameManager.curPlayerTurn].ball.transform.Find("Line").gameObject.active = false;
				gameManager.players[gameManager.curPlayerTurn].ball.canHit = false;
				gameManager.players[gameManager.curPlayerTurn].curStroke++;
				gameManager.ui.UpdatePlayerStrokeUI();
				gameManager.ui.powerBar.fillAmount = 0.0f;
				hitPower = 0;
			}
		}

		//Are we currently preparing a hit?
		if(preparingHit){
			//Set the line renderer to connect from the ball to our mouse position.
			gameManager.players[gameManager.curPlayerTurn].ball.transform.Find("Line").GetComponent<LineRenderer>().SetPosition(0, gameManager.players[gameManager.curPlayerTurn].ball.transform.position - Vector3.forward);
			gameManager.players[gameManager.curPlayerTurn].ball.transform.Find("Line").GetComponent<LineRenderer>().SetPosition(1, mousePositon - Vector3.forward);

			//Set the power bar to match our current hit power.
			gameManager.ui.powerBar.fillAmount = (1.0f / maxPower) * hitPower;
		}
	}
}
