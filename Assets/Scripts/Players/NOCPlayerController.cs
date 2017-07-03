using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerController : MonoBehaviour {

	private NOCPlayerMover playerMover;
	private NOCPlayerHands playerHands;

	// Use this for initialization
	void Start () {
		playerMover = GetComponent<NOCPlayerMover>();
		playerHands = GetComponent<NOCPlayerHands>();
	}
	
	// Update is called once per frame
	void Update () {
		playerMover.GetMoveInputAndMove();
		playerHands.GetHandsInputAndAct();
	}

	public Transform GetPlayerCameraPivot()
	{
		return gameObject.transform.GetChild(2);
	}
}
