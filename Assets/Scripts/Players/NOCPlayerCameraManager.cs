using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerCameraManager : MonoBehaviour {

	private Transform cameraTransform;

	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;
	}
	
	public float GetPlayerCameraEulerY()
	{
		return cameraTransform.eulerAngles.y;
	}
}
