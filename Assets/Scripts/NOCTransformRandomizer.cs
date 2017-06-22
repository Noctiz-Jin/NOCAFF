using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCTransformRandomizer : MonoBehaviour {

	void Awake () {
		transform.position = NOCUtility.RandomVector3(transform.position);
		transform.eulerAngles = NOCUtility.RandomEulerAngles();
	}
}
