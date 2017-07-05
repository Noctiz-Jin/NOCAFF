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
		if (playerAnimator.PlayerCanMove())
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
				playerAnimator.PlayerHorizontalLeftSlash();
			}
		}
	}

	private void GrabWithLeftHand(bool isLeft)
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
}
