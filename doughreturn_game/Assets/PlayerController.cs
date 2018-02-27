using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float xSpeed = 10, ySpeed = 10, jumpVelocity = 20;
	public LayerMask playerMask;
	public bool isGrounded, canClimb, canMoveInAir = true;
	Transform myTrans, tagGround;
	Rigidbody2D myBody;
	float hInput = 0;
	Vector3 reset;

	void Start ()
	{
		myBody = this.GetComponent<Rigidbody2D>();
		myTrans = this.transform;
		reset = new Vector3 (0, 2, 0);
		isGrounded = false;
		canClimb = false;
	}

	void FixedUpdate ()
	{
		//canClimb = Physics2D.Linecast(myTrans.position, );

		#if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
		Move(Input.GetAxisRaw("Horizontal"));
		Climb(Input.GetAxisRaw("Vertical"));
		if(Input.GetButtonDown("Jump"))
			Jump();
		#else
		Move (hInput);
		#endif
	}

	void Move(float horizonalInput)
	{
		if (!canMoveInAir && !isGrounded && !canClimb)
			return;

		Vector2 moveVel = myBody.velocity;
		moveVel.x = horizonalInput * xSpeed;
		myBody.velocity = moveVel;

	}

	void Climb (float verticalInput) {
		if (canClimb) {
			Vector2 moveVel = myBody.velocity;
			moveVel.y = verticalInput * ySpeed;
			myBody.velocity = moveVel;
		}
	}

	public void Jump()
	{
		if(isGrounded)
			myBody.velocity += jumpVelocity * Vector2.up;
		if(canClimb)
			myBody.velocity += jumpVelocity * Vector2.up;
	}

	public void StartMoving(float horizonalInput)
	{
		hInput = horizonalInput;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "platform")
			isGrounded = true;
		if (other.gameObject.tag == "climbable")
			canClimb = true;
		if (other.gameObject.tag == "death")
			gameObject.transform.position = reset;
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "platform")
			isGrounded = false;
		if (other.gameObject.tag == "climbable")
			canClimb = false;
	}

}