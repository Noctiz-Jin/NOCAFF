using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerHands : MonoBehaviour {

	private NOCPlayerAnimator playerAnimator;

	void Start () {
		playerAnimator = GetComponent<NOCPlayerAnimator>();
	}
	
	public void GetHandsInputAndAct()
	{
		if (Input.GetButtonDown("LeftHandAct"))
		{
			playerAnimator.PlayerHandHold(true);
		}

		if (Input.GetButtonDown("RightHandAct"))
		{
			playerAnimator.PlayerHandHold(false);
		}
	}
}
