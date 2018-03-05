using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float xSpeed = 10, ySpeed = 10, jumpVelocity = 20;
	public LayerMask playerMask;
	public bool touchingPlatform, touchingWall, touchingClimbable, 
		canDoubleJump, 
		canMoveInAir = true;
	Transform myTrans, tagGround;
	Rigidbody2D rb;
	float hInput = 0;
	Vector3 reset;

	void Start ()
	{
		rb = this.GetComponent<Rigidbody2D>();
		myTrans = this.transform;
		reset = new Vector3 (-4, 2, 0);
		rb.position = reset;
	}

	void FixedUpdate ()
	{
		//touchingClimbable = Physics2D.Linecast(myTrans.position, );

		#if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
		Move(Input.GetAxisRaw("Horizontal"));
		Climb(Input.GetAxisRaw("Vertical"));
		if(Input.GetButtonDown("Jump"))
			Jump(Input.GetAxisRaw("Horizontal"));
		#else
		Move (hInput);
		#endif
	}

	void Move(float horizontalInput)
	{
		if (!canMoveInAir && !touchingPlatform && !touchingClimbable)
			return;

		Vector2 moveVel = rb.velocity;
		moveVel.x = horizontalInput * xSpeed;
		if(touchingClimbable)
			moveVel.y = 0;
		rb.velocity = moveVel;

		if(horizontalInput != 0)
			transform.Rotate (Vector3.back * horizontalInput * 10);

	}

	void Climb (float verticalInput) {
		if (touchingClimbable) {
			Vector2 moveVel = rb.velocity;
			moveVel.y = verticalInput * ySpeed;
			rb.velocity = moveVel;
		}
	}

	public void Jump(float horizontalInput)
	{
		
		if (touchingPlatform)
			rb.velocity = jumpVelocity * Vector2.up;


		else if (touchingWall) {
			//Vector2 moveVel = rb.velocity;
			Vector2 moveVel = transform.InverseTransformDirection(rb.velocity);
			moveVel.y = jumpVelocity;

			if (horizontalInput == -1) {
				moveVel.x = xSpeed * 10;
				//rb.AddForce (-transform.right * 50);
			} else if (horizontalInput == 1) {
				moveVel.x = -xSpeed * 10;
				//rb.AddForce (-transform.right * 50);
			}
			rb.velocity = moveVel;

		} else if (canDoubleJump) {
			rb.velocity = jumpVelocity * Vector2.up;
			canDoubleJump = false;
		}
	}

	public void StartMoving(float horizonalInput)
	{
		hInput = horizonalInput;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "platform") {
			touchingPlatform = true;
			canDoubleJump = true;
		}
		if (other.gameObject.tag == "wall") {
			touchingWall = true;
			canDoubleJump = true;
		}
		if (other.gameObject.tag == "climbable") {
			touchingClimbable = true;
			canDoubleJump = true;
		}
		if (other.gameObject.tag == "death") {
			gameObject.transform.position = reset;
			canDoubleJump = true;
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "platform")
			touchingPlatform = false;
		if (other.gameObject.tag == "wall")
			touchingWall = false;
		if (other.gameObject.tag == "climbable")
			touchingClimbable = false;
	}

}