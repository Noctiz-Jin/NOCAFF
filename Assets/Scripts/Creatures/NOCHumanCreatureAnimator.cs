using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCHumanCreatureAnimator : MonoBehaviour {

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	public void CreatureMoverAnimation(float speedPercent, float speedSmoothTime, float animationSpeed)
	{
		animator.SetFloat ("SpeedPercent", speedPercent, speedSmoothTime, Time.deltaTime);
		animator.SetFloat ("AnimationSpeed", animationSpeed);

	}

	public void CreatureStaticAnimation(string animation)
	{
		animator.SetTrigger(animation);
	}
}
