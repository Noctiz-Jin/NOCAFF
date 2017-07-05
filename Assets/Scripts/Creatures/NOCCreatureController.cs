using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NOCCreatureController : MonoBehaviour {

	////// Movement Assets //////
	public float walkSpeed = 2;
	public float runSpeed = 6;
	const float defaultWalkSpeed = 2;
	const float defaultRunSpeed = 8;
	public float speedSmoothTime = 0.1f;

	private NOCCreatureAnimator creatureAnimator;
	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		creatureAnimator = GetComponent<NOCCreatureAnimator>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		float currentSpeed = agent.velocity.magnitude;
		bool running = currentSpeed > 2;
		float speedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
		creatureAnimator.CreatureMoverAnimation(speedPercent, speedSmoothTime, walkSpeed/defaultWalkSpeed);
		//Debug.Log(agent.velocity);
	}

	private void CreatureHandsHold()
	{
		
	}
}
