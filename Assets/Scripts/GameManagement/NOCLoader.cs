using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Entry point of the Game
public class NOCLoader : MonoBehaviour {

	public GameObject gameManager;

	void Awake () {
		if (NOCGameManager.instance == null)
			Instantiate (gameManager).name = "GameManager";
	}

//	void Start () {
//		Camera.main.aspect = 480f / 800f;
//	}
}
