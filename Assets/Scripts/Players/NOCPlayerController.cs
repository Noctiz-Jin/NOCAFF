using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerController : MonoBehaviour {

	private NOCPlayerMover playerMover;
	private NOCPlayerHands playerHands;
	private NOCPlayerAnimator playerAnimator;

	// Use this for initialization
	void Start () {
		playerMover = GetComponent<NOCPlayerMover>();
		playerHands = GetComponent<NOCPlayerHands>();
		playerAnimator = GetComponent<NOCPlayerAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
		playerMover.GetRollInputAndDodge();
		if (playerAnimator.PlayerCanMove())
		{
			playerMover.GetMoveInputAndMove();
		}
		playerHands.GetHandsInputAndAct();
	}

	public Transform GetPlayerCameraPivot()
	{
		return gameObject.transform.GetChild(2);
	}
}
