using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerHands : MonoBehaviour {

	private NOCPlayerAnimator playerAnimator;
	private static string leftHandLookUp = "Armature/Root/Hip/Spine/Upper_Arm_L/Lower_Arm_L/Hand_L/LeftHandHold";
	private static string rightHandLookUp = "Armature/Root/Hip/Spine/Upper_Arm_R/Lower_Arm_R/Hand_R/RightHandHold";
	private Transform leftHandHold;
	private Transform rightHandHold;

	void Start () {
		playerAnimator = GetComponent<NOCPlayerAnimator>();
		leftHandHold = gameObject.transform.Find(leftHandLookUp);
		rightHandHold = gameObject.transform.Find(rightHandLookUp);
	}
	
	public void GetHandsInputAndAct()
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
	}

	private void GrabWithLeftHand(bool isLeft)
	{
		GameObject sword = GameObject.Find("NOCSwordHandle");
		if (isLeft)
		{
			sword.GetComponent<NOCHandle>().GrabHandle(leftHandHold, leftHandHold);
		} else {
			sword.GetComponent<NOCHandle>().GrabHandle(rightHandHold, rightHandHold);
		}
	}
}
