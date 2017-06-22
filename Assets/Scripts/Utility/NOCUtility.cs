using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCUtility : MonoBehaviour {

	public static Vector3 RandomVector3(Vector3 origin, Vector3 jitter = default(Vector3))
	{
		Vector3 randomJitter;
		if (jitter == Vector3.zero)
		{
			randomJitter = new Vector3(Random.Range(-1.00f, 1.00f), Random.Range(-1.00f, 1.00f), Random.Range(-1.00f, 1.00f));
		} else {
			randomJitter = new Vector3(Random.Range(-jitter.x, jitter.x), Random.Range(-jitter.y, jitter.y), Random.Range(-jitter.z, jitter.z));
		}
		return origin + randomJitter;
	}

	public static Vector3 RandomEulerAngles(Vector3 range = default(Vector3))
	{
		if (range == Vector3.zero)
		{
			return new Vector3(Random.Range(-180.00f, 180.00f), Random.Range(-180.00f, 180.00f), Random.Range(-180.00f, 180.00f));
		} else {
			return new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), Random.Range(-range.z, range.z));
		}
	}
}
