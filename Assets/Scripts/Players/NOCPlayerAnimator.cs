using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerAnimator : MonoBehaviour {

	private Animator animator;
	AnimatorStateInfo currentBaseState;
	AnimatorStateInfo currentLeftHandState;
	AnimatorStateInfo currentRightHandState;
	static int movingState = Animator.StringToHash("Base Layer.MoveBlendTree");
	static int dodgingState = Animator.StringToHash("Base Layer.RollDodge");
	static int leftHandHoldingState = Animator.StringToHash("LeftHandMoving Layer.HandHoldingWave");
	static int rightHandHoldingState = Animator.StringToHash("RightHandMoving Layer.HandHoldingWave");


	// Constants //
	static float HandsWavingThreshold = 0.6f;
	static float HandsWavingWeight = 0.6f;

	void Start () {
		animator = GetComponent<Animator> ();
	}

	public bool PlayerCanMove()
	{
		currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		if (currentBaseState.fullPathHash == movingState)
		{
			return true;
		} else {
			SetHandsMovingWeight(0);
			return false;
		}
	}

	public bool PlayerDodging()
	{
		currentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		if (currentBaseState.fullPathHash == dodgingState)
		{
			SetHandsMovingWeight(0);
			return true;
		} else {
			return false;
		}
	}

	public void PlayerRollDodge()
	{
		animator.SetTrigger("RollDodge");
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

	public void PlayerHorizontalRightSlash()
	{
		animator.SetTrigger("HorizontalRightSlash");
	}

	public void PlayerHorizontalLeftSlash()
	{
		animator.SetTrigger("HorizontalLeftSlash");
	}

	private void SetHandsMovingWeight(float weight)
	{
		animator.SetLayerWeight(1, weight);
		animator.SetLayerWeight(2, weight);
	}
}
