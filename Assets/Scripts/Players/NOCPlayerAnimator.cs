using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCPlayerAnimator : MonoBehaviour {

	private Animator animator;
	//AnimatorStateInfo currentArmHandState;
	//static int castState = Animator.StringToHash("Arm Hand Layer.Cast");

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	public void PlayerMoverAnimation(float speedPercent, float speedSmoothTime, float animationSpeed)
	{
		animator.SetFloat ("SpeedPercent", speedPercent, speedSmoothTime, Time.deltaTime);
		animator.SetFloat ("AnimationSpeed", animationSpeed);
	}
}
