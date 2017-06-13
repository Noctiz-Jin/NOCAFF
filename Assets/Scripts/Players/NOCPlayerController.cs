using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerController : MonoBehaviour {

	private NOCPlayerMover playerMover;

	// Use this for initialization
	void Start () {
		playerMover = GetComponent<NOCPlayerMover>();
	}
	
	// Update is called once per frame
	void Update () {
		playerMover.GetMoveInputAndMove();
	}

	public Transform GetPlayerCameraPivot()
	{
		return gameObject.transform.GetChild(2);
	}
}
