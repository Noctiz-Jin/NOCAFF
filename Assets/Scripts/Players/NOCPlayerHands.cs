using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerHands : MonoBehaviour {

	public float turnSmoothTime = 0.01f;
	NOCPlayerAnimator playerAnimator;
	static string leftHandLookUp = "Armature/Root/Hip/Spine/Upper_Arm_L/Lower_Arm_L/Hand_L/LeftHandHold";
	static string rightHandLookUp = "Armature/Root/Hip/Spine/Upper_Arm_R/Lower_Arm_R/Hand_R/RightHandHold";
	Transform leftHandHold;
	Transform rightHandHold;
	float turnSmoothVelocity;
	bool actionOn = true;

	NOCPlayerCameraManager playerCameraManager;

	void Start () {
		playerAnimator = GetComponent<NOCPlayerAnimator>();
		leftHandHold = gameObject.transform.Find(leftHandLookUp);
		rightHandHold = gameObject.transform.Find(rightHandLookUp);
		playerCameraManager = GetComponent<NOCPlayerCameraManager> ();
	}
	
	public void GetHandsInputAndAct()
	{
		if (playerAnimator.PlayerLeftInteracting() && actionOn) {
			AdjustFacingWithCamera();
			if (!Input.GetButton("Fire1"))
			{
				playerAnimator.PlayerLeftInteractionOff();
				actionOn = false;
			}
		} else if (playerAnimator.PlayerCanMove())
		{
			if (Input.GetButtonDown("LeftHandAct"))
			{
				playerAnimator.PlayerHandHold(true);
				GrabWithLeftHand(true);
			}

			if (Input.GetButtonDown("RightHandAct"))
			{
				playerAnimator.PlayerHandHold(false);
				GrabWithLeftHand(false);
			}

			if (Input.GetButtonDown("Fire2"))
			{
				playerAnimator.PlayerHorizontalRightSlash();
			}

			if (Input.GetButtonDown("Fire1"))
			{
				playerAnimator.PlayerLeftInteractionOn();
				actionOn = true;
			}
		}
	}

	void GrabWithLeftHand(bool isLeft)
	{
		if (isLeft)
		{
			GameObject sword = GameObject.Find("NOCSwordHandle");
			sword.GetComponent<NOCHandle>().GrabHandle(leftHandHold, leftHandHold);
		} else {
			GameObject sword = GameObject.Find("NOCSwordHandle2");
			sword.GetComponent<NOCHandle>().GrabHandle(rightHandHold, rightHandHold);
		}
	}

	void AdjustFacingWithCamera()
	{
		float targetRotation = playerCameraManager.GetPlayerCameraEulerY();
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
	}
}
