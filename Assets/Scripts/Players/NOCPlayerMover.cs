using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerMover : MonoBehaviour {

	////// Movement Assets //////
	public float walkSpeed = 2;
	public float runSpeed = 6;
	const float defaultWalkSpeed = 2;
	const float defaultRunSpeed = 8;
	public float gravity = -12;
//	public float jumpHeight = 1;
	[Range(0,1)]
	public float airControlPercent;
	public float turnSmoothTime = 0.05f;
	float turnSmoothVelocity;
	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;


	private CharacterController characterController;
	private NOCPlayerCameraManager playerCameraManager;
	private NOCPlayerAnimator playerAnimator;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController> ();
		playerCameraManager = GetComponent<NOCPlayerCameraManager> ();
		playerAnimator = GetComponent<NOCPlayerAnimator>();
	}

	public void GetMoveInputAndMove()
	{
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey (KeyCode.LeftShift);

		if (running && (inputDir.x != 0 || inputDir.y != 0)) {
			running = true;
		}

		MovePlayer(inputDir, running);

		float speedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
		playerAnimator.PlayerMoverAnimation(speedPercent, speedSmoothTime, walkSpeed/defaultWalkSpeed);
	}

	private void MovePlayer(Vector2 inputDir, bool running)
	{
		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + playerCameraManager.GetPlayerCameraEulerY();
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}
			
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

		characterController.Move (velocity * Time.deltaTime);
		currentSpeed = new Vector2 (characterController.velocity.x, characterController.velocity.z).magnitude;

		if (characterController.isGrounded) {
			velocityY = 0;
		}
	}

	private float GetModifiedSmoothTime(float smoothTime) {
		if (characterController.isGrounded) {
			return smoothTime;
		}

		if (airControlPercent == 0) {
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}
}
