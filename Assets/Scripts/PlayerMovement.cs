using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {



    private const float LANE_DISTANCE = 2.0f;
    private const float TURN_SPEED = 0.05f;

    private bool IsRunning = false;

    //Animation
    private Animator anim;

    //movemenet
    private CharacterController controller;
    public float jumpForce = 4.0f;
	private float gravity = 12.0f;
	private float verticalVelocity;
	private int desiredLane = 1; // 0 left , 1 middle , 2 right
	private float startSpeed = 6.0f;
	public float speed = 7.0f;
	private float speedIncreaseLastTick;
	private float speedIncreaseTime = 2.5f;

	private float speedIncreaseAmount = 0.1f;



	private void Start(){
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();


	}

	private void Update(){

		if (! IsRunning) {
			return;
		}

       
        // hedhi bch ma yab9ach dima yzid f speed
        // bch ywalli yzid ken ba3d un certain temp 
        if (Time.time -speedIncreaseLastTick > speedIncreaseTime) {
			speedIncreaseLastTick = Time.time;
			speed += speedIncreaseAmount;
			GameManager.Instance.UpdateSpeed (speed -startSpeed);

		}
		if (MobileInputs.Instance.SwipeLeft || Input.GetKey(KeyCode.LeftArrow))
			MoveLane(false);
		if (MobileInputs.Instance.SwipeRight || Input.GetKey(KeyCode.RightArrow))
			MoveLane(true);


		Vector3 targetPosition = transform.position.z * Vector3.forward;
		if (desiredLane == 0)
			//vector 3.let Shorthand for writing Vector3(-1, 0, 0)
			//vector 3.right Shorthand for writing Vector3(1, 0, 0)
			targetPosition += Vector3.left * LANE_DISTANCE;
		else if (desiredLane == 2)
			targetPosition += Vector3.right * LANE_DISTANCE;


		// let's calculate our move delta 

		Vector3 moveVector = Vector3.zero;

		moveVector.x = (targetPosition - transform.position).normalized.x * speed;

		bool isGrounded = IsGrounded ();
		anim.SetBool ("Grounded", isGrounded);

		//calculate Y
		if (isGrounded) 
		{ // if grounded 
			//verticalVelocity = 0.0f;

			if (MobileInputs.Instance.SwipeUp || Input.GetKey(KeyCode.UpArrow)) {
				//jump
				anim.SetTrigger("Jump");
				verticalVelocity = jumpForce;
			}else if (MobileInputs.Instance.SwipeDown || Input.GetKey(KeyCode.DownArrow)) {
				StartSliding ();
				Invoke ("StopSliding", 1.0f);
			}

            if (GameManager.Instance.activePowerUp)
            {
                GameManager.Instance.powerUpAnim.SetTrigger("TimerShow");
                jumpForce = 10.1f;
                GameManager.Instance.powerUpAnim.SetTrigger("TimerHide");
                GameManager.Instance.activePowerUp = false;
                Invoke("ResetJump", 4);

            }




        } 
		else 
		{
			verticalVelocity -= (gravity * Time.deltaTime);


			//fast falling mechanic 
			if (MobileInputs.Instance.SwipeDown || Input.GetKey(KeyCode.DownArrow)) 
			{
				verticalVelocity = -jumpForce;
			}

           
        }

		moveVector.y = verticalVelocity;
		moveVector.z = speed;

		//move penguin

		controller.Move (moveVector * Time.deltaTime);

		// Rotate penguin to wher is he going

		Vector3 dir = controller.velocity;
		if (dir != Vector3.zero) 
		{
			dir.y = 0;
			transform.forward = Vector3.Lerp (transform.forward, dir, TURN_SPEED);
		}


       





    }

	private void MoveLane(bool goingRight){
		desiredLane += (goingRight) ? 1 : -1;
		desiredLane = Mathf.Clamp (desiredLane, 0,2);

	}

    void ResetJump() {
        jumpForce = 4.0f;
    }


	private bool IsGrounded()
	{
		Ray groundRay = new Ray (
			new Vector3 (controller.bounds.center.x,(controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,controller.bounds.center.z),
			Vector3.down);

		Debug.DrawRay (groundRay.origin, groundRay.direction, Color.cyan, 1.0f);

		return (Physics.Raycast (groundRay, 0.2f + 1.0f));
	}

	public void StartRunning(){
		IsRunning = true;
		anim.SetTrigger ("StartRunning");
	}

	public void StartSliding(){
	
		anim.SetBool ("sliding", true);
		controller.height /= 2;
		controller.center = new Vector3 (controller.center.x, controller.center.y/2, controller.center.z);
	
	}

	public void StopSliding(){

		anim.SetBool ("sliding", false);
		controller.height *= 2;
		controller.center = new Vector3 (controller.center.x, controller.center.y*2, controller.center.z);


	}


	private void OnControllerColliderHit(ControllerColliderHit hit){
	
		switch (hit.gameObject.tag) {
		case "Obstacle":
			Crash ();
			break;
	
		}
	}


	private void Crash(){
		anim.SetTrigger ("Death");
		IsRunning = false;
		GameManager.Instance.OnDeath();
		//GameManager.Instance.backMusic.Stop ();

	}


}
