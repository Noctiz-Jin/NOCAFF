using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCGameManager : MonoBehaviour {

	// singleton self
	public static NOCGameManager instance = null;
	// WorldManager instance
	private NOCWorldManager worldManager;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		//DontDestroyOnLoad (gameObject);
		Debug.Log("--GameManager Loaded--");

		worldManager = GetComponent<NOCWorldManager> ();
		worldManager.BluePrint ();
		worldManager.SetupScene ();
	}

	// Use this for initialization
	void Start () {

	}
}
