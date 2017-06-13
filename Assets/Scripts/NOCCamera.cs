using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCCamera : MonoBehaviour {

	public bool lockCursor;
	public float mouseSensitivity = 10;
	public float dstFromTarget = 4;
	public Vector2 pitchMinMax = new Vector2 (0, 85);
	public float rotationSmoothTime = .12f;

	bool isFocused;
	float yaw;
	float pitch;

	private NOCPlayerController playerController;
	private Transform target;
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

		GetInputAndFollowPlayer();
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

	private void SwitchCursorLock (bool isLock)
	{
		lockCursor = isLock;
		Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;
		Cursor.visible = !isLock;
	}

	private bool SearchForPlayer()
	{
		GameObject player = GameObject.FindWithTag("Player");
		if (player != null)
		{
			playerController = player.GetComponent<NOCPlayerController>();
			return true;
		}

		return false;
	}

	private void GetInputAndFollowPlayer()
	{
		if (!lockCursor) return;

		yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);
		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

		transform.eulerAngles = currentRotation;
		transform.position = target.position - transform.forward * dstFromTarget;
	}
}
