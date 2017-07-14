using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCCamera : MonoBehaviour {

	public bool lockCursor;
	public bool isPan = false;
	public float mouseSensitivity = 10;
	public float dstFromTargetOverShoulder = 7;
	public Vector3 dstFromTargetPan = new Vector3 (0, 0, 0);
	public Vector2 pitchMinMax = new Vector2 (0, 85);
	public float rotationSmoothTime = .12f;

	bool isFocused;
	float yaw;
	float pitch;

	NOCPlayerController playerController;
	Transform target;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	void Start () {
		SwitchPlayer();
	}
	
	void LateUpdate () {
		if (isFocused == false) return;

		if (Input.GetButtonDown("Cancel")) {
			SwitchCursorLock(!lockCursor);
		}

		if (isPan)
		{
			GetInputAndFollowPlayerPan();
		} else {
			GetInputAndFollowPlayerOverShoulder();
		}
	}

	public bool SwitchPlayer () {
		if (SearchForPlayer())
		{
			isFocused = true;
			SwitchCursorLock(true);
			target = playerController.GetPlayerCameraPivot();
			return true;
		}

		return false;
	}

	void SwitchCursorLock (bool isLock)
	{
		lockCursor = isLock;
		Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;
		Cursor.visible = !isLock;
	}

	bool SearchForPlayer()
	{
		GameObject player = GameObject.FindWithTag("Player");
		if (player != null)
		{
			playerController = player.GetComponent<NOCPlayerController>();
			return true;
		}

		return false;
	}

	void GetInputAndFollowPlayerOverShoulder()
	{
		if (!lockCursor) return;

		yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);
		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

		transform.eulerAngles = currentRotation;
		transform.position = target.position - transform.forward * dstFromTargetOverShoulder;
	}

	void GetInputAndFollowPlayerPan()
	{
		transform.position = target.position + dstFromTargetPan;
		transform.rotation = Quaternion.Euler(50, -90, 0);
	}
}
