﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerAnimator : MonoBehaviour {

	private Animator animator;
	AnimatorStateInfo currentLeftHandState;
	AnimatorStateInfo currentRightHandState;
	static int leftHandHoldingState = Animator.StringToHash("LeftHandMoving Layer.HandHoldingWave");
	static int rightHandHoldingState = Animator.StringToHash("RightHandMoving Layer.HandHoldingWave");


	// Constants
	static float HandsWavingThreshold = 0.6f;
	static float HandsWavingWeight = 0.6f;

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	public void PlayerMoverAnimation(float speedPercent, float speedSmoothTime, float animationSpeed)
	{
		animator.SetFloat ("SpeedPercent", speedPercent, speedSmoothTime, Time.deltaTime);
		animator.SetFloat ("AnimationSpeed", animationSpeed);

		if (speedPercent < HandsWavingThreshold)
		{
			SetHandsMovingWeight(0);
		} else {
			SetHandsMovingWeight(HandsWavingWeight);
		}
	}

	public void PlayerHandHold(bool isLeft)
	{
		if (isLeft)
		{
			currentLeftHandState = animator.GetCurrentAnimatorStateInfo(1);
			if (currentLeftHandState.fullPathHash == leftHandHoldingState)
			{
				animator.SetBool("isLeftHandHold", false);
			} else {
				animator.SetBool("isLeftHandHold", true);
			}
		} else {
			currentRightHandState = animator.GetCurrentAnimatorStateInfo(2);
			if (currentRightHandState.fullPathHash == rightHandHoldingState)
			{
				animator.SetBool("isRightHandHold", false);
			} else {
				animator.SetBool("isRightHandHold", true);
			}
		}
	}

	private void SetHandsMovingWeight(float weight)
	{
		animator.SetLayerWeight(1, weight);
		animator.SetLayerWeight(2, weight);
	}
}
